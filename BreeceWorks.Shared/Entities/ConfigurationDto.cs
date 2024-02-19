using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    public class ConfigurationDto
    {
        [Key]
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
