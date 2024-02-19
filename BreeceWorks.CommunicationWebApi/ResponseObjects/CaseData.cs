using BreeceWorks.CommunicationWebApi.Objects;
using System.Runtime.InteropServices;

namespace BreeceWorks.CommunicationWebApi.ResponseObjects
{
    public class CaseData
    {
        public Guid Id { get; set; }
        public String claimNumber { get; set; }
        public String? dateOfLoss { get; set; }
        public String policyNumber { get; set; }
        public Int32? deductible { get; set; }
        public String? brand { get; set; }
        public LineOfBusiness? lineOfBusiness { get; set; }
        public Boolean archived { get; internal set; }
    }
}
