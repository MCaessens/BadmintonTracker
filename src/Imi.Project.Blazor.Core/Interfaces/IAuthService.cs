using System.Threading.Tasks;
using Imi.Project.Blazor.Core.Entities.Games;

namespace Imi.Project.Blazor.Core.Interfaces
{
    public interface IAuthService
    {
        Task<BaseApiModel<LoginModel>> LoginAsync(LoginModel loginRequest);
        Task<bool> LogoutAsync();
    }
}
