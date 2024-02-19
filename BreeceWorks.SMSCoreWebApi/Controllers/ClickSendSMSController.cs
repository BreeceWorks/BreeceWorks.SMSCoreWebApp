using BreeceWorks.Shared;
using BreeceWorks.Shared.Services;
using BreeceWorks.Shared.SMS;
using BreeceWorks.SMSCoreWebApi.Objects;
using IO.ClickSend.ClickSend.Api;
using IO.ClickSend.ClickSend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreeceWorks.SMSCoreWebApi.Controllers
{
    //TODO: This has only been completed enough for me to do some testing.  It needs to finish being fleshed out to be integrated with the BreeceWorks.CommunicationWebApi
    [Route("api/")]
    [ApiController]
    public class ClickSendSMSController : ControllerBase
    {
        private readonly IConfigureService _configureService;
        private const string SavePath = @"\App_Data\";
        private readonly IWebHostEnvironment _env;
        private readonly HttpClient _httpClient;

        public ClickSendSMSController(IConfigureService configureService, IWebHostEnvironment webHostEnvironment, HttpClient httpClient)
        {
            _configureService = configureService ??
                throw new ArgumentNullException(nameof(configureService));
            _env = webHostEnvironment ?? throw new ArgumentNullException();
            _httpClient = httpClient ?? throw new ArgumentNullException();
            _httpClient.BaseAddress = new Uri(_configureService.GetValue("BreeceWorks.CommunicationWebApi"));
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _configureService.GetValue("CommunicationWebApiKey"));
        }

        [HttpPost, Route("[controller]/VerifyValidMobile/{phoneNumber}")]
        public async Task<MobileNumberValidationResponse> VerifyValidMobile(String phoneNumber)
        {
            //I have not yet found any way to do this through Click Send.
            return new MobileNumberValidationResponse()
            {
                IsValidMobile = true
            };
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
            String? response = null;
            try
            {
                var configuration = new IO.ClickSend.Client.Configuration()
                {
                    Username = _configureService.GetValue("ClickSend:Username"),
                    Password = _configureService.GetValue("ClickSend:Password")
                };

                if (sMSMessage.attachmentUrls != null && sMSMessage.attachmentUrls.Count > 0)
                {
                    var mmsApi = new MMSApi(configuration);
                    var listOfMessages = new List<MmsMessage>();
                    listOfMessages.Add(new MmsMessage(sMSMessage.toNumber, sMSMessage.message,
                        sMSMessage.message, sMSMessage.fromNumber, null, null, null, null, "BreeceWorks", null)
                    );
                    String? attachedurl = null;

                    for (int i = 0; i < listOfMessages.Count; i++)
                    {
                        attachedurl = sMSMessage.attachmentUrls[0].url;
                        var mmsCollection = new MmsMessageCollection(attachedurl, listOfMessages);
                        response = mmsApi.MmsSendPost(mmsCollection);
                    }

                }
                else
                {
                    var smsApi = new SMSApi(configuration);

                    var listOfSms = new List<SmsMessage>
                    {
                        new SmsMessage(
                            to: sMSMessage.toNumber,
                            from: sMSMessage.fromNumber,
                            body: sMSMessage.message,
                            source: "sdk",
                            customString: "BreeceWorks"
                            )
                    };

                    var smsCollection = new SmsMessageCollection(listOfSms);
                    response = smsApi.SmsSendPost(smsCollection);
                }

                if (response != null)
                {
                    ClickSendResponse? responseMessage = JsonConvert.DeserializeObject<ClickSendResponse>(response);
                    if (responseMessage != null)
                    {
                        if (responseMessage.data != null && responseMessage.data.messages != null && responseMessage.data.messages.Length > 0)
                        {
                            sMSResponseMessage.messageID = responseMessage.data.messages[0].message_id;
                            sMSResponseMessage.initialStatus = responseMessage.response_code;
                        }
                        else
                        {
                            sMSResponseMessage.errorMessage = responseMessage.response_msg;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                sMSResponseMessage.errorMessage += e.Message;
            }
            return sMSResponseMessage;
        }

        [HttpPost, Route("[controller]/Incoming")]
        public IActionResult Incoming(String token = "")
        {
            //TODO:  Add security per these recommendations from ClickSend
            //Security for inbound web-hooks - replies and delivery reports
            //
            //
            //Many clients ask us how the can ensure security when we push replies and delivery reports over HTTP to their web server.
            //Here are a few tips:
            //Use HTTPS
            //We recommend that you setup and use https on your server
            //Verify User ID in the Post Body
            //We post your user_id with every webhook. The user ID is unique to your account and fixed. You can check the body for this value.
            //
            //Use the custom_string parameter
            //When sending the SMS, you can supply a 'custom_string' parameter for each recipient (see the sms / send endpoint). We will pass this back with all replies.You can set this to anything and validate it when we post it back.
            //
            //Add a token to the request
            //When setting up the inbound SMS rule, you can add a query string token to the end of your URL.
            //
            //for example: https:// yourserver.com/incoming/sms.php?token=Fsk83jdiao2e
            //
            //By adding a token to the end of the URL that only you know, you can verify that the script is coming from us.
            //
            //Verify Our IP Address
            //We always post from the same pool of IP addresses.You can verify that it's coming from us.
            //
            //
            //


            IncomingMessage incomingMessage = new IncomingMessage()
            {
                body = Request.Form["body"],
                customstring = Request.Form["customstring"],
                custom_string = Request.Form["custom_string"],
                from = Request.Form["from"],
                to = Request.Form["to"],
                message = Request.Form["message"],
                message_id = Request.Form["message_id"],
                originalmessage = Request.Form["originalmessage"],
                originalmessageid = Request.Form["originalmessageid"],
                originalsenderid = Request.Form["originalsenderid"],
                original_body = Request.Form["original_body"],
                original_message_id = Request.Form["original_message_id"],
                sms = Request.Form["sms"],
                subaccount_id = Request.Form["subaccount_id"],
                timestamp = Request.Form["timestamp"],
                user_id = Request.Form["user_id"]
            };

            String message = String.Empty;
            String[] messageBlock = incomingMessage.message.Split("\n");
            if (messageBlock.Length > 0)
            {
                message = messageBlock[messageBlock.Length - 1];
            }
            else
            {
                message = messageBlock[0];
            }
            List<SMSAttachment> attachmentUrls = new List<SMSAttachment>();
            if (messageBlock.Length > 1)
            {
                for (int i = 0; i < messageBlock.Length -1; i++)
                {
                    attachmentUrls.Add(new SMSAttachment() { url = messageBlock[i], extension = GetExtension(messageBlock[i]), data = GetData(messageBlock[i]), name = GetFileName(messageBlock[i]) });
                    
                }
            }



            SMSIncomingeMessage sMSMessage = new SMSIncomingeMessage()
            {
                fromNumber = incomingMessage.from,
                toNumber = incomingMessage.to,
                message = message,
                messageID = incomingMessage.message_id,
                attachmentUrls = attachmentUrls
            };

            if (sMSMessage.fromNumber.IndexOf("+1") == 0)
            {
                sMSMessage.fromNumber = sMSMessage.fromNumber.Substring(2, sMSMessage.fromNumber.Length - 2);
            }

            if (sMSMessage.toNumber.IndexOf("+1") == 0)
            {
                sMSMessage.toNumber = sMSMessage.toNumber.Substring(2, sMSMessage.toNumber.Length - 2);
            }

            try
            {
                HttpResponseMessage httpResponse = _httpClient.PostAsJsonAsync(Constants.URLTemplates.Incoming, sMSMessage).Result;
            }
            catch (Exception e)
            {
                //log exceptions
            }

            return Ok();
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

        private string GetExtension(string url)
        {
            String fileExtension = String.Empty;
            try
            {
                string[] segmentArray = url.Split('/');

                string savedFileName = segmentArray[segmentArray.Length - 1].Substring(0, segmentArray[segmentArray.Length - 1].IndexOf("?X-Amz-Content-Sha256"));

                fileExtension = "." + savedFileName.Split('.')[1];
            }
            catch(Exception ex) 
            { 
            }

            return fileExtension;
        }

        private string GetFileName(string url)
        {
            String fileName = String.Empty;
            try
            {
                string[] segmentArray = url.Split('/');

                string savedFileName = segmentArray[segmentArray.Length - 1].Substring(0, segmentArray[segmentArray.Length - 1].IndexOf("?X-Amz-Content-Sha256"));

                fileName = savedFileName.Split('.')[0];
            }
            catch (Exception ex)
            {
            }

            return fileName;
        }

        private string GetMediaFileName(string mediaUrl)
        {
            string savedFileName = mediaUrl.Substring(0, mediaUrl.IndexOf("?X-Amz-Content-Sha256"));
            string contentRootPath = _env.ContentRootPath;

            string path = "";
            path = Path.Combine(SavePath + Path.GetFileName(savedFileName));
            path = _env.WebRootPath +
                // e.g. ~/App_Data/MExxxx.jpg
                SavePath +
                Path.GetFileName(savedFileName);


            return path;
        }


        //Save images sent by SMS
        private async Task SaveImages(int numMedia)
        {
            for (var i = 0; i < numMedia; i++)
            {
                var mediaUrl = Request.Form[$"MediaUrl{i}"];
                Trace.WriteLine(mediaUrl);
                var contentType = Request.Form[$"MediaContentType{i}"];

                var filePath = GetMediaFileName(mediaUrl, contentType);
                await DownloadUrlToFileAsync(mediaUrl, filePath);
            }
        }

        private string GetMediaFileName(string mediaUrl,
            string contentType)
        {

            return _env.WebRootPath +
                // e.g. ~/App_Data/MExxxx.jpg
                SavePath +
                Path.GetFileName(mediaUrl) +
                GetDefaultExtension(contentType);
        }

        private static async Task DownloadUrlToFileAsync(string mediaUrl,
            string filePath)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(mediaUrl);
                var httpStream = await response.Content.ReadAsStreamAsync();
                using (var fileStream = System.IO.File.Create(filePath))
                {
                    await httpStream.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }
            }
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




        [HttpPost, Route("[controller]/sms_status_callback")]
        public ActionResult sms_status_callback(string token = "")
        {
            //TODO:  Add security per these recommendations from ClickSend
            //Security for inbound web-hooks - replies and delivery reports
            //
            //
            //Many clients ask us how the can ensure security when we push replies and delivery reports over HTTP to their web server.
            //Here are a few tips:
            //Use HTTPS
            //We recommend that you setup and use https on your server
            //Verify User ID in the Post Body
            //We post your user_id with every webhook. The user ID is unique to your account and fixed. You can check the body for this value.
            //
            //Use the custom_string parameter
            //When sending the SMS, you can supply a 'custom_string' parameter for each recipient (see the sms / send endpoint). We will pass this back with all replies.You can set this to anything and validate it when we post it back.
            //
            //Add a token to the request
            //When setting up the inbound SMS rule, you can add a query string token to the end of your URL.
            //
            //for example: https:// yourserver.com/incoming/sms.php?token=Fsk83jdiao2e
            //
            //By adding a token to the end of the URL that only you know, you can verify that the script is coming from us.
            //
            //Verify Our IP Address
            //We always post from the same pool of IP addresses.You can verify that it's coming from us.
            //
            //
            //


            SMSStatusReport sMSStatusReport = new SMSStatusReport()
            {
                customstring = Request.Form["customstring"],
                custom_string = Request.Form["custom_string"],
                message_id = Request.Form["message_id"],
                subaccount_id = Request.Form["subaccount_id"],
                timestamp = Request.Form["timestamp"],
                user_id = Request.Form["user_id"],
                error_code = Request.Form["error_code"],
                error_text = Request.Form["error_text"],
                messageid = Request.Form["messageid"],
                message_type = Request.Form["message_type"],
                status = Request.Form["status"],
                status_code = Request.Form["status_code"],
                status_text = Request.Form["status_text"],
                timestamp_send = Request.Form["timestamp_send"]
            };


            //string[] keys = Request.Form.Keys.ToArray();
            //for (int i = 0; i < keys.Length; i++)
            //{
            //    Debug.WriteLine(keys[i] + ": " + Request.Form[keys[i]]);
            //}


            SMSMessageStatus sMSMessageStatus = new SMSMessageStatus()
            {
                messageID = sMSStatusReport.messageid,
                status = sMSStatusReport.status
            };
            try
            {
                HttpResponseMessage httpResponse = _httpClient.PostAsJsonAsync(Constants.URLTemplates.StatusCallback, sMSMessageStatus).Result;

            }
            catch (Exception ex)
            {
                //log exceptions
            }

            return Ok();


        }


    }

    public class IncomingMessage
    {
        public string body { get; set; }
        public string to { get; set; }
        public string from { get; set; }
        public string originalsenderid { get; set; }
        public string message { get; set; }
        public string sms { get; set; }
        public string custom_string { get; set; }
        public string original_message_id { get; set; }
        public string originalmessageid { get; set; }
        public string customstring { get; set; }
        public string originalmessage { get; set; }
        public string user_id { get; set; }
        public string subaccount_id { get; set; }
        public string original_body { get; set; }
        public string timestamp { get; set; }
        public string message_id { get; set; }
    }

    public class SMSStatusReport
    {
        public String error_code { get; set; }
        public String error_text { get; set; }
        public String custom_string { get; set; }
        public String timestamp_send { get; set; }
        public String message_id { get; set; }
        public String user_id { get; set; }
        public String timestamp { get; set; }
        public String messageid { get; set; }
        public String customstring { get; set; }
        public String status_text { get; set; }
        public String subaccount_id { get; set; }
        public String message_type { get; set; }
        public String status { get; set; }
        public String status_code { get; set; }
    }
}
