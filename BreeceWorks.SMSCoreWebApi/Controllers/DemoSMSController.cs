using BreeceWorks.Shared;
using BreeceWorks.Shared.Services;
using BreeceWorks.Shared.SMS;
using BreeceWorks.SMSCoreWebApi.IControllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BreeceWorks.SMSCoreWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DemoSMSController : ControllerBase, ISMSController
    {

        private readonly IConfigureService _configureService;
        private readonly HttpClient _simulatorHttpClient;
        private readonly HttpClient _communicationHttpClient;

        public DemoSMSController(IConfigureService configureService, HttpClient simulatorHttpClient, HttpClient communicationHttpClient)
        {
            _configureService = configureService;
            _simulatorHttpClient = simulatorHttpClient;
            _communicationHttpClient = communicationHttpClient;
            _simulatorHttpClient.BaseAddress = new Uri(_configureService.GetValue("BreeceWorks.SMSSimulator"));
            _communicationHttpClient.BaseAddress = new Uri(_configureService.GetValue("BreeceWorks.CommunicationWebApi"));
            _communicationHttpClient.DefaultRequestHeaders.Add("x-api-key", _configureService.GetValue("CommunicationWebApiKey"));
        }

        [HttpPost, Route("[controller]/VerifyValidMobile/{phoneNumber}")]
        public async Task<MobileNumberValidationResponse> VerifyValidMobile(String phoneNumber)
        {
            //This is a simple simulator which assumes any number entered is valid
            return new MobileNumberValidationResponse()
            {
                IsValidMobile = true
            };
        }


        [HttpPost, Route("[controller]/Outgoing")]
        public async Task<SMSIncomingeMessage> Outgoing(SMSOutgoingMessage sMSMessage)
        {
            SMSIncomingeMessage sMSResponseMessage = new SMSIncomingeMessage()
            {
                attachmentUrls = sMSMessage.attachmentUrls,
                fromNumber = sMSMessage.fromNumber,
                toNumber = sMSMessage.toNumber,
                message = sMSMessage.message
            };
            try
            {
                HttpResponseMessage httpResponse = _simulatorHttpClient.PostAsJsonAsync("SignalR", sMSMessage).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
                string json = await httpResponse.Content.ReadAsStringAsync();
                SMSIncomingeMessage? responseMessage = JsonConvert.DeserializeObject<SMSIncomingeMessage>(json);
                if (responseMessage != null)
                {
                    sMSResponseMessage = responseMessage;
                }
            }
            catch (Exception ex)
            {
                sMSResponseMessage.errorMessage = ex.Message;
            }
            return sMSResponseMessage;
        }

        [HttpPost, Route("[controller]/Incoming")]
        public void Incoming(SMSIncomingeMessage sMSMessage)
        {
            try
            {
                HttpResponseMessage httpResponse = _communicationHttpClient.PostAsJsonAsync(Constants.URLTemplates.Incoming, sMSMessage).Result;
            }
            catch (Exception ex)
            {
                //log exceptions
            }
        }

        [HttpPost, Route("[controller]/sms_status_callback")]
        public void sms_status_callback(SMSMessageStatus sMSMessageStatus)
        {
            try
            {
                HttpResponseMessage httpResponse = _communicationHttpClient.PostAsJsonAsync(Constants.URLTemplates.StatusCallback, sMSMessageStatus).Result;
            }
            catch (Exception ex)
            {
                //log exceptions
            }
        }

        [HttpPost, Route("[controller]/CleanUpIncomingMessage")]
        public void CleanUpIncomingMessage(SMSIncomingeMessage sMSMessage)
        {
            
        }
    }
}
