using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings EmailSettings { get; }
        public ILogger<EmailService> Logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            this.EmailSettings = emailSettings.Value;
            this.Logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {
            SendGridClient client = new SendGridClient(EmailSettings.ApiKey);

            string subject = email.Subject;
            EmailAddress to = new EmailAddress(email.To);
            string emailBody = email.Body;

            EmailAddress from = new EmailAddress
            {
                Email = EmailSettings.FromAddress,
                Name = EmailSettings.FromName
            };

            SendGridMessage sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            Response response = await client.SendEmailAsync(sendGridMessage);

            Logger.LogInformation("Email sent.");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            Logger.LogError("Email sending failed.");

            return false;
        }
    }
}
