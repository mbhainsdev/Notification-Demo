using Newtonsoft.Json;

namespace Notification.Model
{
    public class Notify
    {

        /// <summary>
        /// Message / Body
        /// </summary>
        /// <example>Alert Performance issue</example>
        [JsonProperty("message", Required = Required.DisallowNull)]
        public string Message { get; set; }

        /// <summary>
        /// Mail to
        /// </summary>
        /// <example>xyz@org.com</example>
        [JsonProperty("to", Required = Required.Always)]
        public string To { get; set; }//= "manish.bhainsora@gmail.com";

        /// <summary>
        /// Email Subject
        /// </summary>
        /// <example>Alert test application unavailable</example>
        [JsonProperty("subject", Required = Required.Always)]
        public string Subject { get; set; }//= "Alert Notification";

        /// <summary>
        /// Slack webhook url
        /// </summary>
        /// <example>https://hooks.slack.com/services/T01fsfs3Z/B01dgfd/1nZWGsfadaD</example>
        [JsonProperty("slackwebhook")]
        public string SlackWebhook { get; set; }


        //[JsonProperty("from")]
        //public string From { get; set; }//= "noreply@tgtalert.com";
    }
}
