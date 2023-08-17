using ContactBook.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ContactBook.Data
{
    public class ContactBookContext : IdentityDbContext<AppUser>
    {
        public ContactBookContext(DbContextOptions<ContactBookContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }


      
    }
}
