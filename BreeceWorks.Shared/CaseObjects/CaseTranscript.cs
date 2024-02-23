namespace BreeceWorks.Shared.CaseObjects
{
    public class CaseTranscript
    {
        private string _state;
        private CaseData _caseData;
        private string _caseType;
        private string _businessName;
        private Customer _customer;
        private Operator _primaryContact;
        private Operator _createdBy;
        private string _privacy;
        private string _languagePreference;
        private System.Collections.Generic.List<Operator> _secondaryOperators;
        private System.Collections.Generic.List<Error> _errors;
        private System.Collections.Generic.List<Message> _messages;

        public string State
        {
            get { return _state; }

            set
            {
                if (_state != value)
                {
                    _state = value;
                }
            }
        }

        public CaseData CaseData
        {
            get { return _caseData; }

            set
            {
                if (_caseData != value)
                {
                    _caseData = value;
                }
            }
        }

        public string CaseType
        {
            get { return _caseType; }

            set
            {
                if (_caseType != value)
                {
                    _caseType = value;
                }
            }
        }

        public string BusinessName
        {
            get { return _businessName; }

            set
            {
                if (_businessName != value)
                {
                    _businessName = value;
                }
            }
        }

        public Customer Customer
        {
            get { return _customer; }

            set
            {
                if (_customer != value)
                {
                    _customer = value;
                }
            }
        }

        public Operator PrimaryContact
        {
            get { return _primaryContact; }

            set
            {
                if (_primaryContact != value)
                {
                    _primaryContact = value;
                }
            }
        }

        public Operator CreatedBy
        {
            get { return _createdBy; }

            set
            {
                if (_createdBy != value)
                {
                    _createdBy = value;
                }
            }
        }

        public string Privacy
        {
            get { return _privacy; }

            set
            {
                if (_privacy != value)
                {
                    _privacy = value;
                }
            }
        }

        public string LanguagePreference
        {
            get { return _languagePreference; }

            set
            {
                if (_languagePreference != value)
                {
                    _languagePreference = value;
                }
            }
        }

        public System.Collections.Generic.List<Operator> SecondaryOperators
        {
            get { return _secondaryOperators; }

            set
            {
                if (_secondaryOperators != value)
                {
                    _secondaryOperators = value;
                }
            }
        }

        public System.Collections.Generic.List<Error> Errors
        {
            get { return _errors; }

            set
            {
                if (_errors != value)
                {
                    _errors = value;
                }
            }
        }

        public System.Collections.Generic.List<Message> Messages
        {
            get { return _messages; }

            set
            {
                if (_messages != value)
                {
                    _messages = value;
                }
            }
        }

    }
}
