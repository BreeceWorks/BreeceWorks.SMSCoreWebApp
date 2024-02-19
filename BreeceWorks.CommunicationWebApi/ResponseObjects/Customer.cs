using BreeceWorks.Shared.Enums;

namespace BreeceWorks.CommunicationWebApi.ResponseObjects
{
    public class Customer
    {
        private CustomerRole _customerRole;
        public Guid id { get; set; }
        public String first { get; set; }
        public String last { get; set; }
        public String email { get; set; }
        public String mobile { get; set; }
        public String role
        {
            get
            {
                return _customerRole.ToString();
            }
            set
            {
                Enum.TryParse(value, out _customerRole);
            }
        }
        public Boolean optStatus { get; set; }
        public String optStatusDetail { get; set; }

    }
}
