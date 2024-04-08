namespace BreeceWorks.Shared
{
    public static class Constants
    {
        public static class ErrorMessages
        {
            public const String TemplateNotFound = "Template not found";
            public const String PhoneIsLandLine = "Phone is land line";
            public const String PhoneIsVoip = "Phone is VOIP";
            public const String InvalidPhoneNumber = "Invalid phone number";
            public const String ClaimExists = "ClaimExists";
            public const String OperatorsNotFound = "No Operators Were Found.";
            public const String CustomerNotOptedIn = "The customer has not opted in to receiving messages";
        }
        public static class MessageTemplates
        {
            public const String Welcome = "WELCOME";
            public const String OPTED_IN_RESPONSE = "OPTED_IN_RESPONSE";
            public const String OPTED_OUT_RESPONSE = "OPTED_OUT_RESPONSE";
            public const String PRIMARY_CONTACT_ASSIGNED = "PRIMARY_CONTACT_ASSIGNED";
            public const String PRIMARY_CONTACT_UNASSIGNED = "PRIMARY_CONTACT_UNASSIGNED";
        }

        public static class MessageStatus
        {
            public const String UNDELIVERED = "UNDELIVERED";
            public const String SENT = "SENT";
            public const String DELIVERED = "DELIVERED";
        }

        public static List<String> imageExtenstions = new List<String>() { "jpg", ".jpg", "gif", ".gif", "jpeg", ".jpeg", "png", ".png", "bmp", ".bmp" };
        public static List<String> optKeyWords = new List<string>() { "YES", "ACCEPT", "STOP", "START" };

        public static class OptKeyWords
        {
            public const String YES = "YES";
            public const String ACCEPT = "ACCEPT";
            public const String STOP = "STOP";
            public const String START = "START";
        }

        public static class OptStatus
        {
            public const String REQUESTED = "REQUESTED";
            public const String OPTED_IN = "OPTED_IN";
            public const String OPTED_OUT = "OPTED_OUT";            
        }

        public static class URLTemplates
        {
            public const String VerifyValidMobile = "/api/{0}/VerifyValidMobile/{1}";
            public const String Outgoing = "/api/{0}/Outgoing";
            public const String Incoming = "/api/case/actions/incoming-message-webhook";
            public const String StatusCallback = "/api/case/actions/sms_status_callback";
            public const String AttachmentDownloadURL = "/api/SMSAttachment/attachment-download/{0}";
            public const String AttachmentUploadURL = "/api/SMSAttachment/attachment-upload";
        }

        public const int MAX_FILESIZE = 50000 * 1024; // 50 MB

    }
}
