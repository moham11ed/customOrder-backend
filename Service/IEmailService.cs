namespace customOrder.Service
{
    public interface IEmailService
    {
        Task SendOrderConfirmationAsync(string toEmail, int orderId);
    }
}
