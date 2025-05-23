using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using customOrder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShapeWithDesignsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShapeWithDesignsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ShapeWithDesigns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShapeWithDesign>>> GetShapeWithDesigns()
        {
            return await _context.ShapeWithDesigns
                .Include(s => s.BottleDesign)
                .Include(s => s.LogoDesign)
                .ToListAsync();
        }

        // GET: api/ShapeWithDesigns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShapeWithDesign>> GetShapeWithDesign(int id)
        {
            var shapeWithDesign = await _context.ShapeWithDesigns
                .Include(s => s.BottleDesign)
                .Include(s => s.LogoDesign)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (shapeWithDesign == null)
            {
                return NotFound();
            }

            return shapeWithDesign;
        }

        // POST: api/ShapeWithDesigns
        [HttpPost]
        public async Task<ActionResult<ShapeWithDesign>> CreateShapeWithDesign(ShapeWithDesign shapeWithDesign)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify that the referenced designs exist
            var bottleDesignExists = await _context.BottleDesigns.AnyAsync(b => b.Id == shapeWithDesign.ShapeId);
            var logoDesignExists = await _context.LogoDesigns.AnyAsync(l => l.Id == shapeWithDesign.DesignId);

            if (!bottleDesignExists || !logoDesignExists)
            {
                return BadRequest("One or more referenced designs do not exist");
            }

            _context.ShapeWithDesigns.Add(shapeWithDesign);
            await _context.SaveChangesAsync();

            // Return the created entity with its navigation properties loaded
            var result = await _context.ShapeWithDesigns
                .Include(s => s.BottleDesign)
                .Include(s => s.LogoDesign)
                .FirstOrDefaultAsync(s => s.Id == shapeWithDesign.Id);

            return CreatedAtAction(nameof(GetShapeWithDesign), new { id = shapeWithDesign.Id }, result);
        }

        // PUT: api/ShapeWithDesigns/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShapeWithDesign(int id, ShapeWithDesign shapeWithDesign)
        {
            if (id != shapeWithDesign.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify that the referenced designs exist
            var bottleDesignExists = await _context.BottleDesigns.AnyAsync(b => b.Id == shapeWithDesign.ShapeId);
            var logoDesignExists = await _context.LogoDesigns.AnyAsync(l => l.Id == shapeWithDesign.DesignId);

            if (!bottleDesignExists || !logoDesignExists)
            {
                return BadRequest("One or more referenced designs do not exist");
            }

            _context.Entry(shapeWithDesign).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShapeWithDesignExists(id))
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

        // DELETE: api/ShapeWithDesigns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShapeWithDesign(int id)
        {
            var shapeWithDesign = await _context.ShapeWithDesigns.FindAsync(id);
            if (shapeWithDesign == null)
            {
                return NotFound();
            }

            _context.ShapeWithDesigns.Remove(shapeWithDesign);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/ShapeWithDesigns/ByShapeAndDesign?shapeId=1&designId=2
        [HttpGet("ByShapeAndDesign/{shapeId}/{designId}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ShapeWithDesign>>> GetByShapeAndDesign(int shapeId, int designId)
        {
            var shapeExists = await _context.BottleDesigns.AnyAsync(b => b.Id == shapeId);
            var designExists = await _context.LogoDesigns.AnyAsync(l => l.Id == designId);

            if (!shapeExists || !designExists)
            {
                return NotFound("Shape or design not found");
            }

            var result = await _context.ShapeWithDesigns
                .Where(s => s.ShapeId == shapeId && s.DesignId == designId)
                .Include(s => s.BottleDesign)
                .Include(s => s.LogoDesign)
                .ToListAsync();

            return Ok(result);
        }


        private bool ShapeWithDesignExists(int id)
        {
            return _context.ShapeWithDesigns.Any(e => e.Id == id);
        }
    }
}