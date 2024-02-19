using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public class ActiveCases
    {
        private System.Collections.Generic.List<Case> _cases;
        private System.Collections.Generic.List<Error> _errors;

        public System.Collections.Generic.List<Case> Cases
        {
            get { return _cases; }

            set
            {
                if (_cases != value)
                {
                    _cases = value;
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
