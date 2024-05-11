using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTrove.DTO;
using TechTrove.Models;
using TechTrove.UnitOfWork;

namespace TechTrove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishesController : ControllerBase
    {
        Unit unit;
        private UserManager<User> userManager;
        public WishesController(Unit unit, UserManager<User> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }

        // GET: api/wishes
        [HttpGet]
        public async Task<ActionResult> GetWishAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                if (userId != null)
                {
                    List<Wish> cart = unit.WishRepo.GetForUser(userId);
                    if (cart.Count == 0) return NoContent();
                    List<ProductDTO> data = new List<ProductDTO>();
                    foreach (var item in cart)
                    {
                        int reviewsCount = item.Product.Comments != null ? item.Product.Comments.Count : 0;
                        int ordersCount = item.Product.Order != null ? item.Product.Order.Count : 0;
                        ProductDTO prod = new ProductDTO()
                        {
                            id = item.ProductID,
                            title = item.Product.Title,
                            description = item.Product.Description,
                            img = item.Product.Img,
                            price = item.Product.Price,
                            stock = item.Product.Stock,
                            category = item.Product.Category?.Title,
                            reviewCount = reviewsCount,
                            orderCount = ordersCount
                        };
                        data.Add(prod);
                    }
                    return Ok(data);
                }
            }
            return Unauthorized("User not authenticated or user ID claim not found");
        }
        // POST: api/wishes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AddToWishAsync(CartDTO cart)
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                bool found = true;
                if (unit.WishRepo.GetById(cart.ProductId, userId) != null)
                {
                    return Ok(found);
                }
                else
                {
                    Wish data = new()
                    {
                        ProductID = cart.ProductId,
                        UserID = userId
                    };
                    unit.WishRepo.Add(data);
                    unit.WishRepo.Save();
                    return Ok(!found);
                }
                
            }
            return Unauthorized();
        }

        // DELETE: api/wishes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishItem(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                unit.WishRepo.Delete(id, userId);
                try
                {
                    unit.WishRepo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return StatusCode(500, "An error occurred while deleting the item from the cart.");
                }
                return Ok(new { message = "Item deleted successfully" });
            }
            return Unauthorized();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllWishes()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                unit.WishRepo.DeleteAll(userId);
                try
                {
                    unit.WishRepo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return StatusCode(500, "An error occurred while deleting the item from the cart.");
                }
                return Ok(new { message = "Items deleted successfully" });
            }
            return Unauthorized();
        }
    }
}
