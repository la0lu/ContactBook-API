using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTO
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "First Name is compulsory")]
        [StringLength(15, MinimumLength = 2, ErrorMessage ="Name must contain 2 - 15 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Name must contain 2 - 15 characters")]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress(ErrorMessage ="Input a Valid EmailAddress")]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

    }
}
