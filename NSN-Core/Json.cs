using Newtonsoft.Json;
using System.Collections.Generic;

namespace NSN.Core
{
    public class Json
    {
        private readonly string _payload;

        public Json(object obj)
        {
            _payload = JsonConvert.SerializeObject(obj);
        }

        public static T ToObject<T>(string payload)
        {
            T obj;
            try
            {
                obj = JsonConvert.DeserializeObject<T>(payload);
                return obj;
            }
            catch(System.Exception e)
            {
                System.Console.WriteLine($"Error on String: '{payload}'");
                System.Console.WriteLine(e.StackTrace);
            }
            return default(T);
        } 
        
        public override string ToString()
        {
            return _payload;
        }
    }
}
