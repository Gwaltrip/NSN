using System.Reflection;
using System.Dynamic;
using RestSharp;

namespace NSN.Core
{
    public class ServiceDynamicObject<T>: DynamicObject
    {
        private readonly string _uri;
        private readonly RestClient _restClient;

        public ServiceDynamicObject(string uri)
        {
            _uri = uri;
            _restClient = new RestClient(_uri);
        }

        public TR Pass<TR>(string method, params object[] obj)
        {
            MethodInfo methodInfo = typeof(T).GetMethod(method);
            if (methodInfo is null)
                throw new System.MissingMethodException($"No Method by type '{method}'!");
            var request = new RestRequest($"/{method}");
            if (obj?.Length > 0)
            {
                for (var index = 0; index < methodInfo.GetParameters().Length; index++)
                {
                    var param = methodInfo.GetParameters()[index];
                    request.AddParameter(param.Name, obj[index]);
                }
            }

            var response = _restClient.Execute(request);
            var content = response.Content;

            return Json.ToObject<TR>(content);
        }
        public TR Pass<TR>(string method)
        {
            MethodInfo methodInfo = typeof(T).GetMethod(method);
            var request = new RestRequest($"/{method}");

            var response = _restClient.Execute(request);
            var content = response.Content;

            return Json.ToObject<TR>(content);
        }
    }
}
