using BreeceWorks.Shared.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.Services
{
    public class ConfigureService : IConfigureService
    {
        private readonly ConfigurationDbContext _context;

        public ConfigureService(ConfigurationDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public string? GetValue(String Name)
        {
            var configuration = _context.Configurations.FirstOrDefault();
            if (configuration == null)
                return null;
            else
                return _context.Configurations.FirstOrDefault(c => c.Name == Name) == null ? String.Empty : _context.Configurations.First(c => c.Name == Name).Value;

        }
    }
}
