using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BreeceWorks.CommunicationWebApi.Hubs
{
    [Authorize(Policy = "CustomHubAuthorizatioPolicy")]

    public class CommunicationHub : Hub
    {
    }
}
