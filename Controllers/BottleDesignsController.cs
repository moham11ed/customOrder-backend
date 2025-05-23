using customOrder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace customOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BottleDesignsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BottleDesignsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BottleDesigns
        [HttpGet]
        public IActionResult GetAll()
        {
            var designs = _context.BottleDesigns.ToList();
            return Ok(designs);
        }

        // GET: api/BottleDesigns/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var design = _context.BottleDesigns.Find(id);
            if (design == null)
                return NotFound();

            return Ok(design);
        }

        // POST: api/BottleDesigns
        [HttpPost]
        public IActionResult Create([FromBody] BottleDesign design)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.BottleDesigns.Add(design);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = design.Id }, design);
        }

        // PUT: api/BottleDesigns/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BottleDesign updatedDesign)
        {
            if (id != updatedDesign.Id)
                return BadRequest("ID mismatch");

            var existing = _context.BottleDesigns.Find(id);
            if (existing == null)
                return NotFound();

            _context.Entry(existing).CurrentValues.SetValues(updatedDesign);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/BottleDesigns/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var design = _context.BottleDesigns.Find(id);
            if (design == null)
                return NotFound();

            _context.BottleDesigns.Remove(design);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
