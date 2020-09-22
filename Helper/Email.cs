using Microsoft.Extensions.Configuration;
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
        public static IConfiguration configuration;
        public Email(IConfiguration iConfig)
        {
            configuration = iConfig;

        }
        public static async Task SendEmail(string to, string subject, string body, AppSettingsHelper appSettings)
        {
            try
            {
                string apiKey = appSettings.SgKey;
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(appSettings.From, appSettings.FromName),
                    Subject = subject,
                    HtmlContent = body,
                    ReplyTo = new EmailAddress(appSettings.ReplyTo)
                };
                msg.AddTo(new EmailAddress(to));
                await client.SendEmailAsync(msg);
            }
            catch(Exception e)
            {
                throw new Exception("Send email failed. Please try again.");
            }
        }

        public static async Task SendEmail(List<string> tos, string subject, string body, AppSettingsHelper appSettings)
        {
            try
            {
                string apiKey = appSettings.SgKey;
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(appSettings.From, appSettings.FromName),
                    Subject = subject,
                    HtmlContent = body,
                    ReplyTo = new EmailAddress(appSettings.ReplyTo)
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
