using ContactBook.Data.Entities;
using ContactBook.DTO;
using ContactBook.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContactBook.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        public AuthController(IAuthService authService, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginDto model) 
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result =await _authService.Login(user, model.Password);
                    if (result != null && result.Succeeded) 
                    {
                        var userToReturn = new ReturnUserDto
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            UserName = user.UserName,
                        };

                        var userroles = await _userManager.GetRolesAsync(user);
                        var token = _authService.GeneratejWT(user, userroles);

                        return Ok(new { user = userToReturn, token = token });
                    }

                    ModelState.AddModelError("Password", "invalid Credentials");

                    return BadRequest(ModelState);
                }

                return BadRequest("Invalid Credentials");
                
            }

            return BadRequest(ModelState);
            

        }
    }
}
