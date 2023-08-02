using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTO
{
    public class UpdateContactDto
    {
        //public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name should be between 2 and 20 characters.")]
        public string FirstName { get; set; } 

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name should be between 2 and 20 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}