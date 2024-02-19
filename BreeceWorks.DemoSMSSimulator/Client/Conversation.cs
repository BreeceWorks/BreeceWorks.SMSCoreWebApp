namespace BreeceWorks.DemoSMSSimulator.Client
{
    public class Conversation
    {
        public Conversation() 
        { 
            Messages = new List<Message>();
        }
        public String FirstParty { get; set; }
        public String SecondParty { get; set; }
        public List<Message> Messages { get; set; }
    }
}
