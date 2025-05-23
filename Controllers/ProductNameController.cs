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
    public class ProductNameController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductNameController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductName
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductName>>> GetAll()
        {
            return await _context.ProductNames.ToListAsync();
        }

        // GET: api/ProductName/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductName>> GetById(int id)
        {
            var productName = await _context.ProductNames.FindAsync(id);

            if (productName == null)
            {
                return NotFound();
            }

            return productName;
        }

        // POST: api/ProductName
        [HttpPost]
        public async Task<ActionResult<ProductName>> Create(ProductName productName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductNames.Add(productName);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = productName.Id }, productName);
        }

        // PUT: api/ProductName/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductName productName)
        {
            if (id != productName.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(productName).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductNameExists(id))
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

        // DELETE: api/ProductName/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productName = await _context.ProductNames.FindAsync(id);
            if (productName == null)
            {
                return NotFound();
            }

            _context.ProductNames.Remove(productName);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductNameExists(int id)
        {
            return _context.ProductNames.Any(e => e.Id == id);
        }
    }
}