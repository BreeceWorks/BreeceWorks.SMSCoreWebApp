using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.Services
{
    public interface IConfigureService
    {
        String? GetValue(String Name);
    }
}
