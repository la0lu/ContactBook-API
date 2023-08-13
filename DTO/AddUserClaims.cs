using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTO
{
    public class AddUserClaims
    {
        [Required]
        public string UserId { get; set; } = "";
        [Required]
        public Dictionary<string, bool> ClaimsToAdd { get; set; } = new Dictionary<string, bool>();
    }
}