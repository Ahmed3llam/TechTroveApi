using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTrove.DTO;
using TechTrove.Models;
using TechTrove.Repository;
using TechTrove.UnitOfWork;

namespace TechTrove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        Unit unit;
        private UserManager<User> userManager;
        public CartItemsController(Unit unit, UserManager<User> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }

        // GET: api/CartItems
        [HttpGet]
        public async Task<ActionResult> GetCartAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                if (userId != null)
                {
                    List<CartItem> cart = unit.CartRepo.GetForUser(userId);
                    if (cart.Count == 0) return NoContent();
                    List<CartItemDTO> cartDto = new List<CartItemDTO>();
                    decimal TotalPrice = 0;
                    foreach (var item in cart)
                    {
                        CartItemDTO cartItem = new CartItemDTO()
                        {
                            Id = item.ProductID,
                            Title= item.Product?.Title,
                            Description = item.Product?.Description,
                            Img = item.Product?.Img,
                            Price = item.Product.Price,
                            Stock = item.Product.Stock,
                            Quantity = item.Quantity,
                            TPrice = item.Quantity * item.Product.Price,
                        };
                        TotalPrice += cartItem.TPrice;
                        cartDto.Add(cartItem);
                    }
                    return Ok(new { cartDto, TotalPrice });
                }
            }
            return Unauthorized("User not authenticated or user ID claim not found");
        }
        // PUT: api/CartItems/Increment/5
        [HttpPut("Increment/{id}")]
        public async Task<IActionResult> IncrementQuantityAsync(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var cartItem = unit.CartRepo.GetById(id,user.Id);
            if (cartItem != null)
            {
                if(cartItem.Quantity >= cartItem.Product.Stock)
                {
                    return BadRequest("Quantity cannot be more than number of product in stock.");
                }
                cartItem.Quantity++;
                unit.CartRepo.Update(cartItem);
                unit.CartRepo.Save();
                CartItemDTO cartItemdto = new CartItemDTO()
                {
                    Id = cartItem.ProductID,
                    Title = cartItem.Product?.Title,
                    Description = cartItem.Product?.Description,
                    Img = cartItem.Product?.Img,
                    Price = cartItem.Product.Price,
                    Stock = cartItem.Product.Stock,
                    Quantity = cartItem.Quantity,
                    TPrice = cartItem.Quantity * cartItem.Product.Price,
                };
                return Ok(cartItemdto);
            }
            return NotFound();
        }

        // Put: api/Cart/Decrement/5
        [HttpPut("Decrement/{id}")]
        [Authorize]
        public async Task<IActionResult> DecrementQuantityAsync(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var cartItem = unit.CartRepo.GetById(id, user.Id);
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--; 
                    unit.CartRepo.Update(cartItem);
                    unit.CartRepo.Save();
                    CartItemDTO cartItemdto = new CartItemDTO()
                    {
                        Id = cartItem.ProductID,
                        Title = cartItem.Product?.Title,
                        Description = cartItem.Product?.Description,
                        Img = cartItem.Product?.Img,
                        Price = cartItem.Product.Price,
                        Stock = cartItem.Product.Stock,
                        Quantity = cartItem.Quantity,
                        TPrice = cartItem.Quantity * cartItem.Product.Price,
                    };
                    return Ok(cartItemdto);
                }
                return BadRequest("Quantity cannot be less than 1.");
            }
            return NotFound();
        }

        // POST: api/CartItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AddToCartAsync(CartDTO cart)
        {
            var user = await userManager.GetUserAsync(User);
            if(user != null)
            {
                var userId = user.Id;
                if (unit.CartRepo.GetById(cart.ProductId, userId) != null)
                {
                    CartItem cartItem = unit.CartRepo.GetById(cart.ProductId, userId);
                    cartItem.Quantity++;
                    unit.CartRepo.Update(cartItem);
                    unit.CartRepo.Save();
                    return Ok(cartItem.Quantity);
                }
                else
                {
                    CartItem cartItem = new()
                    {
                        ProductID = cart.ProductId,
                        UserID = userId,
                        Quantity = cart.Quantity
                    };
                    unit.CartRepo.Add(cartItem);
                    unit.CartRepo.Save();
                    return Ok(cartItem.Quantity);
                }
            }
            return Unauthorized();
        }

        // DELETE: api/CartItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                unit.CartRepo.Delete(id, userId);
                try
                {
                    unit.CartRepo.Save();
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
        public async Task<IActionResult> DeleteAllCartItem()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                unit.CartRepo.DeleteAll(userId);
                try
                {
                    unit.CartRepo.Save();
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
