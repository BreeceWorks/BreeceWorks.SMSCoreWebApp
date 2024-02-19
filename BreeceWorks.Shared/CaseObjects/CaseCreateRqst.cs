using BreeceWorks.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public partial class CaseCreateRqst
    {
        private CaseDataCreateUpdateRqst _caseData;
        private OperatorBaseRqst _createdBy;
        private string _businessName;
        private PrimaryContactCaseCreateUpdateRqst _primaryContact;
        private Privacy? _privacy;
        private LanguagePreference? _languagePreference;
        private System.Collections.Generic.List<SecondaryOperatorCreateUpdateRqst> _secondaryOperators;
        private string _caseType;
        private CustomerCreateUpdateRqst _customer;

        public CaseDataCreateUpdateRqst CaseData
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

        public OperatorBaseRqst CreatedBy
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

        public PrimaryContactCaseCreateUpdateRqst PrimaryContact
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

        public Privacy? Privacy
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

        public LanguagePreference? LanguagePreference
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

        public System.Collections.Generic.List<SecondaryOperatorCreateUpdateRqst> SecondaryOperators
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

        public CustomerCreateUpdateRqst Customer
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
    }
}
