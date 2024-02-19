using BreeceWorks.Shared.Enums;

namespace BreeceWorks.CommunicationWebApi.Objects
{
    public class Case
    {
        public CaseData CaseData { get; set; }
        public CaseState State { get; set; }
        public CaseType CaseType { get; set; }
        public Customer Customer { get; set; }
        public Operator? PrimaryContact { get; set; }
        public Operator? CreatedBy { get; set; }
        public DateTime CreateTime { get; set; }
        public String ReferenceId { get; set; }
        public Privacy? Privacy { get; set; }
        public String? SMSNumber { get; set; }
        public String? BusinessName { get; set; }
        public LanguagePreference? LanguagePreference { get; set; }

        public Operator[]? SecondaryOperators { get; set; }

    }
}
