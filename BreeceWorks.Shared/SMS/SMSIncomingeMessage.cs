using System;
using System.Collections.Generic;
using System.Text;

namespace BreeceWorks.Shared.SMS
{
    public class SMSIncomingeMessage
    {
        public SMSIncomingeMessage() 
        {
            attachmentUrls = new List<SMSAttachment>();
        }

        public String? messageID { get; set; }
        public String? toNumber { get; set; }
        public String? fromNumber { get; set; }
        public String? message { get; set; }
        public List<SMSAttachment>? attachmentUrls { get; set; }
        public String? initialStatus { get; set; }
        public String? errorMessage { get;set; }
    }
}
