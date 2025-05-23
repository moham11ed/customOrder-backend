namespace customOrder.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; } = DateTime.UtcNow;
    }
}
