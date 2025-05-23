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
    public class ProductTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/ProductType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetById(int id)
        {
            var productType = await _context.Products.FindAsync(id);

            if (productType == null)
            {
                return NotFound();
            }

            return productType;
        }

        // POST: api/ProductType
        [HttpPost]
        public async Task<ActionResult<ProductType>> Create(ProductType productType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(productType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = productType.Id }, productType);
        }

        // PUT: api/ProductType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductType productType)
        {
            if (id != productType.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(productType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))
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

        // DELETE: api/ProductType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productType = await _context.Products.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductTypeExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}