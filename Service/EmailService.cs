using System.Net.Mail;
using System.Net;

namespace customOrder.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOrderConfirmationAsync(string toEmail, int orderId)
        {
            await SendEmailAsync(toEmail,
                $"Order Confirmation - #{orderId}",
                $"Your order #{orderId} has been successfully received.");
        }

        public async Task SendOrderStatusUpdateAsync(string toEmail, int orderId, string newStatus)
        {
            await SendEmailAsync(toEmail,
                $"Order Status Update - #{orderId}",
                $"The status of your order #{orderId} has been updated to: {newStatus}.");
        }

        public async Task SendBulkEmailAsync(List<string> toEmails, string subject, string body)
        {
            using var smtpClient = CreateSmtpClient();

            foreach (var email in toEmails)
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_config["EmailSettings:SmtpUser"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(email);
                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using var smtpClient = CreateSmtpClient();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:SmtpUser"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mailMessage.To.Add(toEmail);
            await smtpClient.SendMailAsync(mailMessage);
        }

        private SmtpClient CreateSmtpClient()
        {
            return new SmtpClient(_config["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_config["EmailSettings:SmtpPort"]),
                Credentials = new NetworkCredential(
                    _config["EmailSettings:SmtpUser"],
                    _config["EmailSettings:SmtpPass"]),
                EnableSsl = true
            };
        }
    }
}