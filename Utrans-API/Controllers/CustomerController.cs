using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utrans_API.DBContexts;
using Utrans_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Utrans_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {   
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.id == id);
        }

        // GET: api/<CustomerController>
        // api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            return await _context.Customers.Where(b => b.Deleted_at == null).ToListAsync();
        }

        // GET api/<CustomerController>/5
        // api/Customer/[ID]
        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> GetCustomers(int id)
        {
            var Customer = await _context.Customers.Where(b => b.Deleted_at == null).FirstOrDefaultAsync(b => b.id == id); ;

            if (Customer == null)
            {
                return NotFound();
            }

            return Customer;
        }

        // POST api/<CustomerController>
        // api/Customer
        [HttpPost]
        public async Task<ActionResult<Customers>> PostCustomers(Customers Customer)
        {   

            var customer = new Customers
            {
                Code = Customer.Code,
                Name = Customer.Name,
                Address = Customer.Address,
                District = Customer.District,
                City = Customer.City,
                Phone = Customer.Phone,
                Email = Customer.Email,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };

            await _context.Customers.AddAsync(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                _context.Entry(customer).State = EntityState.Detached;
                throw;
            }

            return CreatedAtAction(nameof(GetCustomers), new { id = Customer.id }, Customer);
        }

        // PUT api/<CustomerController>/5
        // api/Customer/[ID]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customers Customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
            {
                return NotFound($"Brand with ID {id} not found");
            }

            existingCustomer.Code = Customer.Code;
            existingCustomer.Name = Customer.Name;
            existingCustomer.Address = Customer.Address;
            existingCustomer.District = Customer.District;
            existingCustomer.City = Customer.City;
            existingCustomer.Phone = Customer.Phone;
            existingCustomer.Email = Customer.Email;
            existingCustomer.Updated_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound($"Brand with ID {id} not found");
                }
                else
                {
                    _context.Entry(existingCustomer).State = EntityState.Detached;
                    throw;
                }
            }

            return Ok(new { message = "Customer Updated" });
        }

        // DELETE api/<CustomerController>/5
        // api/Customer/[ID]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customers>> DeleteCustomer(int id)
        {
            
            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Deleted_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                
            }

            return Ok(new {message = "Customer Deleted"});
        }
    }
}
