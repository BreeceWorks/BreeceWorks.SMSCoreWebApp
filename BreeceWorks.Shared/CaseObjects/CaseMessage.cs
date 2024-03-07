using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public class CaseMessage:Message
    {
        private System.Guid? _caseId;
        public System.Guid? CaseId
        {
            get { return _caseId; }

            set
            {
                if (_caseId != value)
                {
                    _caseId = value;
                }
            }
        }
    }
}
