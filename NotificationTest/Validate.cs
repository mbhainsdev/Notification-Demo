using System;
using Xunit;
using Notification.Model;
using Notification.Helper;

namespace NotificationTest
{
    public class Validate
    {
        [Fact]
        public async void ValidateEmail()
        {
            Notify data = new Notify()
            {
                Message = "Message",
                SlackWebhook = "somerandom",
                To = "Some Random",
                Subject = "Hi"
            };

            AppSettingsHelper appsettings = new AppSettingsHelper();

            var obj = new Notification.Helper.EmailNotification();
            try
            {
                await obj.SendNotificationAsync(data, appsettings);
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }

        }

        [Fact]
        public async void ValidateUri()
        {
            Notify data = new Notify()
            {
                Message = "Hi",
                SlackWebhook = "https://mysalackwebhook",
                To = "xyz@org.com",
                Subject = "Hi"
            };
            AppSettingsHelper appsettings = new AppSettingsHelper();

            var obj = new Notification.Helper.SlacklNotification();
            try
            {
                await obj.SendNotificationAsync(data, appsettings);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }

        }
    }
}
