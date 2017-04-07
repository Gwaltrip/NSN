using System;
using System.Collections.Generic;
using System.Reflection;
using Nancy;

namespace NSN.Core.Modules
{
    public class CoreModule : NancyModule
    {
        internal static object CoreObject { private get; set; }

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
                        if(!types.ContainsKey(i.Name))
                            types.Add(i.Name, i);
                        if (i.GetParameters().Length == 0)
                            continue;
                        if (parameters.ContainsKey(i.Name))
                            continue;
                        parameters.Add(i.Name, new List<ParameterInfo>());
                        parameters[i.Name].AddRange(i.GetParameters());
                    }
                    set = true;
                }
                if (!types.ContainsKey(_.route.ToString()))
                    return HttpStatusCode.NotFound;

                var paraObjects = new List<object>();
                if (parameters.ContainsKey(_.route.ToString()))
                {
                    foreach (ParameterInfo p in parameters[_.route.ToString()])
                    {
                        if (Request.Query[p.Name] != null)
                        {
                            paraObjects.Add(Convert.ChangeType(Request.Query[p.Name], p.ParameterType));
                        }
                    }
                }

                MethodInfo method = types[_.route.ToString()];
                object returnObject;
                if (method.ReturnType.IsPrimitive)
                    returnObject = method.Invoke(CoreObject, paraObjects.ToArray());
                else
                    returnObject = new Json(method.Invoke(CoreObject, paraObjects.ToArray()));
                return returnObject.ToString();
            };
        }
    }
}
