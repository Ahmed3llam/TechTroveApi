using Microsoft.EntityFrameworkCore;
using TechTrove.Models;

namespace TechTrove.Repository
{
    public class WishRepository : Repository<Wish>
    {
        TechTroveContext _context;
        public WishRepository(TechTroveContext context) : base(context)
        {
            _context = context;
        }
        public List<Wish> GetForUser(string Userid)
        {
            return _context.Wish.Include(p => p.Product).Where(p => p.UserID == Userid).ToList();
        }
        public Wish GetById(int productId, string Userid)
        {
            return _context.Wish.Include(p => p.Product).SingleOrDefault(i => i.ProductID == productId && i.UserID == Userid);
        }
        public void Delete(int productId, string Userid)
        {
            _context.Wish.Remove(GetById(productId, Userid));
        }
        public void DeleteAll(string UserId)
        {
            foreach (Wish wish in GetForUser(UserId))
            {
                _context.Wish.Remove(wish);
            }
        }
    }
}
