using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public class Error
    {
        private string _code;
        private string _category;
        private bool? _retryable;
        private int? _status;
        private string _detail;
        private ErrorRspseMeta _meta;
        private string _path;
        private string _method;
        private System.Guid? _requestId;

        public string Code
        {
            get { return _code; }

            set
            {
                if (_code != value)
                {
                    _code = value;
                }
            }
        }

        public string Category
        {
            get { return _category; }

            set
            {
                if (_category != value)
                {
                    _category = value;
                }
            }
        }

        public bool? Retryable
        {
            get { return _retryable; }

            set
            {
                if (_retryable != value)
                {
                    _retryable = value;
                }
            }
        }

        public int? Status
        {
            get { return _status; }

            set
            {
                if (_status != value)
                {
                    _status = value;
                }
            }
        }

        public string Detail
        {
            get { return _detail; }

            set
            {
                if (_detail != value)
                {
                    _detail = value;
                }
            }
        }

        public ErrorRspseMeta Meta
        {
            get { return _meta; }

            set
            {
                if (_meta != value)
                {
                    _meta = value;
                }
            }
        }

        public string Path
        {
            get { return _path; }

            set
            {
                if (_path != value)
                {
                    _path = value;
                }
            }
        }

        public string Method
        {
            get { return _method; }

            set
            {
                if (_method != value)
                {
                    _method = value;
                }
            }
        }

        public System.Guid? RequestId
        {
            get { return _requestId; }

            set
            {
                if (_requestId != value)
                {
                    _requestId = value;
                }
            }
        }

    }
}
