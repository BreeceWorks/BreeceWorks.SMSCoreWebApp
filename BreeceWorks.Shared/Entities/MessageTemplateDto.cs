using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    public class MessageTemplateDto
    {
        [Key]
        [Required]
        public Int32 TemplateId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String TemplateText { get; set; }
        public List<TemplateValueDto> TemplateValues { get; set; }
        public List<MessageDto>? Messages { get; set; }
    }
}
