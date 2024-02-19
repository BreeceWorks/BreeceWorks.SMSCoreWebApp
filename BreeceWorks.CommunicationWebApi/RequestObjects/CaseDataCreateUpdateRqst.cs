using BreeceWorks.CommunicationWebApi.Objects;

namespace BreeceWorks.CommunicationWebApi.RequestObjects
{
    public class CaseDataCreateUpdateRqst
    {
        public String claimNumber { get; set; }
        public String? dateOfLoss { get; set; }
        public String policyNumber { get; set; }
        public Int32? deductible { get; set; }
        public String? brand { get; set; }
        public LineOfBusinessRqst? lineOfBusiness { get; set; }
    }
}
