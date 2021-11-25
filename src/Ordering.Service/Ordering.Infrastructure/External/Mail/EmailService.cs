using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using System.Net;
using System.Net.Mail;

namespace Ordering.Infrastructure.External.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _settingsEmail { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _settingsEmail = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {
            return await SendMail(email);
        }

        private async Task<bool> SendMail(Email email)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(_settingsEmail.Email);
                mail.To.Add(email.To);

                mail.Subject = email.Subject;
                mail.Body = email.Body;

                using (SmtpClient smtpClient = new SmtpClient(_settingsEmail.Domain, _settingsEmail.Port))
                {
                    smtpClient.Credentials = new NetworkCredential(_settingsEmail.Email, _settingsEmail.Password);
                    smtpClient.EnableSsl = true;
                    await smtpClient.SendMailAsync(mail);
                    _logger.LogInformation($"Date Time: ${DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ff")}\n\n Email has been sent.");
                }

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Email failed because: ${ex.StackTrace}");
                return false;
            }
        }
    }
}
