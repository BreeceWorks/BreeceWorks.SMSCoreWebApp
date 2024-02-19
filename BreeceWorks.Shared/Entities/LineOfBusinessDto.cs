using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    public class LineOfBusinessDto
    {
        [Key]
        public Guid Id { get; set; }
        public String Type { get;set; }
        public String SubType { get;set; }
        public List<CaseDto>? Cases { get; set; }
    }
}
