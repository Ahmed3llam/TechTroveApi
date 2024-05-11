using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTrove.Models;

namespace TechTrove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodesController : ControllerBase
    {
        private readonly TechTroveContext _context;

        public PromoCodesController(TechTroveContext context)
        {
            _context = context;
        }

        // GET: api/PromoCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCode>>> GetPromo()
        {
            return await _context.Promo.ToListAsync();
        }

        // GET: api/PromoCodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PromoCode>> GetPromoCode(int id)
        {
            var promoCode = await _context.Promo.FindAsync(id);

            if (promoCode == null)
            {
                return NotFound();
            }

            return promoCode;
        }

        // PUT: api/PromoCodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromoCode(int id, PromoCode promoCode)
        {
            if (id != promoCode.Id)
            {
                return BadRequest();
            }

            _context.Entry(promoCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromoCodeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PromoCodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PromoCode>> PostPromoCode(PromoCode promoCode)
        {
            _context.Promo.Add(promoCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPromoCode", new { id = promoCode.Id }, promoCode);
        }

        // DELETE: api/PromoCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromoCode(int id)
        {
            var promoCode = await _context.Promo.FindAsync(id);
            if (promoCode == null)
            {
                return NotFound();
            }

            _context.Promo.Remove(promoCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PromoCodeExists(int id)
        {
            return _context.Promo.Any(e => e.Id == id);
        }
    }
}
