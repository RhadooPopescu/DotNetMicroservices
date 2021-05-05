using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    //This class handles the email sending feature for when the customers complete an order.
    public class EmailService : IEmailService
    {
        //Fields.
        public EmailSettings EmailSettings { get; }
        public ILogger<EmailService> Logger { get; }

        //Constructor.
        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            this.EmailSettings = emailSettings.Value;
            this.Logger = logger;
        }

        //This method is used to send an email to the customer and is following the SendGrid implementation.
        public async Task<bool> SendEmail(Email email)
        {
            //Creating the sendgrid client with the api key.
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

            Logger.LogInformation("Email was successfully sent.");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            Logger.LogError("Email sending has failed.");

            return false;
        }
    }
}
