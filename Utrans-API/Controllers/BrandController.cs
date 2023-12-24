using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utrans_API.DBContexts;
using Utrans_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Utrans_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _context;

        public BrandController(BrandContext context)
        {
            _context = context;
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.id == id);
        }

        // GET: api/<BrandController>
        // /api/Brand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brands>>> GetBrands()
        {   
            return _context.Brands.Where(b => b.Deleted_at == null).ToList();
        }

        // GET api/<BrandController>/5
        // api/Brand/[ID]
        // hash
        [HttpGet("{id}")]
        public async Task<ActionResult<Brands>> GetBrands(int id)
        {   
            var Brand = await _context.Brands.Where(b => b.Deleted_at == null).FirstOrDefaultAsync(b => b.id == id); ;

            if (Brand == null)
            {
                return NotFound();
            }

            return Brand;
            //return "value";
        }

        // POST api/<BrandController>
        // api/Brand
        [HttpPost]
        public async Task<ActionResult<Brands>> PostBrands(Brands Brand)
        {
            var brand = new Brands
            {
                Code = Brand.Code,
                Name = Brand.Name,
                Number = Brand.Number,
                Email = Brand.Email,
                Website = Brand.Website,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            await _context.Brands.AddAsync(brand);

            try
            {
                await _context.SaveChangesAsync();
            } catch (DbUpdateException)
            {
                _context.Entry(brand).State = EntityState.Detached;
                throw;
            }

            return CreatedAtAction(nameof(GetBrands), new { id = Brand.id }, Brand);
        }   

        // PUT api/<BrandController>/5
        // api/Brand/[ID]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, Brands Brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBrand = await _context.Brands.FindAsync(id);

            if (existingBrand == null)
            {
                return NotFound($"Brand with ID {id} not found");
            }

            existingBrand.Name = Brand.Name;
            existingBrand.Number = Brand.Number;
            existingBrand.Email = Brand.Email;
            existingBrand.Website = Brand.Website;
            existingBrand.Updated_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
                {
                    return NotFound($"Brand with ID {id} not found");
                }
                else
                {
                    _context.Entry(existingBrand).State = EntityState.Detached;
                    throw;
                }
            }

            return Ok(new { message = "Brand updated" });
        }

        // DELETE api/<BrandController>/5
        // api/Brand/[ID]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Brands>> DeleteBrand(int id)
        {
            var existingBrand = await _context.Brands.FindAsync(id);

            if (existingBrand == null)
            {
                return NotFound();
            }

            existingBrand.Deleted_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Brand deleted" });
        }
    }
}
