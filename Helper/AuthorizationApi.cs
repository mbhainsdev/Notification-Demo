using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Notification.Helper
{
    public class AuthorizationApi : Attribute, IAsyncActionFilter
    {

        private const string ApiKeyHeaderName = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var givenKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var apiKey = "mystrongapikey";
            if (!apiKey.Equals(givenKey))
            {
                context.Result = new UnauthorizedResult();
                return;

            }
            await next();

        }
    }
}
