using customOrder.DTO;
using customOrder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace customOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(AdminLoginDto login)
        {
            var admin = _context.Admins.SingleOrDefault(a => a.Username == login.Username);
            if (admin == null || admin.PasswordHash != login.Password) // NOTE: Use real hashing in production
            {
                return Unauthorized();
            }

            return Ok(new { message = "Login successful" });
        }
    }
}