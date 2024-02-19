using BreeceWorks.Shared;
using BreeceWorks.Shared.Services;
using BreeceWorks.Shared.SMS;
using BreeceWorks.SMSCoreWebApi.Objects;
using BreeceWorks.SMSCoreWebApi.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Text.Json;
using Twilio;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.Base;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Api.V2010.Account.Message;
using Twilio.Rest.Lookups.V2;
using Twilio.TwiML;

namespace BreeceWorks.SMSCoreWebApi.Controllers
{
    //TODO: This has only been completed enough for me to do some testing.  It needs to finish being fleshed out to be integrated with the BreeceWorks.CommunicationWebApi
    [Route("api/")]
    [ApiController]
    public class TwilioSMSController : TwilioController
    {
        private IWebHostEnvironment _env;
        private readonly IConfigureService _configureService;
        private readonly HttpClient _httpClient;


        public TwilioSMSController(IWebHostEnvironment env, IConfigureService configureService, HttpClient httpClient)
        {
            _env = env;
            //_configuration = configuration;
            _configureService = configureService ??
                throw new ArgumentNullException(nameof(configureService));
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configureService.GetValue("BreeceWorks.CommunicationWebApi"));
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _configureService.GetValue("CommunicationWebApiKey"));
        }

        [HttpPost, Route("[controller]/VerifyValidMobile/{phoneNumber}")]
        public async Task<MobileNumberValidationResponse> VerifyValidMobile(String phoneNumber)
        {
            String responseMessage = String.Empty;

            String authToken = _configureService.GetValue("Twilio:AuthToken");
            string accountSid = _configureService.GetValue("Twilio:Client:AccountSid");

            TwilioClient.Init(accountSid, authToken);

            var lookupPhoneNumber = PhoneNumberResource.Fetch(
                fields: "line_type_intelligence",
                pathPhoneNumber: phoneNumber
            );

            responseMessage = HandleLookupPhoneNumber(lookupPhoneNumber);


            if (!String.IsNullOrEmpty(responseMessage))
            {
                return new MobileNumberValidationResponse()
                {
                    IsValidMobile = false,
                    ErrorMessage = responseMessage
                };
            }
            return new MobileNumberValidationResponse() { IsValidMobile = true, ErrorMessage = responseMessage };
        }

        [HttpPost, Route("[controller]/Outgoing")]
        public SMSIncomingeMessage Outgoing(SMSOutgoingMessage sMSMessage)
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
                String? authToken = _configureService.GetValue("Twilio:AuthToken");
                string? accountSid = _configureService.GetValue("Twilio:Client:AccountSid");
                String statusCallBackurl = _configureService.GetValue("Twilio:StatusCallbackUrl");




                TwilioClient.Init(accountSid, authToken);
                List<Uri> urls = new List<Uri>();
                foreach (SMSAttachment urlString in  sMSMessage.attachmentUrls)
                {

                    //urls.Add(new Uri("https://api.twilio.com/2010-04-01/Accounts/AC031d68f58ccf94e0cc25766fcd461caf/Messages/MMdf5fddca1a707bb2a18287af80df1ae8/Media/ME266384a7c248c6d9e4f8f3852f5e784b"));
                    urls.Add(new Uri(urlString.url));
                }
                MessageResource sentMessage = MessageResource.Create(
                body: sMSResponseMessage.message,
                from: new Twilio.Types.PhoneNumber(sMSMessage.fromNumber),
                to: new Twilio.Types.PhoneNumber(sMSMessage.toNumber),
                statusCallback: !String.IsNullOrEmpty(statusCallBackurl) ? new Uri(statusCallBackurl) : null,
                pathAccountSid: null,
                messagingServiceSid: null,
                //messagingServiceSid: messagingServiceSID,
                mediaUrl: urls,
                applicationSid: null,
                maxPrice: null,
                provideFeedback: null,
                attempt: null,
                validityPeriod: null,
                forceDelivery: null,
                contentRetention: null,
                addressRetention: null,
                smartEncoded: null,
                persistentAction: null,
                shortenUrls: null,
                scheduleType: null,
                sendAt: null,
                sendAsMms: null,
                contentSid: null,
                contentVariables: null
                );
                sMSResponseMessage.messageID = sentMessage.Sid;

                if (!String.IsNullOrEmpty(sentMessage.NumMedia) && Int64.Parse(sentMessage.NumMedia) > 0)
                {

                }
            }
            catch ( Exception ex )
            {
                sMSResponseMessage.errorMessage += ex.Message;
            }
            return sMSResponseMessage;
        }


        private const string SavePath = @"\App_Data\";
        [HttpPost, Route("[controller]/Incoming")]
        [ValidateTwilioRequest]
        public async Task<TwiMLResult> Incoming([FromForm] SmsRequest request, [FromForm] int numMedia)
        {
            request = RemoveInternational(request);

            List<SMSAttachment> urlList = new List<SMSAttachment>();
            if (numMedia > 0)
            {
                for (var i = 0; i < numMedia; i++)
                {
                    var mediaUrl = Request.Form[$"MediaUrl{i}"];
                    var contentType = Request.Form[$"MediaContentType{i}"];
                    urlList.Add(new SMSAttachment() { url = mediaUrl, extension = GetDefaultExtension(contentType), data = GetData(mediaUrl), name = GetFileName(mediaUrl) });
                }
            }


            SMSIncomingeMessage sMSMessage = new SMSIncomingeMessage()
            {
                fromNumber = request.From,
                toNumber = request.To,
                message = request.Body,
                messageID = request.SmsSid,
                attachmentUrls = urlList
            };

            try
            {
                HttpResponseMessage httpResponse = _httpClient.PostAsJsonAsync(Constants.URLTemplates.Incoming, sMSMessage).Result;
                String? authToken = _configureService.GetValue("Twilio:AuthToken");
                String? accountSid = _configureService.GetValue("Twilio:Client:AccountSid");

                TwilioClient.Init(accountSid, authToken);

                if (numMedia > 0)
                {
                    List<String> mediaSids = new List<String>();
                    ResourceSet<MediaResource> media = MediaResource.Read(
                            pathMessageSid: request.SmsSid,
                            limit: numMedia
                        );

                    foreach (MediaResource record in media)
                    {
                        mediaSids.Add(record.Sid);
                    }
                    foreach (String sid in mediaSids)
                    {
                        MediaResource.Delete(
                            pathMessageSid: request.SmsSid,
                            pathSid: sid
                        );
                    }
                }
                MessageResource.Delete(pathSid: request.SmsSid);
            }
            catch (Exception ex)
            {
                //log exceptions
            }

            //Twilio needs this response
            var response = new MessagingResponse();
            return TwiML(response);
        }

        private SmsRequest RemoveInternational(SmsRequest request)
        {
            if (request.To.IndexOf("+1") == 0)
            {
                request.To = request.To.Remove(0, 2);
            }
            if (request.From.IndexOf("+1") == 0)
            {
                request.From = request.From.Remove(0, 2);
            }
            return request;
        }

        private byte[] GetData(string url)
        {
            Byte[] messageBytes;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var httpStream = response.Content.ReadAsStream();

                using (BinaryReader br = new BinaryReader(httpStream))
                {
                    messageBytes = br.ReadBytes((Int32)httpStream.Length);
                }
            }
            return messageBytes;
        }

        private string GetFileName(string url)
        {
            String fileName = String.Empty;
            try
            {
                string[] segmentArray = url.Split('/');

                fileName = segmentArray[segmentArray.Length - 1];
            }
            catch (Exception ex)
            {
            }

            return fileName;
        }

        [HttpPost, Route("[controller]/sms_status_callback")]
        [ValidateTwilioRequest]
        public TwiMLResult sms_status_callback([FromForm] SmsRequest request)
        {
            SMSMessageStatus sMSMessageStatus = new SMSMessageStatus()
            {
                messageID = request.SmsSid,
                status = request.MessageStatus
            };
            try
            {
                HttpResponseMessage httpResponse = _httpClient.PostAsJsonAsync(Constants.URLTemplates.StatusCallback, sMSMessageStatus).Result;
                if (request.MessageStatus.ToUpper() == Constants.MessageStatus.DELIVERED)
                {
                    String? authToken = _configureService.GetValue("Twilio:AuthToken");
                    String? accountSid = _configureService.GetValue("Twilio:Client:AccountSid");

                    TwilioClient.Init(accountSid, authToken);
                    MessageResource.Delete(pathSid: request.SmsSid);
                }
            }
            catch (Exception ex)
            {
                //log exceptions
            }

            var response = new MessagingResponse();
            return TwiML(response);
        }






        private String HandleLookupPhoneNumber(PhoneNumberResource lookupPhoneNumber)
        {
            String responseMessage = String.Empty;
            responseMessage = HandleValidValue(lookupPhoneNumber);
            if (String.IsNullOrEmpty(responseMessage))
            {
                responseMessage = HandlePhoneNumberType(lookupPhoneNumber);
            }
            return responseMessage;
        }

        private string HandlePhoneNumberType(PhoneNumberResource lookupPhoneNumber)
        {
            if (lookupPhoneNumber == null
                            || lookupPhoneNumber.LineTypeIntelligence == null)
            {
                // if invalid values returned or phone number is invalid
                return Constants.ErrorMessages.InvalidPhoneNumber;
            }
            else
            {
                LineTypeIntelligence? lineTypeIntelligence =
                JsonSerializer.Deserialize<LineTypeIntelligence>(lookupPhoneNumber.LineTypeIntelligence.ToString());

                switch (lineTypeIntelligence.type)
                {
                    case "landline":
                        return Constants.ErrorMessages.PhoneIsLandLine;
                    case "nonFixedVoip":
                        return Constants.ErrorMessages.PhoneIsVoip;
                    default:
                        break;
                }

            }

            return String.Empty;
        }

        private static string HandleValidValue(PhoneNumberResource lookupPhoneNumber)
        {
            if (lookupPhoneNumber == null
                            || !lookupPhoneNumber.Valid.HasValue
                            || !lookupPhoneNumber.Valid.Value)
            {
                // if invalid values returned or phone number is invalid
                return "Invalid phone number";
            }

            return String.Empty;
        }

        public static string GetDefaultExtension(string mimeType)
        {
            // NOTE: This implementation is Windows specific (uses Registry)
            // Platform independent way might be to download a known list of
            // mime type mappings like: http://bit.ly/2gJYKO0
            var key = Registry.ClassesRoot.OpenSubKey(
                @"MIME\Database\Content Type\" + mimeType, false);
            var ext = key?.GetValue("Extension", null)?.ToString();
            return ext ?? "application/octet-stream";
        }
    }
}
