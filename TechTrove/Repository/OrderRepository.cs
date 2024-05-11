using Microsoft.EntityFrameworkCore;
using TechTrove.Models;

namespace TechTrove.Repository
{
    public class OrderRepository:Repository<Order>
    {
        TechTroveContext _context;
        public OrderRepository(TechTroveContext context):base(context)
        {
            _context = context;
        }
		public List<Order> GetSomeOrders(int skip, int content)
		{
			return _context.Order.Include(o => o.User).Skip(skip).Take(content).ToList();
		}
        public List<Order> GetSomeOrdersForUser(string userid, int skip, int content)
        {
            return _context.Order
                     .Include(o => o.payment)
                     .Include(o => o.User)
                     .Where(o => o.UserId == userid)
                     .Skip(skip)
                     .Take(content)
                     .ToList();
        }
        public List<OrderItem> GetOrderItem(int Orderid)
        {
            return _context.OrderItem
                           .Include(o => o.Product).Include(o => o.Order)
                           .Where(o=>o.OrderId==Orderid).OrderBy(o=>o.Order.Date)
                           .ToList();
        }
        public int CountOrdersForUser(string userid)
        {
            return _context.Order.Where(o => o.UserId == userid).Count();
        }
        public int GetOrderItemCount(int Orderid)
        {
            return _context.OrderItem.Where(o => o.OrderId == Orderid).Count();
        }
    }
}
