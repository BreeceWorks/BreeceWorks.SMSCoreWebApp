namespace BreeceWorks.CommunicationWebApi.RequestObjects
{
    public class CaseCreateRqst:CaseCreateUpdateBaseRqst
    {
        public String caseType { get; set; }
        public CustomerCreateUpdateRqst customer { get; set; }
    }
}
