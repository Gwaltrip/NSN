using System.Reflection;
using System.Dynamic;
using RestSharp;

namespace NSN.Core
{
    public class ServiceDynamicObject: DynamicObject
    {
        public static ReturnType Invoke<InheirantType,ReturnType>(string uri, string method, params object[] obj)
        {
            RestClient restClient = new RestClient(uri);
            MethodInfo methodInfo = typeof(InheirantType).GetMethod(method);
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

            var response = restClient.Execute(request);
            var content = response.Content;

            return Json.ToObject<ReturnType>(content);
        }
        public static ReturnType Invoke<InheirantType,ReturnType>(string uri, string method)
        {
            RestClient restClient = new RestClient(uri);
            MethodInfo methodInfo = typeof(InheirantType).GetMethod(method);
            var request = new RestRequest($"/{method}");

            var response = restClient.Execute(request);
            var content = response.Content;

            return Json.ToObject<ReturnType>(content);
        }
    }
}
