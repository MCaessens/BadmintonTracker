using Imi.Project.Mobile.Core.Entities;
using Imi.Project.Mobile.Core.Models;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Infrastructure.Interfaces
{
    public interface IAuthService
    {
        Task<BaseApiModel<LoginModel>> LoginAsync(LoginModel loginRequest);
        Task<bool> RegisterAsync(RegisterModel registerApiRequest);
        Task<bool> LogoutAsync();
    }
}
