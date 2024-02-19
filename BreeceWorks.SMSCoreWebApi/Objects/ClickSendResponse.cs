namespace BreeceWorks.SMSCoreWebApi.Objects
{
    public class ClickSendResponse
    {
        public String http_code { get; set; }
        public String response_code { get; set; }
        public String response_msg { get; set; }
        public ClickSendResponseData data { get; set; }
    }
}
