namespace BreeceWorks.CommunicationWebApi.ResponseObjects
{
    public class TemplateMessage
    {
        public Boolean isAI {get;set; }
        public Boolean isEvent{get;set; }
        public Boolean isInbound {get;set; }
        public String[] redactionData { get;set; }
        public String[] redactionViewedEvents { get;set; }
        public Boolean isFile { get;set; }
        public Boolean isImage { get;set; }
        public Boolean isNote { get;set; }
        public Boolean isArchived { get;set; }
        public Boolean isActive { get;set; }
        public String  _id { get;set; }
        public String chatId { get;set; }
        public String? authorId { get;set; }
        public String body { get;set; }
        public String deliveryStatus { get;set; }
        public DateTime createdAt { get;set; }
        public DateTime updatedAt { get;set; }
        public Error[]? errors { get; set; }
    }
}
