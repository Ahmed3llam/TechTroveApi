using TechTrove.Models;

namespace TechTrove.Repository
{
    public class PromoCodeRepository :Repository<PromoCode>
    {
        TechTroveContext _context;
        public PromoCodeRepository(TechTroveContext context) : base(context)
        {
            _context = context;
        }
        public PromoCode GetPromoCode(string code)
        {
            return _context.Promo.Where(x => x.Code == code).FirstOrDefault();
        }
    }
}
