using Microsoft.AspNetCore.Authorization;

namespace MiaTicket.WebAPI.Policy
{
    public class UserAuthorizeHandler : AuthorizationHandler<UserAuthorizeAttribute>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAuthorizeAttribute requirement)
        {
            var role = context.User.Claims.FirstOrDefault(x => x.Type == "role")?.Value;
            if (role == null)
            {
                context.Fail();
            }
            else
            {
                bool isValidRole = requirement.RequireRoles.Contains(role);
                if (isValidRole) context.Succeed(requirement);
                else context.Fail();
            }
        }
    }

}
