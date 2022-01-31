using Imi.Project.Common.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IAccountsService
    {
        Task<IActionResult> RegisterAsync(RegisterUserRequestDto userDto);
        Task<IActionResult> LoginAsync(LoginUserRequestDto userDto);
        Task<IActionResult> LogoutAsync();
    }
}
