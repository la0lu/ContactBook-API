using Microsoft.AspNetCore.Identity;

namespace ContactBook.Data.Entities
{
    public class AppUser : IdentityUser
    {
       public string FirstName { get; set; } = string.Empty;
       public string LastName { get; set; } = string.Empty;
       //public string FullName => $"{FirstName} {LastName}";
    
    }
}
