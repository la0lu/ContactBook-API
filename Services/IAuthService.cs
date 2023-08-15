using ContactBook.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ContactBook.Services
{
    public interface IAuthService
    {
        string GeneratejWT(AppUser user, IList<string> roles, IList<Claim> userclaims);
        Task<SignInResult> Login(AppUser user, string password);
    }
}
