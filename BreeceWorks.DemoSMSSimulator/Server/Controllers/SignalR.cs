using BreeceWorks.DemoSMSSimulator.Server.Hubs;
using BreeceWorks.Shared;
using BreeceWorks.Shared.Services;
using BreeceWorks.Shared.SMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Text;

namespace BreeceWorks.DemoSMSSimulator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class SignalRController : ControllerBase
    {
        private readonly IConfigureService _configureService;
        private readonly HttpClient _communicationHttpClient;

        private IHubContext<ChatHub> _hubContext { get; set; }

        public SignalRController(IHubContext<ChatHub> hubcontext, IConfigureService configureService, HttpClient communicationHttpClient)
        {
            _hubContext = hubcontext;
            _configureService = configureService;
            _communicationHttpClient = communicationHttpClient;
            _communicationHttpClient.BaseAddress = new Uri(_configureService.GetValue("BreeceWorks.SMSCoreWebApi"));
        }


        [HttpPost]
        public async Task<SMSIncomingeMessage> SendMessage([FromBody] SMSOutgoingMessage sMSMessage)
        {
            await this._hubContext.Clients.All.SendAsync("ReceiveMessage", sMSMessage.fromNumber, sMSMessage.toNumber, sMSMessage.message, sMSMessage.attachmentUrls);

            SMSIncomingeMessage sMSResponseMessage = new SMSIncomingeMessage()
            {
                attachmentUrls = sMSMessage.attachmentUrls,
                fromNumber = sMSMessage.fromNumber,
                toNumber = sMSMessage.toNumber,
                message = sMSMessage.message,
                initialStatus = Constants.MessageStatus.DELIVERED
            };
            sMSResponseMessage.messageID = Guid.NewGuid().ToString();
            try
            {
                SMSMessageStatus sMSMessageStatus = new SMSMessageStatus() { messageID = sMSResponseMessage.messageID, status = Constants.MessageStatus.DELIVERED };
                HttpResponseMessage httpResponse = _communicationHttpClient.PostAsJsonAsync("api/DemoSMS/sms_status_callback", sMSMessageStatus).Result;
            }
            catch (Exception ex)
            {
                sMSResponseMessage.errorMessage = ex.Message;
            }
            return sMSResponseMessage;
        }
    }
}
