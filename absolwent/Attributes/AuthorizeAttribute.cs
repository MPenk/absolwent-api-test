using absolwent.DTO;
using absolwent.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace absolwent.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (University)context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new Response { Error = true, StatusCode = 401, Message = "Brak autoryzacji" };

            }
        }
    }
}
