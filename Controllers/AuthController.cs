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
            var response = new ResponseObject<object, object>();
            response.Code = 400;
            response.Status = false;
            response.Message = "Failed";
            response.Data = null;

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
                        var userclaims = await _userManager.GetClaimsAsync(user);

                        var token = _authService.GeneratejWT(user, userroles, userclaims);
                        
                        response.Code = 200;
                        response.Status = true;
                        response.Message = "Successful";
                        response.Data = new { user = userToReturn, token = token };

                        return Ok(response);

                        

                    }

                    response.Errors = "Invalid Credential";

                    return BadRequest(response);
                }

                response.Errors = "Invalid Credential";

                return BadRequest(response);

            }
            response.Errors = ModelState;
            return BadRequest(response);
            

        }
    }
}
