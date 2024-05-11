using Microsoft.EntityFrameworkCore;
using TechTrove.Models;

namespace TechTrove.Repository
{
    public class ProductRepository : Repository<Product>
    {
        TechTroveContext _context;
        public ProductRepository(TechTroveContext context) : base(context)
        {
            _context = context;
        }
        public List<Product> GetForCategory(string Category)
        {
            return _context.Product.Where(p => p.Title == Category).ToList();
        }
        public List<Product> GetForUser(string Userid)
        {
            return _context.Product.Include(p=>p.Category).Include(p=>p.User).Where(p => p.UserID == Userid).ToList();
        }
        public void DeleteForUser(string Userid)
        {
            List<Product> products = _context.Product.Where(p => p.UserID == Userid).ToList();
            foreach(var item in products)
            {
                _context.Product.Remove(item);
            }
        }
        public List<Product> GetSpeceficProduct(List<Category> ParamCategory)
        {
            var allProducts = _context.Product.Include(p => p.Category).Include(p => p.User).Include(p=>p.Order).Include(p=>p.Comments).ToList();
            var filteredProducts = allProducts.Where(p => ParamCategory.Any(c => p.Category.Title.Contains(c.Title))).ToList();
            return filteredProducts;
        }
        public List<Product> GetSpeceficProduct(List<string> ParamCategory)
        {
            var allProducts = _context.Product.Include(p => p.Category).Include(p => p.User).Include(p => p.Order).Include(p => p.Comments).ToList();
            var filteredProducts = allProducts.Where(p => ParamCategory.Any(c => p.Category.Title.Contains(c))).ToList();
            return filteredProducts;
        }
        public int GetProductcount(List<Category> ParamCategory)
        {
            var allProducts = _context.Product.Include(p => p.Category).ToList();
            var filteredProducts = allProducts.Where(p => ParamCategory.Any(c => p.Category.Title.Contains(c.Title))).ToList();
            return filteredProducts.Count;
        }
    }
}
