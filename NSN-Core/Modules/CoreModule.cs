using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nancy;

namespace NSN.Core.Modules
{
    public class CoreModule : NancyModule
    {
        public static object CoreObject { private get; set; }

        public CoreModule()
        {
            var set = false;
            var types = new Dictionary<string, MethodInfo>();
            var parameters = new Dictionary<string, List<ParameterInfo>>();
            Get["/{route}"] = _ =>
            {
                if (!set)
                {
                    foreach (var i in CoreObject.GetType().GetMethods())
                    {
                        types.Add(i.Name, i);
                        if (i.GetParameters().Length == 0)
                            continue;
                        parameters.Add(i.Name, new List<ParameterInfo>());
                        parameters[i.Name].AddRange(i.GetParameters());
                    }
                    set = true;
                }
                if (!types.ContainsKey(_.route.ToString()))
                    return HttpStatusCode.NotFound;

                MethodInfo method = types[_.route.ToString()];
                object returnObject;
                if (!parameters.ContainsKey(_.route.ToString()))
                {
                    if (method.ReturnType.IsPrimitive)
                        returnObject = method.Invoke(CoreObject, null);
                    else
                        returnObject = new Json(method.Invoke(CoreObject, null));
                    return returnObject.ToString();
                }

                var paraObjects = new List<object>();
                foreach (ParameterInfo p in parameters[_.route.ToString()])
                {
                    if (Request.Query[p.Name] != null)
                    {
                        paraObjects.Add(Convert.ChangeType(Request.Query[p.Name], p.ParameterType));
                    }
                }
                object[] parObjects = null;
                if (paraObjects.Count != 0)
                    parObjects = paraObjects.ToArray();

                if (method.ReturnType.IsPrimitive)
                    returnObject = method.Invoke(CoreObject, parObjects);
                else
                    returnObject = new Json(method.Invoke(CoreObject, parObjects));
                return returnObject.ToString();
            };
        }
    }
}
