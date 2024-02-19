using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.Shared.Enums;

namespace BreeceWorks.CommunicationWebApi.RequestObjects
{
    public class CaseCreateUpdateBaseRqst
    {
        public CaseDataCreateUpdateRqst caseData { get; set; }
        public OperatorBaseRqst? createdBy { get; set; }
        public String? businessName { get; set; }
        public PrimaryContactCaseCreateUpdateRqst? primaryContact { get; set; }
        public Privacy? privacy { get; set; }
        public LanguagePreference? languagePreference { get; set; }
        public SecondaryOperatorCreateUpdateRqst[]? secondaryOperators { get; set; }
    }
}
