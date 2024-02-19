using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public class CaseDataCreateUpdateRqst
    {
        private string _claimNumber;
        private string _dateOfLoss;
        private string _policyNumber;
        private int? _deductible;
        private string _brand;
        private LineOfBusinessRqst _lineOfBusiness;

        public string ClaimNumber
        {
            get { return _claimNumber; }

            set
            {
                if (_claimNumber != value)
                {
                    _claimNumber = value;
                }
            }
        }

        public string DateOfLoss
        {
            get { return _dateOfLoss; }

            set
            {
                if (_dateOfLoss != value)
                {
                    _dateOfLoss = value;
                }
            }
        }

        public string PolicyNumber
        {
            get { return _policyNumber; }

            set
            {
                if (_policyNumber != value)
                {
                    _policyNumber = value;
                }
            }
        }

        public int? Deductible
        {
            get { return _deductible; }

            set
            {
                if (_deductible != value)
                {
                    _deductible = value;
                }
            }
        }

        public string Brand
        {
            get { return _brand; }

            set
            {
                if (_brand != value)
                {
                    _brand = value;
                }
            }
        }

        public LineOfBusinessRqst LineOfBusiness
        {
            get { return _lineOfBusiness; }

            set
            {
                if (_lineOfBusiness != value)
                {
                    _lineOfBusiness = value;
                }
            }
        }

    }
}
