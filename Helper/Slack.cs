using SlackBotMessages;
using SlackBotMessages.Models;
using System;
using System.Threading.Tasks;

namespace Notification.Helper
{
    public class Slack
    {
        public static async Task SlackNotify(string webHookUrl, string message, AppSettingsHelper appSettings)
        {

            try
            {
                var client = new SbmClient(webHookUrl);

                var slmessage = new Message(message).SetUserWithEmoji("Alert", Emoji.Loudspeaker);

                await client.Send(slmessage);
            }
            catch
            {
                throw new Exception("Slack notification failed. Please try again.");
            }

        }

    }

}
