using BreeceWorks.CommunicationWebApi.Objects;

namespace BreeceWorks.CommunicationWebApi.RequestObjects
{
    public class CustomerCreateUpdateRqst
    {
        public String first { get; set; }
        public String last { get; set; }
        public String email { get; set; }
        public String mobile { get; set; }

    }
}
