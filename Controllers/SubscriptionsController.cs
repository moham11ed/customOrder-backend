using customOrder.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace customOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        // POST: api/Subscriptions
        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                return BadRequest("Invalid email");

            var success = await _subscriptionService.SubscribeAsync(email);

            if (!success)
                return Conflict("Email already subscribed");

            return Ok("Subscription successful");
        }

        // GET: api/Subscriptions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subscriptions = await _subscriptionService.GetAllAsync();
            return Ok(subscriptions);
        }

        // DELETE: api/Subscriptions/{email}
        [HttpDelete("{email}")]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                return BadRequest("Invalid email");

            var removed = await _subscriptionService.UnsubscribeAsync(email);

            if (!removed)
                return NotFound("Email not found");

            return Ok("Unsubscribed successfully");
        }
    }
}
