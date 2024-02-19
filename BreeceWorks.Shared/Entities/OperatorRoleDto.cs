using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    public class OperatorRoleDto
    {
        [Key]
        public Guid Id { get; set; }

        public String Role { get; set; }
        public List<OperatorDto>? Operators { get; set; }
    }
}
