using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public class LineOfBusinessRqst
    {
        private string _type;
        private string _subType;

        public string Type
        {
            get { return _type; }

            set
            {
                if (_type != value)
                {
                    _type = value;
                }
            }
        }

        public string SubType
        {
            get { return _subType; }

            set
            {
                if (_subType != value)
                {
                    _subType = value;
                }
            }
        }

    }
}
