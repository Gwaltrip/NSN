using System;
using System.Collections.Generic;
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
            var getType = typeof(Core.Get);
            var postType = typeof(Core.Post);
            Get["/{route}"] = _ =>
            {
                if (!set)
                {
                    foreach (var i in CoreObject.GetType().GetMethods())
                    {
                        if (i.ReturnType == getType)
                        {
                            types.Add(i.Name, i);
                            if (i.GetParameters().Length == 0)
                                continue;
                            parameters.Add(i.Name, new List<ParameterInfo>());
                            parameters[i.Name].AddRange(i.GetParameters());
                        }
                        else if (i.ReturnType == postType)
                        {
                            
                        }
                    }
                    set = true;
                }
                if (!types.ContainsKey(_.route.ToString()))
                    return HttpStatusCode.NotFound;
                if (!parameters.ContainsKey(_.route.ToString()))
                    return types[_.route.ToString()].Invoke(CoreObject, null).ToString();

                var paraObjects = new List<object>();
                foreach (ParameterInfo p in parameters[_.route.ToString()])
                {
                    if (Request.Query[p.Name] != null)
                    {
                        paraObjects.Add(Convert.ChangeType(Request.Query[p.Name], p.ParameterType));
                    }
                }

                return types[_.route.ToString()].Invoke(CoreObject, paraObjects.Count == 0 ? null : paraObjects.ToArray()).ToString();
            };
        }
    }
}
