using BreeceWorks.CommunicationWebApi.Adapters.Interfaces;
using BreeceWorks.Shared;
using BreeceWorks.Shared.Services;
using BreeceWorks.Shared.SMS;
using Newtonsoft.Json;

namespace BreeceWorks.CommunicationWebApi.Adapters.Implementations
{
    public class SMSAdapter : ISMSAdapter
    {
        private readonly IConfigureService _configureService;
        private readonly HttpClient _httpClient;

        public SMSAdapter(IConfigureService configureService, HttpClient httpClient)
        {
            _configureService = configureService;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configureService.GetValue("BreeceWorks.SMSCoreWebApi"));
        }

        public async Task<SMSIncomingeMessage> SendSMS(SMSOutgoingMessage message)
        {
            SMSIncomingeMessage responseMessage = new SMSIncomingeMessage()
            {
                attachmentUrls = message.attachmentUrls,
                fromNumber = message.fromNumber,
                toNumber = message.toNumber,
                message = message.message
            };

            //validate number is mobile
            HttpResponseMessage httpResponse = _httpClient.PostAsJsonAsync(String.Format(Constants.URLTemplates.VerifyValidMobile, message.SMSProcessor, message.toNumber), message).Result;
            string json = await httpResponse.Content.ReadAsStringAsync();

            MobileNumberValidationResponse validationResponse = JsonConvert.DeserializeObject<MobileNumberValidationResponse>(json);
            if (!validationResponse.IsValidMobile)
            {
                responseMessage.errorMessage = validationResponse.ErrorMessage;
                return responseMessage;
            }


            //send sms
            httpResponse = _httpClient.PostAsJsonAsync(String.Format(Constants.URLTemplates.Outgoing, message.SMSProcessor), message).Result;
            json = await httpResponse.Content.ReadAsStringAsync();
            SMSIncomingeMessage? response = JsonConvert.DeserializeObject<SMSIncomingeMessage>(json);
            if (response != null)
            {
                responseMessage = response;
            }
            return responseMessage;

        }
    }
}
