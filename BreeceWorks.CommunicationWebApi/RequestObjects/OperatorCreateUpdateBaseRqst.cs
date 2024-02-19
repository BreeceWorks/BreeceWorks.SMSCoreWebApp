namespace BreeceWorks.CommunicationWebApi.RequestObjects
{
    public class OperatorCreateUpdateBaseRqst
    {
        public String firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public String mobile { get; set; }
        public String? dfficeNumber { get; set; }
        public string[]? roles { get; set; }
        public String? title { get; set; }


    }
}
