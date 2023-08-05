using ContactBook.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactBook.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthService(IConfiguration config, SignInManager<AppUser> signInManager)
        {
            _config = config;
            _signInManager = signInManager;
        }

        public string GeneratejWT(AppUser user, IList<string> roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var JwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_config.GetSection("JWT:KEY").Value);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToInt32(_config.GetSection("JWT:LifeSpan").Value)),
                signingCredentials: signingCredentials
                );

            var token = JwtSecurityTokenHandler.WriteToken(securityToken);
            return token;
        }

        public async Task<SignInResult>  Login (AppUser user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }
    }
}
