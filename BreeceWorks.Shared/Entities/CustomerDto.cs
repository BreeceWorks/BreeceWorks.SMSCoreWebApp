using BreeceWorks.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    [Index(nameof(Email), nameof(Mobile), IsUnique = true)]
    public class CustomerDto
    {
        [Key]
        public Guid Id { get; set; }
        public String First { get; set; }
        public String Last { get; set; }
        public String Email { get; set; }
        public String Mobile { get; set; }
        public CustomerRole Role { get; set; }
        public Boolean OptStatus { get; set; }
        public String OptStatusDetail { get; set; }
        public List<CaseDto>? Cases { get; set; }
    }
}
