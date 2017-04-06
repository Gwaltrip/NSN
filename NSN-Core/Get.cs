using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NSN.Core
{
    public class Get
    {
        public string Payload { get; set; }

        public Get(object obj)
        {
            Payload = JsonConvert.SerializeObject(obj);
        }

        public Get()
        {
            
        }

        public override string ToString()
        {
            return Payload;
        }
    }
}
