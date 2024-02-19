namespace BreeceWorks.CommunicationWebApi.RequestObjects
{
    public class OperatorAssignRqst : OperatorBaseRqst
    {
        public String? mobile { get; set; }
        public Guid? id { get; set; }
    }
}
