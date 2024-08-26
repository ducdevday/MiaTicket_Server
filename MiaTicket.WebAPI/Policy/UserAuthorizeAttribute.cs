using MiaTicket.Data.Enum;
using Microsoft.AspNetCore.Authorization;

namespace MiaTicket.WebAPI.Policy
{
    public class UserAuthorizeAttribute : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
    {
        public Role[] RequireRoles { get; set; }
        public bool RequireEmailActived { get; set; }
        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            yield return this;
        }
    }
}
