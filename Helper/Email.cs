using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Notification.Helper
{
    public class Email
    {

        public static async Task SendEmail(string to, string subject, string body, string apikey)
        {
            try
            {
                string apiKey = apikey;
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("noreply@myorg.com", "Alert Notification"),
                    Subject = subject,
                    HtmlContent = body,
                    ReplyTo = new EmailAddress("noreply@myorg.com")
                };
                msg.AddTo(new EmailAddress(to));
                await client.SendEmailAsync(msg);
            }
            catch
            {
                throw new Exception("Send email failed. Please try again.");
            }
        }

        public static async Task SendEmail(List<string> tos, string subject, string body, string apikey)
        {
            try
            {
                string apiKey = apikey;
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("noreply@myorg.com", "Alert Notification"),
                    Subject = subject,
                    HtmlContent = body,
                    ReplyTo = new EmailAddress("noreply@myorg.com")
                };

                foreach (string to in tos)
                {
                    msg.AddTo(new EmailAddress(to));
                }

                await client.SendEmailAsync(msg);
            }
            catch
            {
                throw new Exception("Send email failed. Please try again.");
            }
        }
        public static bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
