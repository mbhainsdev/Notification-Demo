using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notification.Helper;
using Notification.Model;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Notification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationApi]
    public class NotificationController : ControllerBase
    {
        private IConfiguration configuration;
        public NotificationController(IConfiguration iConfig)
        {
            configuration = iConfig;

        }

        /// <summary>
        /// Generate - Alert Notification
        /// </summary>
        /// <param name="data">Notification Object</param>
        /// <response code="200">Notification Succesful</response>
        /// <response code="500">Unable to send notification. Please try again.</response>
        /// <returns>Status message about creation of delete alert rules job</returns>
        [HttpPost]
        public async Task<ContentResult> PostAsync([FromBody] Notify data)
        {
            try
            {
                string sgApiKey = configuration.GetValue<string>("Settings:sgKey");
                //string from = configuration.GetValue<string>("Settings:from");
                string webHookUrl = data.SlackWebhook;//configuration.GetValue<string>("Settings:webHookUrl");
                //Validate email
                foreach (string email in data.To.Split(','))
                {
                    if (!Email.IsValid(email)) { throw new Exception("Email is not valid"); }
                }
                if (data.To.Split(',').Count() > 1)
                {
                    var to = data.To.Split(',').ToList();
                    await Email.SendEmail(to, data.Subject, data.Message, sgApiKey);
                }
                else
                {
                    await Email.SendEmail(data.To, data.Subject, data.Message, sgApiKey);
                }

                // Slack Notify
                if (!string.IsNullOrEmpty(data.SlackWebhook))
                {
                    new Uri(data.SlackWebhook);
                    await Slack.SlackNotify(webHookUrl, data.Subject);
                }

                string jsonFileContent = "Notification created successfully";
                return new ContentResult()
                {
                    Content = JsonConvert.SerializeObject(jsonFileContent),
                    StatusCode = (int)HttpStatusCode.OK,
                    ContentType = "application/json",
                };


            }
            catch (Exception)
            {
                return new ContentResult()
                {
                    Content = JsonConvert.SerializeObject(new APIErrorModel() { ErrorMessage = "Unable to send notification. Please try again." }),
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ContentType = "application/json",
                };

            }
        }


    }
}
