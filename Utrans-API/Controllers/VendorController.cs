using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
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
            return _context.Vendors.Where(b => b.Deleted_at == null).ToList();
        }

        // GET api/<VendorController>/5
        // api/Vendor/[ID]
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendors>> GetVendors(int id)
        {
            var Vendor = await _context.Vendors.Where(b => b.Deleted_at == null).FirstOrDefaultAsync(b => b.id == id); ;

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

            var vendor = new Vendors
            {
                Code = Vendor.Code,
                Name = Vendor.Name,
                Address = Vendor.Address,
                District = Vendor.District,
                City = Vendor.City,
                Phone = Vendor.Phone,
                Email = Vendor.Email,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
                Deleted_at = null
            };

            await _context.Vendors.AddAsync(vendor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                _context.Entry(vendor).State = EntityState.Detached;
                throw;
            }

            return CreatedAtAction(nameof(GetVendors), new { id = Vendor.id }, Vendor);
        }

        // PUT api/<VendorController>/5
        // api/Vendor/[ID]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, Vendors Vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingVendor = await _context.Vendors.FindAsync(id);

            if (existingVendor == null)
            {
                return NotFound($"Vendor with ID {id} not found");
            }

            existingVendor.Code = Vendor.Code;
            existingVendor.Name = Vendor.Name;
            existingVendor.Address = Vendor.Address;
            existingVendor.District = Vendor.District;
            existingVendor.City = Vendor.City;
            existingVendor.Phone = Vendor.Phone;
            existingVendor.Email = Vendor.Email;
            existingVendor.Updated_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
                {
                    return NotFound($"Vendor with ID {id} not found");
                }
                else
                {
                    _context.Entry(Vendor).State = EntityState.Detached;
                    throw;
                }
            }

            return Ok(new {message = "Vendor Updated"});
        }

        // DELETE api/<VendorController>/5
        // api/Vendor/[ID]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vendors>> DeleteVendor(int id)
        {   

            var existingVendor = await _context.Vendors.FindAsync(id);

            if (existingVendor == null)
            {
                return NotFound();
            }

            existingVendor.Deleted_at = DateTime.Now;

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

            return Ok(new {message = "Vendor Deleted"});
        }
    }
}