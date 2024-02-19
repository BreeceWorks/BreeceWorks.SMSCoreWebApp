using BreeceWorks.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Twilio.Security;

namespace BreeceWorks.SMSCoreWebApi.Validation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateTwilioRequestAttribute : TypeFilterAttribute
    {
        public ValidateTwilioRequestAttribute() : base(typeof(ValidateTwilioRequestFilter))
        {
        }
    }

    internal class ValidateTwilioRequestFilter : IAsyncActionFilter
    {
        private readonly RequestValidator _requestValidator;
        private readonly bool _isEnabled;

        public ValidateTwilioRequestFilter(IConfigureService configureService)
        {
            Boolean isEnabledValue = false;
            var authToken = configureService.GetValue("Twilio:AuthToken") ?? throw new Exception("'Twilio:AuthToken' not configured.");
            _requestValidator = new RequestValidator(authToken);
            _isEnabled = Boolean.TryParse(configureService.GetValue("Twilio:RequestValidation:Enabled"), out isEnabledValue) ? isEnabledValue : false;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_isEnabled)
            {
                await next();
                return;
            }

            var httpContext = context.HttpContext;
            var request = httpContext.Request;

            var requestUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
            Dictionary<string, string> parameters = null;

            if (request.HasFormContentType)
            {
                var form = await request.ReadFormAsync(httpContext.RequestAborted).ConfigureAwait(false);
                parameters = form.ToDictionary(p => p.Key, p => p.Value.ToString());
            }

            var signature = request.Headers["X-Twilio-Signature"];
            var isValid = _requestValidator.Validate(requestUrl, parameters, signature);

            if (!isValid)
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            await next();
        }
    }
}
