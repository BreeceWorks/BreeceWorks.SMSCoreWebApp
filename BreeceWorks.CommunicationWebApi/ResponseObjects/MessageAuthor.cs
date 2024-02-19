using BreeceWorks.CommunicationWebApi.Objects;
using BreeceWorks.Shared.Enums;

namespace BreeceWorks.CommunicationWebApi.ResponseObjects
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
