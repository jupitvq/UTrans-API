using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Utrans_API.DBContexts;
using Utrans_API.Models;
using Utrans_API.Repository;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brands>>> GetBrands()
        {   
            return await _context.Brands.ToListAsync();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<BrandController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brands>> GetBrands(int id)
        {   
            var Brand = await _context.Brands.FindAsync(id);

            if (Brand == null)
            {
                return NotFound();
            }

            return Brand;
            //return "value";
        }

        // POST api/<BrandController>
        [HttpPost]
        public async Task<ActionResult<Brands>> PostBrands(Brands Brand)
        {
            _context.Brands.Add(Brand);
            try
            {
                await _context.SaveChangesAsync();
            } catch (DbUpdateException)
            {
                if (BrandExists(Brand.id))
                {
                    return Conflict();
                } else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetBrands), new { id = Brand.id }, Brand);
        }   

        // PUT api/<BrandController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, Brands Brand)
        {
            if (id != Brand.id)
            {
                return BadRequest();
            }

            _context.Entry(Brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
                {
                    return NotFound();
                } else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<BrandController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Brands>> DeleteBrand(int id)
        {
            var Brand = await _context.Brands.FindAsync(id);
            if (Brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(Brand);
            await _context.SaveChangesAsync();

            return Brand;   
        }
    }
}
