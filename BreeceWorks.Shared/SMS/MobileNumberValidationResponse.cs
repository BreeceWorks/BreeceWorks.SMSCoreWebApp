using System;
using System.Collections.Generic;
using System.Text;

namespace BreeceWorks.Shared.SMS
{
    public class MobileNumberValidationResponse
    {
        public Boolean IsValidMobile { get; set; }
        public String ErrorMessage { get; set; }
    }
}
