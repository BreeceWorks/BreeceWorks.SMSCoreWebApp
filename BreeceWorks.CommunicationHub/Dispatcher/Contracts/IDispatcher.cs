using BreeceWorks.CommunicationHub.Dispatcher.Proxies;
using System.Linq.Expressions;

namespace BreeceWorks.CommunicationHub.Dispatcher.Contracts
{
    public interface IDispatcher

    {

        Task<TResponse> DispatchRequest<TResponse, TProxyClient>

            (Expression<Func<TProxyClient, Task<SwaggerResponse<TResponse>>>> method);



        Task DispatchRequest<TProxyClient>(Expression<Func<TProxyClient, Task<SwaggerResponse>>> method);

    }
}
