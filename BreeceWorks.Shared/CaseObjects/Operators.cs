using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public class Operators
    {
        private System.Collections.Generic.List<Operator> _operators1;
        private System.Collections.Generic.List<Error> _errors;

        public System.Collections.Generic.List<Operator> Operators1
        {
            get { return _operators1; }

            set
            {
                if (_operators1 != value)
                {
                    _operators1 = value;
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

    }
}
