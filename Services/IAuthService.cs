using ContactBook.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ContactBook.Services
{
    public interface IAuthService
    {
        string GeneratejWT(AppUser user);

        Task<SignInResult> Login(AppUser user, string password);
    }
}
