using Notification.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notification.Helper
{

    public interface INotifcator
    {
        public System.Threading.Tasks.Task SendNotificationAsync(Notify msg, AppSettingsHelper appSettings);
    }

    public class EmailNotification : INotifcator
    {
        public async System.Threading.Tasks.Task SendNotificationAsync(Notify data, AppSettingsHelper appSettings)
        {
            //Validate email
            if (!string.IsNullOrEmpty(data.To))
            {
                foreach (string email in data.To.Split(','))
                {
                    if (!Email.IsValid(email)) { throw new Exception("Email is not valid"); }
                }
                if (data.To.Split(',').Count() > 1)
                {
                    var to = data.To.Split(',').ToList();
                    await Email.SendEmail(to, data.Subject, data.Message, appSettings);
                }
                else
                {
                    await Email.SendEmail(data.To, data.Subject, data.Message, appSettings);
                }
            }
        }
    }

    public class SlacklNotification : INotifcator
    {
        public async System.Threading.Tasks.Task SendNotificationAsync(Notify data, AppSettingsHelper appSettings)
        {
            try
            {
                if (!string.IsNullOrEmpty(data.SlackWebhook))
                {
                    new Uri(data.SlackWebhook);
                    await Slack.SlackNotify(data.SlackWebhook, data.Subject, appSettings);
                }
            }
            catch
            {
                throw new Exception("Send Slack notification. Please try again.");
            }
        }
    }

    public class NotifcationLocator
    {
        private static NotifcationLocator _Locator;

        private Dictionary<string, INotifcator> _Services;
        public NotifcationLocator()
        {
            //Build Service registry dynamically use unity framework
            _Services = new Dictionary<string, INotifcator>();
            _Services.Add("email", new EmailNotification());
            _Services.Add("slack", new SlacklNotification());


        }

        public async System.Threading.Tasks.Task NotifyAllAsync(Notify msg, AppSettingsHelper appSettings)
        {
            try
            {
                foreach (INotifcator notify in _Services.Values)
                {
                   await notify.SendNotificationAsync(msg, appSettings);
                }
            }
            catch
            {
                throw new Exception("Send email failed. Please try again.");
            }

        }



        public static NotifcationLocator GetInstance()
        {

            if (_Locator == null)
                _Locator = new NotifcationLocator();

            return _Locator;

        }
    }
}
