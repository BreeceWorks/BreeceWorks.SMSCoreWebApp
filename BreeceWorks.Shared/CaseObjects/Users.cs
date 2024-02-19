using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BreeceWorks.Shared.CaseObjects
{
    public class Users
    {
        private List<Customer> _customers;
        private List<Error> _errors;

        public List<Customer> Customers
        {
            get { return _customers; }

            set
            {
                if (_customers != value)
                {
                    _customers = value;
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
