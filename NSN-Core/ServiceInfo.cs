using System;
using System.Globalization;
using System.Reflection;
using RestSharp;

namespace NSN.Core
{
    public class ServiceInfo:MethodInfo
    {
        private string _uri;
        private MethodInfo _methodInfo;
        private RestClient restClient;
        public ServiceInfo(string uri, MethodInfo methodInfo)
        {
            _uri = uri;
            _methodInfo = methodInfo;
            restClient = new RestClient(uri);
        }

        public override ICustomAttributeProvider ReturnTypeCustomAttributes => _methodInfo.ReturnTypeCustomAttributes;

        public override RuntimeMethodHandle MethodHandle => _methodInfo.MethodHandle;

        public override MethodAttributes Attributes => _methodInfo.Attributes;

        public override string Name => _methodInfo.Name;

        public override Type DeclaringType => _methodInfo.DeclaringType;

        public override Type ReflectedType => _methodInfo.ReflectedType;

        public override MethodInfo GetBaseDefinition()
        {
            return _methodInfo.GetBaseDefinition();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return _methodInfo.GetCustomAttributes(inherit);
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _methodInfo.GetCustomAttributes(attributeType, inherit);
        }

        public override MethodImplAttributes GetMethodImplementationFlags()
        {
            return _methodInfo.GetMethodImplementationFlags();
        }

        public override ParameterInfo[] GetParameters()
        {
            return _methodInfo.GetParameters();
        }

        public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public T Invoke<T>(params object[] parameters)
        {
            var request = new RestRequest($"/{_methodInfo.Name}");
            if (parameters?.Length > 0)
            {
                for (var index = 0; index < _methodInfo.GetParameters().Length; index++)
                {
                    var param = _methodInfo.GetParameters()[index];
                    request.AddParameter(param.Name, parameters[index]);
                }
            }

            var response = restClient.Execute(request);
            var content = response.Content;
            
            return Json.ToObject<T>(content);
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return _methodInfo.IsDefined(attributeType, inherit);
        }

        public static ReturnType Invoke<InheirantType, ReturnType>(string uri, string method, params object[] obj)
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
        public static ReturnType Invoke<InheirantType, ReturnType>(string uri, string method)
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
