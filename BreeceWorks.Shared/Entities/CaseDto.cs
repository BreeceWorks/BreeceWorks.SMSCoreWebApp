using BreeceWorks.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    public class CaseDto
    {
        public CaseDto() 
        {
            Messages = new List<MessageDto>();
        }
        [Key]
        public Guid Id { get; set; }
        public Boolean Archived { get; set; }
        public String ClaimNumber { get; set; }
        public String? DateOfLoss { get; set; }
        public String PolicyNumber { get; set; }
        public Int32? Deductible { get; set; }
        public String? Brand { get; set; }
        public String? BusinessName { get;set; }
        public LineOfBusinessDto? LineOfBusiness { get; set; }

        public CaseState State { get; set; }
        public CaseType CaseType { get; set; }
        public CustomerDto Customer { get; set; }
        public Guid? PrimaryContact { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreateTime { get; set; }
        public String ReferenceId { get; set; }
        public Privacy? Privacy { get; set; }
        public String? SMSNumber { get; set; }
        public LanguagePreference? LanguagePreference { get; set; }

        public List<OperatorDto>? SecondaryOperators { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
