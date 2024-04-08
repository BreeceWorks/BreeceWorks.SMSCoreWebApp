namespace BreeceWorks.Shared.SMS
{
    public class SMSOutgoingMessage
    {
        public SMSOutgoingMessage() 
        { 
            attachmentUrls = new List<SMSAttachment>();
        }
        public String SMSProcessor { get; set; }
        public String toNumber { get; set; }
        public String fromNumber { get; set; }
        public String message { get; set; }
        public List<SMSAttachment> attachmentUrls { get; set; }

    }
}
