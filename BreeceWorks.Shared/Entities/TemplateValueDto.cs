using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreeceWorks.Shared.Entities
{
    public class TemplateValueDto
    {
        [Key]
        public String Id { get; set; }
        public String Name { get; set; }
    }
}
