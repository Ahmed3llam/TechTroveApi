using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class ProductsController : ControllerBase
    {
        Unit unit;

        public ProductsController(Unit unit)
        {
            this.unit = unit;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult GetProduct(int page = 1, int pageSize = 9)
        {
            List<Product> products = unit.ProductRepo.GetAll(p=>p.Category, p=>p.Comments,p=>p.Order);
            if (products.Count == 0) return NotFound();
            int Count = products.Count();
            var totalPages = (int)Math.Ceiling((double)Count / pageSize);
            products =products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            List<ProductDTO> prodDto = new List<ProductDTO>();
            foreach (var item in products)
            {
                int reviewsCount = item.Comments != null ? item.Comments.Count : 0;
                int ordersCount = item.Order != null ? item.Order.Count : 0;
                ProductDTO prod = new ProductDTO()
                {
                    id = item.Id,
                    title = item.Title,
                    description = item.Description,
                    img=item.Img,
                    price=item.Price,
                    stock=item.Stock,
                    category=item.Category?.Title,
                    reviewCount= reviewsCount,
                    orderCount= ordersCount
                };
                prodDto.Add(prod);
            }
            return Ok(new { products = prodDto, totalPages });
        }

        // GET: api/Products/filter
        [HttpGet("filter")]
        public ActionResult FilterProduct([FromQuery] List<string> categories, int page = 1, int pageSize = 9)
        {
            List<Product> products = unit.ProductRepo.GetAll(p => p.Category, p => p.Comments, p => p.Order);
            if (products.Count == 0) return NotFound();
            int Count;
            if (categories.Count == 0) {
                List<Category> ParamCategory = unit.CategoryRepo.GetAll();
                products = unit.ProductRepo.GetSpeceficProduct(ParamCategory);
                Count = products.Count();
            }
            else
            {
                products = unit.ProductRepo.GetSpeceficProduct(categories);
                Count = products.Count();
            }
            var totalPages = (int)Math.Ceiling((double)Count / pageSize);
            //products = unit.ProductRepo.Pagination(products, page, pageSize);
            products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            List<ProductDTO> prodDto = new List<ProductDTO>();
            foreach (var item in products)
            {
                int reviewsCount = item.Comments != null ? item.Comments.Count : 0;
                int ordersCount = item.Order != null ? item.Order.Count : 0;
                ProductDTO prod = new ProductDTO()
                {
                    id = item.Id,
                    title = item.Title,
                    description = item.Description,
                    img = item.Img,
                    price = item.Price,
                    stock = item.Stock,
                    category = item.Category?.Title,
                    reviewCount = reviewsCount,
                    orderCount = ordersCount
                };
                prodDto.Add(prod);
            }
            return Ok(new { products = prodDto, totalPages });
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult GetProduct(int id)
        {
            Product product = unit.ProductRepo.GetById(id,p => p.Category, p => p.Comments, p => p.Order);
            if (product == null) return NotFound();
            int reviewsCount = product.Comments != null ? product.Comments.Count : 0;
            int ordersCount = product.Order != null ? product.Order.Count : 0;
            ProductDTO prod = new ProductDTO()
            {
                id = product.Id,
                title = product.Title,
                description = product.Description,
                img = product.Img,
                price = product.Price,
                stock = product.Stock,
                category = product.Category?.Title,
                reviewCount = reviewsCount,
                orderCount = ordersCount
            };
            return Ok(prod);
        }

        // PUT: api/Products/data/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("data/{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDataDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Product updated = unit.ProductRepo.GetById(product.Id);
                if (updated != null)
                {
                    updated.Id = product.Id;
                    updated.Title = product.Title;
                    updated.Description = product.Description;
                    updated.Price = product.Price;
                    updated.Stock = product.Stock;
                    updated.CategoryId = product.CategoryId;
                    unit.ProductRepo.Update(updated);
                    try
                    {
                        unit.ProductRepo.Save();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return Ok(updated);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPut("data/img/{id}")]
        public async Task<IActionResult> PutImage(int id, ProductImgDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Product updated = unit.ProductRepo.GetById(product.Id);
                if (updated != null)
                {
                    updated.Id = product.Id;
                    updated.Img = product.Img;
                    unit.ProductRepo.Update(updated);
                    try
                    {
                        unit.ProductRepo.Save();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return Ok(updated);
                }
                return NotFound();
            }
            return BadRequest();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            unit.ProductRepo.Add(product);
            try
            {
                unit.ProductRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = unit.ProductRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            unit.ProductRepo.Delete(id);
            try
            {
                unit.ProductRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { message = "Item deleted successfully" });
        }

        private bool ProductExists(int id)
        {
            return unit.ProductRepo.Exist(id);
        }
    }
}
