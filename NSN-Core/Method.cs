using System;
using System.Reflection;
using RestSharp;

namespace NSN.Core
{
    public interface IMethod
    {

    }

    public class Method<T>
    {
        private readonly string _uri;
        private readonly RestClient _restClient;

        public Method(string uri)
        {
            _uri = uri;
            _restClient = new RestClient(_uri);
            //d_masterObject = obj;
        }
        public TR Pass<TR>(string method ,params object[] obj)
        {
            MethodInfo methodInfo = typeof(T).GetMethod(method);
            var request = new RestRequest(methodInfo.Name);
            for (var index = 0; index < methodInfo.GetParameters().Length; index++)
            {
                var param = methodInfo.GetParameters()[index];
                request.AddParameter(param.Name, obj[index]);
            }

            var response = _restClient.Execute(request);
            var content = response.Content;

            return Json.ToObject<TR>(content);
        }
    }
}
