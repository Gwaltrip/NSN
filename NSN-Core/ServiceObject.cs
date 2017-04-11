using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Nancy.ModelBinding;
using RestSharp;

namespace NSN.Core
{
    public class ServiceObject
    {
        private static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
        {
            var member = expression.Body as MethodCallExpression;
            if (member == null)
                throw new ArgumentException("Expression is not a method", nameof(expression));
            return member.Method;
        }
        public static ReturnType Invoke<InheirantType, ReturnType>(string uri, Expression<Action<InheirantType>> expression, params object[] param)
        {
            MethodCallExpression methodCall = (MethodCallExpression)expression.Body;
            MemberInfo methodInfo = GetMethodInfo<InheirantType>(expression);//expression.GetTargetMemberInfo();
            RestClient restClient = new RestClient(uri);
            RestRequest request = new RestRequest($"/{methodInfo.Name}");
            
            for (var index = 0; index < methodCall.Arguments.Count; index++)
            {
                request.AddParameter(methodCall.Arguments[index].ToString().Split('.').Last(), param[index]);
            }

            var response = restClient.Execute(request);
            var content = response.Content;

            return Json.ToObject<ReturnType>(content);
        }
    }
}
