using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechTrove.DTO;
using TechTrove.Models;
using TechTrove.Repository;
using TechTrove.UnitOfWork;

namespace TechTrove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private UserManager<User> userManager;
        Unit unit;
        public ProfileController(UserManager<User> userManager, Unit unit)
        {
            this.userManager = userManager;
            this.unit = unit;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            //var Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = unit.UserRepo.GetUser(Id);
            //var roles = await userManager.GetRolesAsync(user);
            //bool isAdmin = roles.Contains("Admin");
            var user = await userManager.GetUserAsync(User);
            return Ok(user);
        }
        [HttpPut("data")]
        [Authorize]
        public IActionResult EditData(UserDataDTO user)
        {
            if (ModelState.IsValid)
            {
                User updated = unit.UserRepo.GetUser(user.id);
                if (updated != null)
                {
                    updated.FirstName = user.firstName;
                    updated.Lastname = user.lastname;
                    updated.PhoneNumber = user.phoneNumber;
                    updated.Email = user.email;
                    updated.Gender = user.gender;
                    updated.Address = user.Address;
                    //updated.ProfileImage = user.profileImage;
                    unit.UserRepo.Update(updated);
                    unit.UserRepo.Save();
                    return Ok(updated);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPut("data/image")]
        [Authorize]
        public IActionResult EditImage(UserImgDTO user)
        {
            if (ModelState.IsValid)
            {
                User updated = unit.UserRepo.GetUser(user.id);
                if (updated != null)
                {
                    updated.ProfileImage = user.Img;
                    unit.UserRepo.Update(updated);
                    unit.UserRepo.Save();
                    return Ok(updated);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPut("data/password")]
        [Authorize]
        public async Task<IActionResult> EditPasswordAsync(PasswordDTO user)
        {
            if (ModelState.IsValid)
            {
                User updated = unit.UserRepo.GetUser(user.Id);
                if (updated != null)
                {
                    bool found = await userManager.CheckPasswordAsync(updated, user.OldPassword);
                    if (found && user.NewPassword == user.ConfirmPassword)
                    {
                        var changePasswordResult = await userManager.ChangePasswordAsync(updated, user.OldPassword, user.NewPassword);
                        if (changePasswordResult.Succeeded)
                        {
                            return Ok(changePasswordResult);
                        }
                        else
                        {
                            return BadRequest(changePasswordResult.Errors);
                        }
                    }
                    return BadRequest("Invalid Input.");
                }
                return NotFound();
            }
            return BadRequest();
        }
        //http://localhost:37667/api/Profile?id=f0c87d1c-d4e9-468c-8137-e147ff39426c
        [HttpDelete]
        [Authorize]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                unit.CartRepo.DeleteAll(id);
                unit.WishRepo.DeleteAll(id);
                unit.ProductRepo.DeleteForUser(id);
                unit.UserRepo.DeleteUser(id);
                unit.Save();
            }catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            return Ok(new { message = "Items deleted successfully" });
        }
    }
}
