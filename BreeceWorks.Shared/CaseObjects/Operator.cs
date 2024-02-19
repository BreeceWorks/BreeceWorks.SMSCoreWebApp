using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public class Operator
    {
        private System.Guid? _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _officeNumber;
        private string _identityProvider;
        private System.Collections.Generic.List<string> _roles;
        private string _title;
        private System.Collections.Generic.List<Error> _errors;

        public System.Guid? Id
        {
            get { return _id; }

            set
            {
                if (_id != value)
                {
                    _id = value;
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }

            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }

            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                }
            }
        }

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

        public string PhoneNumber
        {
            get { return _phoneNumber; }

            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                }
            }
        }

        public string OfficeNumber
        {
            get { return _officeNumber; }

            set
            {
                if (_officeNumber != value)
                {
                    _officeNumber = value;
                }
            }
        }

        public string IdentityProvider
        {
            get { return _identityProvider; }

            set
            {
                if (_identityProvider != value)
                {
                    _identityProvider = value;
                }
            }
        }

        public System.Collections.Generic.List<string> Roles
        {
            get { return _roles; }

            set
            {
                if (_roles != value)
                {
                    _roles = value;
                }
            }
        }

        public string Title
        {
            get { return _title; }

            set
            {
                if (_title != value)
                {
                    _title = value;
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
