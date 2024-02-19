using BreeceWorks.Shared.Enums;

namespace BreeceWorks.CommunicationWebApi.Objects
{
    public class Customer
    {
        public Guid Id { get; set; }
        public String First { get; set; }
        public String Last { get; set; }
        public String Email { get; set; }
        public String Mobile { get; set; }
        public CustomerRole Role { get; set; }
        public Boolean OptStatus { get; set; }
        public String OptStatusDetail { get; set; }
    }
}
