using Imi.Project.Wpf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Wpf.Core.Interfaces
{
    public interface IAuthService
    {
        Task<BaseApiModel<LoginModel>> LoginAsync(LoginModel loginRequest);
        Task<bool> LogoutAsync();
    }
}
