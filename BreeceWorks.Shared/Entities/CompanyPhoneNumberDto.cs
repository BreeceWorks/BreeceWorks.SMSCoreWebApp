using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    public class CompanyPhoneNumberDto
    {
        [Key]
        public String PhoneNumber { get; set; }

        public String SMSProcessor { get; set; }
        public Boolean IsActive {  get; set; }
    }
}
