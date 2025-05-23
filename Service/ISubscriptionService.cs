namespace customOrder.Service
{
    public interface ISubscriptionService
    {
        Task<bool> SubscribeAsync(string email);
        Task<IEnumerable<string>> GetAllAsync(); 
        Task<bool> UnsubscribeAsync(string email);
    }
}
