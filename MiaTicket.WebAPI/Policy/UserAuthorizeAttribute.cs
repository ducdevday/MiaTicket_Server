using Microsoft.AspNetCore.Authorization;

namespace MiaTicket.WebAPI.Policy
{
    public class UserAuthorizeAttribute : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
    {
        public string[] RequireRoles { get; set; }
        public bool RequireEmailActived { get; set; }
        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            yield return this;
        }
    }
}
