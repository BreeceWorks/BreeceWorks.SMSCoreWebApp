using BreeceWorks.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public partial class MessageAuthor
    {
        private string _id;
        private MessageAuthorRole? _role;
        private MessageAuthorProfile _profile;

        public string Id
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

        public MessageAuthorRole? Role
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

        public MessageAuthorProfile Profile
        {
            get { return _profile; }

            set
            {
                if (_profile != value)
                {
                    _profile = value;
                }
            }
        }

    }
}
