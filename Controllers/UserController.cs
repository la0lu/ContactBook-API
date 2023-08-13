using ContactBook.Data;
using ContactBook.Data.Entities;
using ContactBook.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactBook.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;


        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        //To Create a New User
        [AllowAnonymous]
        [HttpPost("Signup")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = $"{model.FirstName.Trim().Substring(0, 2).ToUpper()}{new string(model.Email?.Trim().Reverse().ToArray())}",
                    PasswordHash = model.Password

                };

                var identityResult = await _userManager.CreateAsync(user, model.Password);

                if (identityResult.Succeeded)
                {


                    var result = await _userManager.AddToRoleAsync(user, "regular");
                    if (!result.Succeeded)
                    {
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError(err.Code, err.Description);
                        }
                        return BadRequest(ModelState);

                    }


                    var userToReturn = new ReturnUserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.UserName

                    };

                    return Ok(userToReturn);

                }

                foreach (var err in identityResult.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }

            return BadRequest(ModelState);
        }
        [AllowAnonymous]
        [HttpGet("get-all")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var usersToReturn = new List<ReturnUserDto>();


            if (users.Any())
            {
                foreach (var user in users)
                {
                    usersToReturn.Add(new ReturnUserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.UserName
                    });
                }
            }
            return Ok(usersToReturn);
        }


        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userToReturn = new ReturnUserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName
                };

                return Ok(userToReturn);

            }

            return NotFound($"No User with ID: {id}");

        }

        [HttpDelete("delete-user/{id}")]
        public async Task <IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var IdentityResult = await _userManager.DeleteAsync(user);

                if (IdentityResult.Succeeded)
                    return Ok("User deleted sucessfully");
                return BadRequest("Failed to delete user");
            }

            return NotFound($"No User found with id  {id}");
        }


        [Authorize(Roles = "admin")]
        [HttpPost("add-user-roles")]
        public async Task<IActionResult> AddUSerRoles([FromBody]AddUserRoleDto model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if(user != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, model.Role);
                    if (!result.Succeeded)
                    {
                        foreach(var err in result.Errors)
                        {
                            ModelState.AddModelError(err.Code, err.Description);
                        }
                    }

                    return Ok("Role added to user");

                }

                return NotFound($"Could not find user with id: {model.UserId}");
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "regular")]
        [HttpGet("get-user-roles/{userId}")]
        public async Task<IActionResult> GetUSerRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound($"Could not find user with Id: {userId}");
            
            var userRoles = await _userManager.GetRolesAsync(user);
            return Ok(userRoles);
        }

        [Authorize(Roles ="admin")]
        [HttpPost("add-claim-to-user")]
        public async Task<IActionResult> AddClaimsToUser([FromBody]AddUserClaims model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                var claimList = new List<Claim>();

                foreach(var item in model.)
            }

            return BadRequest(ModelState);
        }

    }

}
