using Microsoft.AspNetCore.Mvc;
using customOrder.Service;

namespace customOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // POST: api/Email/SendOrderConfirmation
        [HttpPost("SendOrderConfirmation")]
        public async Task<IActionResult> SendOrderConfirmation([FromBody] OrderConfirmationRequest request)
        {
            try
            {
                await _emailService.SendOrderConfirmationAsync(request.ToEmail, request.OrderId);
                return Ok(new { Message = "Email sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Failed to send email: {ex.Message}" });
            }
        }

        // POST: api/Email/SendOrderStatusUpdate
        [HttpPost("SendOrderStatusUpdate")]
        public async Task<IActionResult> SendOrderStatusUpdate([FromBody] OrderStatusUpdateRequest request)
        {
            try
            {
                await _emailService.SendOrderStatusUpdateAsync(request.ToEmail, request.OrderId, request.NewStatus);
                return Ok(new { Message = "Status update email sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Failed to send status update email: {ex.Message}" });
            }
        }

        // POST: api/Email/SendBulkEmail
        [HttpPost("SendBulkEmail")]
        public async Task<IActionResult> SendBulkEmail([FromBody] BulkEmailRequest request)
        {
            try
            {
                await _emailService.SendBulkEmailAsync(request.ToEmails, request.Subject, request.Body);
                return Ok(new { Message = "Bulk emails sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Failed to send bulk emails: {ex.Message}" });
            }
        }
    }

    public class OrderConfirmationRequest
    {
        public string ToEmail { get; set; }
        public int OrderId { get; set; }
    }

    public class OrderStatusUpdateRequest
    {
        public string ToEmail { get; set; }
        public int OrderId { get; set; }
        public string NewStatus { get; set; }
    }

    public class BulkEmailRequest
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}