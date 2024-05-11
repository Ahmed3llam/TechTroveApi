using Microsoft.EntityFrameworkCore;
using TechTrove.Models;

namespace TechTrove.Repository
{
    public class CommentRepository : Repository<Comment>
    {
        TechTroveContext _context;
        public CommentRepository(TechTroveContext context) : base(context)
        {
            _context = context;
        }
        public List<Comment> GetForProduct(int ProductId,int skip,int content)
        {
            return _context.Comment.Include(e => e.user).Include(p => p.product)
                .Where(c => c.ProductID == ProductId)
                .Skip(skip).Take(content)
                .ToList();
        }
    }
}
