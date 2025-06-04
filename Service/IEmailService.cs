namespace customOrder.Service
{
    public interface IEmailService
    {
        Task SendOrderConfirmationAsync(string toEmail, int orderId);
        Task SendOrderStatusUpdateAsync(string toEmail, int orderId, string newStatus);
        Task SendBulkEmailAsync(List<string> toEmails, string subject, string body);
    }
}