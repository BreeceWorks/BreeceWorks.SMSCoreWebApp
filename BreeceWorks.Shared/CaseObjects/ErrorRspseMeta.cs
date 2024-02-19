using BreeceWorks.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public class ErrorRspseMeta
    {
        private CaseData _existingCase;

        public CaseData ExistingCase
        {
            get { return _existingCase; }

            set
            {
                if (_existingCase != value)
                {
                    _existingCase = value;
                }
            }
        }

    }
}
