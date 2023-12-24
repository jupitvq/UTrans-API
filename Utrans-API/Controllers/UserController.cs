using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utrans_API.DBContexts;
using Utrans_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Utrans_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }

        // GET: api/<UserController>
        // /api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {   
            return _context.Users.Where(b => b.Deleted_at == null).ToList();
        }

        // GET api/<UserController>/5
        // api/User/[ID]
        // hash
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {   
            var User = await _context.Users.Where(b => b.Deleted_at == null).FirstOrDefaultAsync(b => b.id == id); ;

            if (User == null)
            {
                return NotFound();
            }

            return User;
            //return "value";
        }

        // POST api/<UserController>
        // api/User
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users User)
        {
            var brand = new Users
            {
                Code = User.Code,
                Name = User.Name,
                Number = User.Number,
                Email = User.Email,
                Website = User.Website,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            await _context.Users.AddAsync(brand);

            try
            {
                await _context.SaveChangesAsync();
            } catch (DbUpdateException)
            {
                _context.Entry(brand).State = EntityState.Detached;
                throw;
            }

            return CreatedAtAction(nameof(GetUsers), new { id = User.id }, User);
        }   

        // PUT api/<UserController>/5
        // api/User/[ID]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, Users User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found");
            }

            existingUser.Name = User.Name;
            existingUser.Number = User.Number;
            existingUser.Email = User.Email;
            existingUser.Website = User.Website;
            existingUser.Updated_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound($"User with ID {id} not found");
                }
                else
                {
                    _context.Entry(existingUser).State = EntityState.Detached;
                    throw;
                }
            }

            return Ok(new { message = "User updated" });
        }

        // DELETE api/<UserController>/5
        // api/User/[ID]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Deleted_at = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "User deleted" });
        }
    }
}
