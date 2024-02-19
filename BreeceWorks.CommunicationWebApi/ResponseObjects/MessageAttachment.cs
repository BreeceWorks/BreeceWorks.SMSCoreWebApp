namespace BreeceWorks.CommunicationWebApi.ResponseObjects
{
    public class MessageAttachment
    {
        public Guid id { get; set; }
        public String? sourceUrl { get; set; }
        public String? name { get; set; }
        public String? extension { get; set; }
        public String? contentType { get; set; }
        public byte[]? data { get; set; }

    }
}
