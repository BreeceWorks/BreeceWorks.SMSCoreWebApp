using System;
using System.Collections.Generic;
using System.Text;

namespace BreeceWorks.SMSLibrary
{
    public class MobileNumberValidationResponse
    {
        public Boolean IsValidMobile { get; set; }
        public String ErrorMessage { get; set; }
    }
}
