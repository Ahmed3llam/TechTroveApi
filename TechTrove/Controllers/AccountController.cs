using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechTrove.DTO;
using TechTrove.Models;
using TechTrove.Repository;
using TechTrove.UnitOfWork;

namespace TechTrove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        Unit unit;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,Unit unit){
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.unit = unit;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    FirstName = user.firstName,
                    Lastname = user.lastname,
                    Email = user.email,
                    PasswordHash = user.password,
                    PhoneNumber = user.phoneNumber,
                    ProfileImage = user.profileImage,
                    Gender = user.gender,
                    Address = user.Address,
                    UserName = user.firstName + user.lastname + Guid.NewGuid().ToString()
                };

                IdentityResult result = await userManager.CreateAsync(newUser, user.password);

                if (result.Succeeded)
                {
                    IdentityResult resultRole = await userManager.AddToRoleAsync(newUser, "User");
                    await signInManager.SignInAsync(newUser, isPersistent: false);
                    return Ok(newUser);
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    FirstName = user.firstName,
                    Lastname = user.lastname,
                    Email = user.email,
                    PasswordHash = user.password,
                    PhoneNumber = user.phoneNumber,
                    ProfileImage = user.profileImage,
                    Gender = user.gender,
                    Address = user.Address,
                    UserName = user.firstName + user.lastname + Guid.NewGuid().ToString()
                };

                IdentityResult result = await userManager.CreateAsync(newUser, user.password);

                if (result.Succeeded)
                {
                    IdentityResult resultRole = await userManager.AddToRoleAsync(newUser, "Admin");
                    await signInManager.SignInAsync(newUser, isPersistent: false);
                    return Ok(newUser);
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<ActionResult> loginAsync(LoginDTO user)
        {
            if (ModelState.IsValid)
            {
                User data = await userManager.FindByEmailAsync(user.email);
                if (data != null)
                {
                    bool found = await userManager.CheckPasswordAsync(data, user.password);
                    if (found)
                    {
                        await signInManager.SignInAsync(data, user.rememberMe);
                        var userClaims = await userManager.GetClaimsAsync(data);
                        userClaims.Add(new Claim(ClaimTypes.NameIdentifier, data.Id));

                        string key = "Iti Pd And Bi 44 Menoufia Web Api And Angular Courses";
                        var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                        var credential = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            claims: userClaims,
                            expires: DateTime.Now.AddDays(2),
                            signingCredentials: credential
                        );

                        var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                        var roles = await userManager.GetRolesAsync(data);
                        int CartCount = unit.CartRepo.GetForUser(data.Id).Count();
                        int WishCount = unit.WishRepo.GetForUser(data.Id).Count();
                        return Ok(new { User = data, Token = tokenstring, Role = roles.FirstOrDefault() ,CartCount,WishCount});
                    }
                    return NotFound("Invalid email or password");
                }
                return NotFound("User not found");
            }
            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
    }
}
