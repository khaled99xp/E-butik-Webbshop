using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "Authorization";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var adminApiKey = config["AdminApiKey"];
            string providedApiKey = null;
            if (context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var potentialKey))
            {
                providedApiKey = potentialKey.FirstOrDefault();
            }
            if (string.IsNullOrEmpty(providedApiKey))
            {

                providedApiKey = context.HttpContext.Request.Query["token"].FirstOrDefault();
            }
            if (providedApiKey != adminApiKey)
            {
                context.Result = new JsonResult(new { message = "Not Authorized" }) { StatusCode = 401 };
                return;
            }
            await next();
        }
    }
}
