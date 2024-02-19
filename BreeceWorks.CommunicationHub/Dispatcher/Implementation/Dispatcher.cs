using BreeceWorks.CommunicationHub.Dispatcher.Contracts;
using BreeceWorks.CommunicationHub.Dispatcher.Proxies;
using BreeceWorks.Shared.Services;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BreeceWorks.CommunicationHub.Dispatcher.Implementation
{
    public class Dispatcher : IDispatcher
    {
        private readonly IConfigureService _configureService;
        private readonly HttpClient _communicationHttpClient;



        public Dispatcher(IConfigureService configureService, HttpClient communicationHttpClient)
        {
            _configureService = configureService;
            _communicationHttpClient = communicationHttpClient;
            _communicationHttpClient.BaseAddress = new Uri(_configureService.GetValue("BreeceWorks.CommunicationWebApi"));
            _communicationHttpClient.DefaultRequestHeaders.Add(SystemConstants.Headers.XAPIKEY, _configureService.GetValue("CommunicationWebApiKey"));

        }



        public async Task<TResponse> DispatchRequest<TResponse, TProxyClient>
            (Expression<Func<TProxyClient, Task<SwaggerResponse<TResponse>>>> method)
        {
            MethodCallExpression methodBody = (MethodCallExpression)method.Body;

            TResponse response = default(TResponse);

            try
            {
                TProxyClient client = (TProxyClient)Activator.CreateInstance(typeof(TProxyClient), _communicationHttpClient);
                List<object> parameters = ResolveExpressionParameters(method);
                string body = ConvertToJson(parameters);

                SwaggerResponse<TResponse> result = await method.Compile().Invoke(client);

                Dictionary<string, IEnumerable<string>> headers = (Dictionary<string, IEnumerable<string>>)result.Headers;

                response = result.Result;
            }
            catch (Exception ex)
            {

            }
            return response;
        }



        public async Task DispatchRequest<TProxyClient>(Expression<Func<TProxyClient, Task<SwaggerResponse>>> method)
        {
            MethodCallExpression methodBody = (MethodCallExpression)method.Body;

            try
            {
                TProxyClient client = (TProxyClient)Activator.CreateInstance(typeof(TProxyClient), _communicationHttpClient);
                List<object> parameters = ResolveExpressionParameters(method);
                string body = ConvertToJson(parameters);

                SwaggerResponse result = await method.Compile().Invoke(client);

                Dictionary<string, IEnumerable<string>> headers = (Dictionary<string, IEnumerable<string>>)result.Headers;
            }
            catch (Exception ex)
            {

            }
        }

        private string ConvertToJson(List<object> parameters)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(parameters, settings);
        }




        private List<object> ResolveExpressionParameters<TResponse, TProxyClient>(Expression<Func<TProxyClient, Task<SwaggerResponse<TResponse>>>> expression)
        {
            List<object> parameters = new List<object>();
            MethodCallExpression call = expression.Body as MethodCallExpression;
            if (call == null)
            {
                throw new ArgumentException("Not a method call");
            }

            foreach (Expression argument in call.Arguments)
            {
                LambdaExpression lambda = Expression.Lambda(argument, expression.Parameters);
                Delegate d = lambda.Compile();
                object value = d.DynamicInvoke(new object[1]);
                parameters.Add(value);
            }

            return parameters;
        }



        private List<object> ResolveExpressionParameters<TProxyClient>(Expression<Func<TProxyClient, Task<SwaggerResponse>>> expression)
        {
            List<object> parameters = new List<object>();
            MethodCallExpression call = expression.Body as MethodCallExpression;
            if (call == null)
            {
                throw new ArgumentException("Not a method call");
            }

            foreach (Expression argument in call.Arguments)
            {
                LambdaExpression lambda = Expression.Lambda(argument, expression.Parameters);
                Delegate d = lambda.Compile();
                object value = d.DynamicInvoke(new object[1]);

                parameters.Add(value);
            }
            return parameters;
        }
    }
}
