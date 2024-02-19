using BreeceWorks.Shared.Entities;

namespace BreeceWorks.CommunicationWebApi.Objects
{
    public class CaseTranscript:Case
    {
        public CaseTranscript()
        {
            Messages = new List<Message>();
        }
        public List<Message> Messages { get; set; }
    }
}
