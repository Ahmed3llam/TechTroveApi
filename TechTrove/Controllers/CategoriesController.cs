using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTrove.DTO;
using TechTrove.Models;
using TechTrove.UnitOfWork;

namespace TechTrove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        Unit unit;

        public CategoriesController(Unit unit)
        {
            this.unit = unit;
        }

        // GET: api/Categories
        [HttpGet]
        public ActionResult GetCategory(int page = 1, int pageSize = 9)
        {
            List<Category> categories = unit.CategoryRepo.GetAll(c=>c.Products);
            if (categories.Count == 0) return NotFound();
            int Count = categories.Count();
            var totalPages = (int)Math.Ceiling((double)Count / pageSize);
            categories = categories.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            List<CategoryDTO> catDto = new List<CategoryDTO>();
            foreach (var item in categories)
            {
                int productsCount = item.Products != null ? item.Products.Count : 0;
                CategoryDTO category = new CategoryDTO()
                {
                    id = item.Id,
                    title = item.Title,
                    productsCount = productsCount
                };
                catDto.Add(category);
            }

            return Ok(new { categories = catDto, totalPages });
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public ActionResult GetCategory(int id)
        {
            Category category = unit.CategoryRepo.GetById(id, p => p.Products);
            if (category == null) return NotFound();
            int productsCount = category.Products != null ? category.Products.Count : 0;
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                id = category.Id,
                title = category.Title,
                productsCount = productsCount
            };
            return Ok(categoryDTO);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            unit.CategoryRepo.Update(category);

            try
            {
                unit.CategoryRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            unit.CategoryRepo.Add(category);
            try
            {
                unit.CategoryRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var product = unit.CategoryRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            unit.CategoryRepo.Delete(id);
            try
            {
                unit.CategoryRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { message = $"{id} Deleted" });
        }


        private bool CategoryExists(int id)
        {
            return unit.CategoryRepo.Exist(id);
        }
    }
}
