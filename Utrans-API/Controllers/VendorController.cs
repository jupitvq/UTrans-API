using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utrans_API.DBContexts;
using Utrans_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Utrans_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly VendorContext _context;

        public VendorController(VendorContext context)
        {
            _context = context;
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.id == id);
        }

        // GET: api/<VendorController>
        // api/Vendor   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendors>>> GetVendors()
        {
            return await _context.Vendors.ToListAsync();
        }

        // GET api/<VendorController>/5
        // api/Vendor/[ID]
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendors>> GetVendors(int id)
        {
            var Vendor = await _context.Vendors.FindAsync(id);

            if (Vendor == null)
            {
                return NotFound();
            }

            return Vendor;
        }

        // POST api/<VendorController>
        // api/Vendor
        [HttpPost]
        public async Task<ActionResult<Vendors>> PostBrands(Vendors Vendor)
        {
            _context.Vendors.Add(Vendor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VendorExists(Vendor.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetVendors), new { id = Vendor.id }, Vendor);
        }

        // PUT api/<VendorController>/5
        // api/Vendor/[ID]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, Vendors Vendor)
        {
            if (id != Vendor.id)
            {
                return BadRequest();
            }

            _context.Entry(Vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // DELETE api/<VendorController>/5
        // api/Vendor/[ID]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vendors>> DeleteVendor(int id)
        {
            var Vendor = await _context.Vendors.FindAsync(id);
            if (Vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(Vendor);
            await _context.SaveChangesAsync();

            return Vendor;
        }
    }
}