using BreeceWorks.Shared.Services;
using BreeceWorks.Shared.SMS;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Text;

namespace BreeceWorks.DemoSMSSimulator.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IConfigureService _configureService;
        private readonly HttpClient _communicationHttpClient;

        public ChatHub(IConfigureService configureService, HttpClient communicationHttpClient)
        {
            _configureService = configureService;
            _communicationHttpClient = communicationHttpClient;
            _communicationHttpClient.BaseAddress = new Uri(_configureService.GetValue("BreeceWorks.SMSCoreWebApi"));
        }

        public async Task SendMessage(string from, string to, string message)
        {
            List<SMSAttachment> attachments = new List<SMSAttachment>();    
            await Clients.All.SendAsync("ReceiveMessage", from, to, message, attachments);
            SendMessageToApi(to, from, message);
        }

        private void SendMessageToApi(String to, String from, string messageToSend)
        {
            SMSIncomingeMessage sMSResponseMessage = new SMSIncomingeMessage()
            {
                fromNumber = from,
                toNumber = to,
                message = messageToSend,
                messageID = Guid.NewGuid().ToString(),
            };
            try
            {
                HttpResponseMessage httpResponse = _communicationHttpClient.PostAsJsonAsync("api/DemoSMS/Incoming", sMSResponseMessage).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Sent message: " + messageToSend + " to: " + to + " from: " + from);
        }
    }
}