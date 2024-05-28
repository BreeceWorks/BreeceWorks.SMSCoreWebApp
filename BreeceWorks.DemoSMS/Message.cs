namespace BreeceWorks.DemoSMS
{
    public class Message
    {
        public Message()
        {
            AttachmentURLs = new List<String>();
        }
        public String To { get; set; }
        public String From { get; set; }
        public String MessageContent { get; set; }
        public List<String> AttachmentURLs { get; set; }
    }
}
