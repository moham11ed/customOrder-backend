using customOrder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OilsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Oils
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OilOption>>> GetAll()
        {
            return await _context.OilOptions.ToListAsync();
        }

        // GET: api/Oils/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OilOption>> GetById(int id)
        {
            var oilOption = await _context.OilOptions.FindAsync(id);

            if (oilOption == null)
            {
                return NotFound(new { message = "Oil option not found" });
            }

            return oilOption;
        }

        // POST: api/Oils
        [HttpPost]
        public async Task<ActionResult<OilOption>> Create([FromBody] OilOption oilOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OilOptions.Add(oilOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = oilOption.Id }, oilOption);
        }

        // PUT: api/Oils/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OilOption oilOption)
        {
            if (id != oilOption.Id)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(oilOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OilOptionExists(id))
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

        // DELETE: api/Oils/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var oilOption = await _context.OilOptions.FindAsync(id);
            if (oilOption == null)
            {
                return NotFound(new { message = "Oil option not found" });
            }

            _context.OilOptions.Remove(oilOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Oils/search?name=lavender
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<OilOption>>> SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Search term cannot be empty" });
            }

            return await _context.OilOptions
                .Where(o => o.Name.Contains(name))
                .ToListAsync();
        }

        private bool OilOptionExists(int id)
        {
            return _context.OilOptions.Any(e => e.Id == id);
        }
    }
}