using System;
using System.Collections.Generic;
using System.Text;

namespace BreeceWorks.Shared.SMS
{
    public class SMSOutgoingCommunication
    {
        public SMSOutgoingCommunication()
        {
            attachmentIDs = new List<String>();
        }
        public String caseId { get; set; }
        public String message { get; set; }
        public object source { get; set; }
        public List<String> attachmentIDs { get; set; }

    }
}
