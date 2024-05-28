using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public partial class CustomerCreateUpdateRqst
    {
        private string _first;
        private string _last;
        private string _email;
        private string _mobile;

        [Required(ErrorMessage = "Customer First Name field is required.")]
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

        [Required(ErrorMessage = "Cuatomer Last Name field is required.")]
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

        [Required(ErrorMessage = "Customer Email field is required.")]
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

        [Required(ErrorMessage = "Customer Mobile field is required.")]
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
    }
}
