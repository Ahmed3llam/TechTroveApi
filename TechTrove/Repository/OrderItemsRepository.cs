using Microsoft.EntityFrameworkCore;
using TechTrove.Models;

namespace TechTrove.Repository
{
    public class OrderItemsRepository: Repository<OrderItem>
    {
        TechTroveContext _context;
        public OrderItemsRepository(TechTroveContext context):base(context)
        {
            _context = context;
        }
        public bool productExists(int productID,string userID) {
            return _context.OrderItem.Include(oi => oi.Order).Any(oi => oi.ProductId == productID && oi.Order.UserId == userID);
        }
    }
}
