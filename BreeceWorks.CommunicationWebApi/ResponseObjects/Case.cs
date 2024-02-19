using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.Shared.Enums;

namespace BreeceWorks.CommunicationWebApi.ResponseObjects
{
    public class Case
    {
        private CaseState _state;
        private CaseType _caseType;
        private Privacy? _privacy;
        private Privacy _tempPrivacy;
        private LanguagePreference? _languagePreference;
        private LanguagePreference _tempLanguagePreference;

        public Case()
        {
            caseData = new CaseData();
        }
        public String state
        {
            get
            {
                return _state.ToString();
            }
            set
            {
                Enum.TryParse(value, out _state);
            }
        }
        public CaseData caseData { get; set; }
        public String caseType {
            get
            {
                return _caseType.ToString();
            }
            set
            {
                Enum.TryParse(value, out _caseType);
            }
        }
        public String? businessName { get; set; }
        public Customer? customer { get; set; }
        public Operator? primaryContact { get; set; }
        public Operator? createdBy { get; set; }
        public String? privacy
        {
            get
            {
                if (_privacy == null)
                {
                    return null;
                }
                return _privacy.ToString();
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    _privacy = null;
                }
                else
                {
                    Enum.TryParse(value, out _tempPrivacy);
                    _privacy = _tempPrivacy;
                }
            }
        }
        public String? languagePreference
        {
            get
            {
                if (_languagePreference == null)
                {
                    return null;
                }
                return _languagePreference.ToString();
            }
            set
            {
                if (value == null)
                {
                    _languagePreference = null;
                }
                else
                {
                    Enum.TryParse(value, out _tempLanguagePreference);
                    _languagePreference = _tempLanguagePreference;
                }
            }
        }
        public Operator[]? secondaryOperators { get; set; }
        public Error[]? errors { get; set; }
    }
}
