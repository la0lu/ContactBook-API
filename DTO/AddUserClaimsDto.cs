using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTO
{
    public class AddUserClaimsDto
    {
        [Required]
        public string UserId { get; set; } = "";
        [Required]
        public Dictionary<string, string> ClaimsToAdd { get; set; } = new Dictionary<string, string>();
    }
}