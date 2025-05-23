using customOrder.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customOrder.Service
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly AppDbContext _context;

        public SubscriptionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SubscribeAsync(string email)
        {
            if (await _context.Subscriptions.AnyAsync(s => s.Email == email))
                return false; // Already subscribed

            var subscription = new Subscription { Email = email };
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<string>> GetAllAsync()
        {
            return await _context.Subscriptions
                .Select(s => s.Email)
                .ToListAsync();
        }

        public async Task<bool> UnsubscribeAsync(string email)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.Email == email);

            if (subscription == null)
                return false;

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
