using Microsoft.AspNetCore.Mvc;
using customOrder.DTO;
using customOrder.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace customOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Admin/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDto loginDto)
        {
            
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
                return BadRequest("Username and password are required.");

            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == loginDto.Username &&
                                          a.PasswordHash == loginDto.Password);

            if (admin == null)
                return Unauthorized("Invalid credentials");

            
            return Ok(new { Message = "Login successful" });
        }

        // POST: api/Admin/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AdminLoginDto registerDto)
        {
            
            if (string.IsNullOrEmpty(registerDto.Username) || string.IsNullOrEmpty(registerDto.Password))
                return BadRequest("Username and password are required.");

            
            if (await _context.Admins.AnyAsync(a => a.Username == registerDto.Username))
                return BadRequest("Username already exists");

            
            var admin = new Admin
            {
                Username = registerDto.Username,
                PasswordHash = registerDto.Password 
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Admin created successfully" });
        }

        // GET: api/Admin
        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _context.Admins
                .Select(a => new { a.Id, a.Username })
                .ToListAsync();

            return Ok(admins);
        }

        // DELETE: api/Admin/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Admin deleted successfully" });
        }
    }
}