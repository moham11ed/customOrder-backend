using customOrder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace customOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoDesignsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogoDesignsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LogoDesigns
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.LogoDesigns.ToList());
        }

        // GET: api/LogoDesigns/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var logo = _context.LogoDesigns.Find(id);
            if (logo == null)
            {
                return NotFound();
            }
            return Ok(logo);
        }

        // POST: api/LogoDesigns
        [HttpPost]
        public IActionResult Create([FromBody] LogoDesign logo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LogoDesigns.Add(logo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = logo.Id }, logo);
        }

        // PUT: api/LogoDesigns/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LogoDesign updatedLogo)
        {
            if (id != updatedLogo.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingLogo = _context.LogoDesigns.Find(id);
            if (existingLogo == null)
            {
                return NotFound();
            }

            // Update properties
            _context.Entry(existingLogo).CurrentValues.SetValues(updatedLogo);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/LogoDesigns/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var logo = _context.LogoDesigns.Find(id);
            if (logo == null)
            {
                return NotFound();
            }

            _context.LogoDesigns.Remove(logo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
