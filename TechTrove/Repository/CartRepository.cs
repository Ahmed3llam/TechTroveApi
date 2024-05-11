using Microsoft.EntityFrameworkCore;
using TechTrove.Models;

namespace TechTrove.Repository
{
    public class CartRepository:Repository<CartItem>
    {
        TechTroveContext _context;
        public CartRepository(TechTroveContext context):base(context)
        {
            _context = context;
        }
        public List<CartItem> GetForUser(string Userid)
        {
            return _context.Cart.Include(p => p.Product).Where(p => p.UserID == Userid).ToList();
        }
        public CartItem GetById(int productId, string Userid)
        {
            return _context.Cart.Include(p => p.Product).FirstOrDefault(i => i.ProductID == productId && i.UserID == Userid);
        }
        public decimal GetTotalPrice(string Userid)
        {
            decimal totalPrice = 0;
            foreach (CartItem cartItem in GetForUser(Userid))
            {
                totalPrice += (cartItem.Product.Price * cartItem.Quantity);
            }
            return totalPrice;
        }
        public decimal GetTotalPriceOfOneItem(CartItem item)
        {
            return item.Quantity * item.Product.Price;
        }
        public void Update(int productId, string Userid, CartItem item)
        {
            if (GetById(productId, Userid) != null)
            {
                _context.Cart.Update(item);
            }
        }
        public void Delete(int productId, string Userid)
        {
            _context.Cart.Remove(GetById(productId, Userid));
        }
        public void DeleteAll(string UserId)
        {
            foreach (CartItem cartItem in GetForUser(UserId))
            {
                _context.Cart.Remove(cartItem);
            }
        }
    }
}
