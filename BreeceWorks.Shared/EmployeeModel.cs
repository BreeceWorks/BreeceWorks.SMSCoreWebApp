using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared
{
    public class EmployeeModel
    {
        private String _name {  get; set; } 
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name 
        { 
            get{ return _name; }
            set{ _name = value; }
        }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [Phone]
        public string PhoneNumer { get; set; }
        [Required]
        [CreditCard]
        public string CreditCardNumer { get; set; }

    }
}
