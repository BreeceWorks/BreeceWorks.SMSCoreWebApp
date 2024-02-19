using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    public class OperatorDto
    {
        [Key]
        public Guid Id { get; set; }

        public String First { get; set; }
        public string Last { get; set; }
        public string Email { get; set; }

        public List<OperatorRoleDto>? Roles { get; set; }
        public String PhoneNumber { get; set; }
        public String? IdentityProvider { get; set; }
        public String? Title { get; set; }
        public String? Password { get;set; }
        public String? OfficeNumber { get;set; }

        public List<CaseDto>? Cases { get; set; }
    }
}
