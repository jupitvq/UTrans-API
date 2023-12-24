using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Utrans_API.DBContexts;
using Utrans_API.Models;
using Utrans_API.DBContexts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Utrans_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly BrandContext _brandContext;

        public ProductController(ProductContext context, BrandContext brandContext)
        {
            _context = context;
            _brandContext = brandContext;
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }

        // GET: api/<ProductsController>
        // api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var brandNames = await _brandContext.Brands.ToDictionaryAsync(brand => brand.id, brand => brand.Name);

            var products = await _context.Products.Where(product => product.Deleted_at == null).Select(product => new
                
                {
                    product.id,
                    product.brand_id,
                    BrandName = brandNames.ContainsKey(product.brand_id) ? brandNames[product.brand_id] : null,
                    product.Code,
                    product.Name,
                    product.Description,
                    product.stock,
                    product.sales_price,
                    product.standard_price,
                    product.Created_at,
                    product.Updated_at,
                    product.Deleted_at,
                }).ToListAsync();

            return Ok(products);
        }

        // GET api/<ProductsController>/5
        // api/Product/[ID]
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var Product = await _context.Products.Where(b => b.Deleted_at == null).FirstOrDefaultAsync(b => b.id == id); ;

            if (Product == null)
            {
                return NotFound();
            }

            return Product;
            //return "value";
        }

        // POST api/<ProductsController>
        // POST api/Product
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products Product)
        {
          
            var product = new Products
            {
                brand_id = Product.brand_id,
                Code = Product.Code,
                Name = Product.Name,
                Description = Product.Description,
                stock = Product.stock,
                sales_price = Product.sales_price,
                standard_price = Product.standard_price,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
                Deleted_at = null
            };

            await _context.Products.AddAsync(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                _context.Entry(product).State = EntityState.Detached;
                throw;
            }

            return CreatedAtAction(nameof(GetProducts), new { id = product.id }, product);
        }

        // PUT api/<ProductsController>/5
        // api/Product/[ID]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Products Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found");
            }

            existingProduct.brand_id = Product.brand_id; // Fixed the issue by assigning the brand_id property directly
            existingProduct.Code = Product.Code;
            existingProduct.Name = Product.Name;
            existingProduct.Description = Product.Description;
            existingProduct.stock = Product.stock;
            existingProduct.sales_price = Product.sales_price;
            existingProduct.standard_price = Product.standard_price;
            existingProduct.Updated_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound($"Brand with ID {id} not found");
                }
                else
                {
                    _context.Entry(Product).State = EntityState.Detached;
                    throw;
                }
            }

            return Ok(new { message = "Product Updated" });
        }

        // DELETE api/<ProductsController>/5
        // api/Product/[ID]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProduct(int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Deleted_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Product deleted" });
        }
    }
}
