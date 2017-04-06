using Newtonsoft.Json;

namespace NSN.Core
{
    public class Get
    {
        private readonly string _payload;

        public Get(object obj)
        {
            _payload = JsonConvert.SerializeObject(obj);
        }

        public override string ToString()
        {
            return _payload;
        }
    }
}
