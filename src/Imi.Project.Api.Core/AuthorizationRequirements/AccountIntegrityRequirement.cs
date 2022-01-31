using Microsoft.AspNetCore.Authorization;

namespace Imi.Project.Api.Core.AuthorizationRequirements
{
    public class AccountIntegrityRequirement : IAuthorizationRequirement
    {
        public AccountIntegrityRequirement()
        {
        }
    }
}
