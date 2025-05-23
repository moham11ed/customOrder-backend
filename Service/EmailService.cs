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
            var smtpClient = new SmtpClient(_config["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_config["EmailSettings:SmtpPort"]),
                Credentials = new NetworkCredential(
                    _config["EmailSettings:SmtpUser"],
                    _config["EmailSettings:SmtpPass"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:SmtpUser"]),
                Subject = $"Order Confirmation - #{orderId}",
                Body = $"Your order #{orderId} has been successfully received.",
                IsBodyHtml = false
            };

            mailMessage.To.Add(toEmail);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
