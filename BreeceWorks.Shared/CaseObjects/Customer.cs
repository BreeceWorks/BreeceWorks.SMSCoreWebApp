using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public class Customer 
    {
        private System.Guid? _id;
        private string _first;
        private string _last;
        private string _email;
        private string _mobile;
        private string _role;
        private bool? _optStatus;
        private string _optStatusDetail;

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

        public string Mobile
        {
            get { return _mobile; }

            set
            {
                if (_mobile != value)
                {
                    _mobile = value;
                }
            }
        }

        public string Role
        {
            get { return _role; }

            set
            {
                if (_role != value)
                {
                    _role = value;
                }
            }
        }

        public bool? OptStatus
        {
            get { return _optStatus; }

            set
            {
                if (_optStatus != value)
                {
                    _optStatus = value;
                }
            }
        }

        public string OptStatusDetail
        {
            get { return _optStatusDetail; }

            set
            {
                if (_optStatusDetail != value)
                {
                    _optStatusDetail = value;
                }
            }
        }

    }
}
