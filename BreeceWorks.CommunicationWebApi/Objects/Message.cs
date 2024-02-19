using BreeceWorks.Shared.Entities;
using BreeceWorks.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.CommunicationWebApi.Objects
{
    public class Message
    {
        public String id { get; set; }
        public String? sMSId { get; set; }
        public MessageType type { get; set; }
        public MessageFormatting formatting { get; set; }
        public String data { get; set; }
        public String status { get; set; }
        public MessageChannelSource channelSource { get; set; }
        public MessageAuthor? author { get; set; }
        public DateTime createdAt { get; set; }
        public Boolean needsAttention { get; set; }
        public Boolean needsAction { get; set; }
        public List<MessageAttachment>? messageAttachments { get; set; }

    }
}
