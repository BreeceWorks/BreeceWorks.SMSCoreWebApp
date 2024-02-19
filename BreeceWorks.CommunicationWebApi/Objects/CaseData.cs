namespace BreeceWorks.CommunicationWebApi.Objects
{
    public class CaseData
    {
        public Guid Id { get; set; }
        public Boolean Archived { get; set; }
        public String ClaimNumber { get; set; }
        public String? DateOfLoss { get; set; }
        public String PolicyNumber { get; set; }
        public Int32? Deductible { get; set; }
        public String? Brand { get; set; }
        public LineOfBusiness? LineOfBusiness { get; set; }

    }
}
