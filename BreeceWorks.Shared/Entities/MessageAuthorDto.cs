using BreeceWorks.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BreeceWorks.Shared.Entities
{
    public class MessageAuthorDto
    {
        [Key]
        public string id { get; set; }
        public MessageAuthorRole role { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
    }
}
