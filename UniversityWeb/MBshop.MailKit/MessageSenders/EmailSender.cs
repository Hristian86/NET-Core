using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MBshop.MailKit.MessageSenders
{
    public class EmailSender : IEmailSender
    {
        private const string EmailFrom = "";
        private const string NameOfSender = "MBshop - verification";

        private readonly SendGridClient client;

        public EmailSender(string apiKey)
        {
            this.client = new SendGridClient(apiKey);
        }

        public async Task SendEmailAsync(string to, string subject, string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(subject) && string.IsNullOrWhiteSpace(htmlContent))
            {
                throw new ArgumentException("Subject and message should be provided.");
            }

            var fromAddress = new EmailAddress(EmailFrom, NameOfSender);
            var toAddress = new EmailAddress(to);
            var message = MailHelper.CreateSingleEmail(fromAddress, toAddress, subject, null, htmlContent);

            try
            {
                var response = await this.client.SendEmailAsync(message);
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Body.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}