using customOrder.Models;
using customOrder.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace customOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TranslationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{language}")]
        public async Task<IActionResult> GetTranslationsForLanguage(string language)
        {
            var translations = await _context.Translations
                .Include(t => t.Items)
                .ToListAsync();

            var result = translations.ToDictionary(
                t => t.Key,
                t => t.Items.FirstOrDefault(i => i.LanguageCode == language)?.Value
                      ?? t.Items.FirstOrDefault(i => i.LanguageCode == "en")?.Value
                      ?? string.Empty
            );

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateTranslation([FromBody] TranslationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var translation = await _context.Translations
                .Include(t => t.Items)
                .FirstOrDefaultAsync(t => t.Key == dto.Key);

            if (translation == null)
            {
                translation = new Translation { Key = dto.Key };
                _context.Translations.Add(translation);
            }

            foreach (var item in dto.Values)
            {
                var existingItem = translation.Items.FirstOrDefault(i => i.LanguageCode == item.Key);
                if (existingItem != null)
                {
                    existingItem.Value = item.Value;
                }
                else
                {
                    translation.Items.Add(new TranslationItem
                    {
                        LanguageCode = item.Key,
                        Value = item.Value
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}