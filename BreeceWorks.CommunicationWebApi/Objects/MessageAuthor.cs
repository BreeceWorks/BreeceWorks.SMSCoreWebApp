using BreeceWorks.Shared.Enums;

namespace BreeceWorks.CommunicationWebApi.Objects
{
    public class MessageAuthor
    {
        public MessageAuthor()
        {
            profile = new MessageAuthorProfile();
        }
        public string id { get; set; }
        public MessageAuthorRole role { get; set; }
        public MessageAuthorProfile profile { get; set; }

    }
}
