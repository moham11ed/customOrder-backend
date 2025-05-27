using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace customOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ImagesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Create uploads directory if it doesn't exist
            var uploadsPath = Path.Combine(_environment.ContentRootPath, "wwwroot/Uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            // Generate unique filename
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the file path or URL
            var fileUrl = $"{Request.Scheme}://{Request.Host}/Uploads/{uniqueFileName}";
            return Ok(new { fileUrl });
        }

        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var uploadsPath = Path.Combine(_environment.ContentRootPath, "Uploads");
            var filePath = Path.Combine(uploadsPath, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var imageFileStream = System.IO.File.OpenRead(filePath);
            return File(imageFileStream, "image/jpeg"); // Adjust content type as needed
        }
    }
}
