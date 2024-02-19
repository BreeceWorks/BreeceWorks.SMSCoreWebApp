using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public partial class PrimaryContactCaseCreateUpdateRqst
    {
        private string _email;
        private string _first;
        private string _last;

        public string Email
        {
            get { return _email; }

            set
            {
                if (_email != value)
                {
                    _email = value;
                }
            }
        }

        public string First
        {
            get { return _first; }

            set
            {
                if (_first != value)
                {
                    _first = value;
                }
            }
        }

        public string Last
        {
            get { return _last; }

            set
            {
                if (_last != value)
                {
                    _last = value;
                }
            }
        }

    }
}
