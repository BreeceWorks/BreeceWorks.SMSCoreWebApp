using System;
using System.Collections.Generic;
using System.Text;

namespace BreeceWorks.Shared.SMS
{
    public class SMSAttachment
    {
        public String url { get; set; } 
        public String name { get; set; }
        public String extension { get; set; }
        public byte[] data { get; set; }
    }
}
