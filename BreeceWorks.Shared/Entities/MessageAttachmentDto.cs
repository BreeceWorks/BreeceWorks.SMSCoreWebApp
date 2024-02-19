using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    public class MessageAttachmentDto
    {
        [Key]
        public Guid id { get; set; }
        public String? sourceUrl { get; set; }
        public String? name { get; set; }
        public String? extension { get; set; }
        public byte[]? data { get; set; }
        public List<MessageDto>? Messages { get; set; }
    }
}
