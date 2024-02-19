using BreeceWorks.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.DbContexts
{
    public class ConfigurationDbContext : DbContext
    {
        public ConfigurationDbContext() { }
        public ConfigurationDbContext(
          DbContextOptions<ConfigurationDbContext> options)
        : base(options)
        {
        }
        public DbSet<ConfigurationDto> Configurations { get; set; }


    }
}
