using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Nancy;

namespace NSN.Core.Modules
{
    public class CoreModule : NancyModule
    {
        public static object CoreObject { private get; set; }

        public CoreModule()
        {
            bool set = false;
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
                            if (i.GetParameters().Length > 0)
                            {
                                parameters.Add(i.Name, new List<ParameterInfo>());
                                parameters[i.Name].AddRange(i.GetParameters());
                            }
                        }
                        else if (i.ReturnType == postType)
                        {
                            
                        }
                    }
                    set = true;
                }
                if (types.ContainsKey(_.route.ToString()))
                {
                    if (parameters.ContainsKey(_.route.ToString()))
                    {
                        List<object> paraObjects = new List<object>();
                        foreach (ParameterInfo p in parameters[_.route.ToString()])
                        {
                            if (Request.Query[p.Name] != null)
                            {
                                paraObjects.Add(Convert.ChangeType(Request.Query[p.Name], p.ParameterType));
                            }
                        }
                        object[] objects = paraObjects.ToArray();
                        if (objects.Length == 0)
                            objects = null;

                        return types[_.route.ToString()].Invoke(CoreObject, objects).ToString();
                    }
                    return types[_.route.ToString()].Invoke(CoreObject, null).ToString();
                }
                return HttpStatusCode.NotFound;
            };
        }
    }
}
