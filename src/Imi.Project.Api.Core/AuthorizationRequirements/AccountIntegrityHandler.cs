using Imi.Project.Api.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.AuthorizationRequirements
{
    public class AccountIntegrityHandler : AuthorizationHandler<AccountIntegrityRequirement>
    {
        private readonly UserManager<User> _userManager;

        public AccountIntegrityHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AccountIntegrityRequirement requirement)
        {

            var accountIdentityIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.AccountIntegrityId);
            var emailClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (emailClaim != null)
            {
                var user = await _userManager.FindByEmailAsync(emailClaim.Value);
                if (user != null)
                {
                    if (user.AccountIntegrityId.ToString() == accountIdentityIdClaim.Value) context.Succeed(requirement);
                }
            }
        }
    }
}
