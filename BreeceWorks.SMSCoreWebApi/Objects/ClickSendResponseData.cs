namespace BreeceWorks.SMSCoreWebApi.Objects
{
    public class ClickSendResponseData
    {
        public float total_price { get; set; }
        public Int32 total_count { get; set; }
        public Int32 queued_count { get; set; }
        public ClickSendMessage[] messages { get; set; }
        public ClickSendCurrency _currency { get; set; }
    }
}
