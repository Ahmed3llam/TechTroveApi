using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechTrove.DTO;

namespace TechTrove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> RoleManager)
        {
            roleManager = RoleManager;
        }
        
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleDTO newRole)
        {
            if (ModelState.IsValid == true)
            {
                IdentityRole roleModel = new IdentityRole()
                {
                    Name = newRole.role
                };
                IdentityResult rust = await roleManager.CreateAsync(roleModel);

            }
            return Ok(newRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, RoleDTO updatedRole)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }
                role.Name = updatedRole.role;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(updatedRole);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update role");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound(); 
            }
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete role");
            }
        }
    }
}
