namespace BreeceWorks.CommunicationWebApi.ResponseObjects
{
    public class CaseTranscript : Case
    {
        public CaseTranscript()
        {
            Messages = new List<Message>();
        }
        public List<Message>? Messages { get; set; }

    }
}
