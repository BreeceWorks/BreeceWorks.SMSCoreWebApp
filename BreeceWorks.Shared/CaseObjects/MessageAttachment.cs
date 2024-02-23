using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public partial class MessageAttachment
    {
        private System.Guid? _id;
        private string _sourceUrl;
        private string _name;
        private string _extension;
        private string _contentType;
        private byte[] _data;

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

        public string SourceUrl
        {
            get { return _sourceUrl; }

            set
            {
                if (_sourceUrl != value)
                {
                    _sourceUrl = value;
                }
            }
        }

        public string Name
        {
            get { return _name; }

            set
            {
                if (_name != value)
                {
                    _name = value;
                }
            }
        }

        public string Extension
        {
            get { return _extension; }

            set
            {
                if (_extension != value)
                {
                    _extension = value;
                }
            }
        }

        public string ContentType
        {
            get { return _contentType; }

            set
            {
                if (_contentType != value)
                {
                    _contentType = value;
                }
            }
        }

        public byte[] Data
        {
            get { return _data; }

            set
            {
                if (_data != value)
                {
                    _data = value;
                }
            }
        }

    }
}
