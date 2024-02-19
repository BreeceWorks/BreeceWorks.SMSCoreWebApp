namespace BreeceWorks.SMSCoreWebApi.Objects
{
    public class ClickSendMessage
    {
        public String? direction { get; set; }
        public Int32? date { get; set; }
        public String to { get; set; }
        public String body { get; set; }
        public String from { get; set; }
        public Int32? schedule { get; set; }
        public String message_id { get; set; }
        public Int32? message_parts { get; set; }
        public String? message_price { get; set; }
        public String? from_email { get; set; }
        public Int32? list_id { get; set; }
        public String custom_string { get; set; }
        public Int32? contact_id { get; set; }
        public Int32? user_id { get; set; }
        public Int32? subaccount_id { get; set; }
        public Boolean is_shared_system_number { get; set; }
        public String country { get; set; }
        public String? carrier { get; set; }
        public String? status { get;set; }
        public String? subject { get; set; }    
        public String? _media_file_url { get; set; }

    }
}
