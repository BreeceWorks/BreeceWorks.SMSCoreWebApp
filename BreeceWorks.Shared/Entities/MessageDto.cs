using BreeceWorks.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    public class MessageDto
    {
        public MessageDto()
        {
            id = Guid.NewGuid().ToString();
            createdAt = DateTime.Now;
            status = Constants.MessageStatus.UNDELIVERED;
            messageAttachments = new List<MessageAttachmentDto>();
        }


        [Key]
        public String id { get; set; }
        public String? sMSId { get; set; }
        public MessageType type { get; set; }
        public MessageFormatting formatting { get; set; }
        public String text { get; set; }
        public String status { get; set; }
        public MessageChannelSource channelSource { get; set; }
        public MessageAuthorDto? author { get; set; }
        public DateTime createdAt { get; set; }
        public Boolean needsAttention { get; set; }
        public Boolean needsAction { get; set; }
        public List<MessageAttachmentDto>? messageAttachments { get; set; }
        public MessageTemplateDto? messageTemplate { get; set; }
    }
}
