using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;


namespace BreeceWorks.Shared.CustomAuthorization
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            // Implement authorization logic  
            if (IsAuthorizationSuccessful(requirement))
            {
                // Authorization passed  
                context.Succeed(requirement);
            }
            else
            {
                // Authorization failed  
                context.Fail();
            }

            // Return completed task  
            return Task.CompletedTask;
        }

        private Boolean IsAuthorizationSuccessful(CustomAuthorizationRequirement requirement)
        {
            // This is where you would do authorization logic
            return true;
        }
    }
}
