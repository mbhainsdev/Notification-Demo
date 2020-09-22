using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Notification.Helper;
using Notification.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Notification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationApi]
    public class NotificationController : ControllerBase
    {

        private readonly AppSettingsHelper _appsettings;
        public NotificationController(IOptions<AppSettingsHelper> appsettings)
        {
            _appsettings = appsettings.Value;
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
                await NotifcationLocator.GetInstance().NotifyAllAsync(data, _appsettings);

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
