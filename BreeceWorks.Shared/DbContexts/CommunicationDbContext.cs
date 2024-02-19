using BreeceWorks.Shared.Entities;
using BreeceWorks.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreeceWorks.Shared.DbContexts
{
    public class CommunicationDbContext : DbContext
    {
        public CommunicationDbContext(
          DbContextOptions<CommunicationDbContext> options)
        : base(options)
        {

        }

        public DbSet<CompanyPhoneNumberDto> CompanyPhoneNumbers { get; set; }
        public DbSet<CaseDto> Cases { get; set; }

        public DbSet<CustomerDto> Customers { get; set; }

        public DbSet<OperatorDto> Operators { get; set; }

        public DbSet<OperatorRoleDto> OperatorRoles { get; set; }

        public DbSet<LineOfBusinessDto> lineOfBusinesses { get; set; }
        public DbSet<MessageDto> Messages { get; set; }
        public DbSet<MessageAttachmentDto> MessageAttachments { get; set; }
        public DbSet<MessageAuthorDto> MessageAuthors { get; set; }
        public DbSet<MessageTemplateDto> MessageTemplates { get; set; }
        public DbSet<TemplateValueDto> MessageTemplateValues { get; set; }

    }
}

