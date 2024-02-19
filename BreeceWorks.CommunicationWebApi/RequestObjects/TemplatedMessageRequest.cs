namespace BreeceWorks.CommunicationWebApi.RequestObjects
{
    public class TemplatedMessageRequest
    {
        public Dictionary<string, string> templateValues { get; set; }
        public String caseId { get; set; }
        public String referenceID { get; set; }
        public object source { get;set; }
    }
}
