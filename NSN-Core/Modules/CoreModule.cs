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
            bool set = false;
            var types = new Dictionary<string, MethodInfo>();
            Get["/{route}"] = _ =>
            {
                if (!set)
                {
                    foreach (var i in CoreObject.GetType().GetMethods())
                    {
                        if (i.ToString().Contains(".Get "))
                        {
                            string[] tokens = i.ToString().Split(' ');
                            string name = tokens[1].Substring(0, tokens[1].Split('(')[0].Length);
                            types.Add(name, i);
                        }
                        else if (i.ToString().Contains(".Post "))
                        {
                            
                        }
                    }
                    set = true;
                }
                if (types.ContainsKey(_.route.ToString()))
                {
                    return types[_.route.ToString()].Invoke(CoreObject, null).ToString();
                }
                return HttpStatusCode.NotFound;
            };
        }
    }
}
