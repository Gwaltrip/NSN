using Newtonsoft.Json;

namespace NSN.Core
{
    internal class Json
    {
        private readonly string _payload;

        public Json(object obj)
        {
            _payload = JsonConvert.SerializeObject(obj);
        }

        public override string ToString()
        {
            return _payload;
        }
    }
}
