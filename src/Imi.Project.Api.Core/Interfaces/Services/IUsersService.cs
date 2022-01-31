using Imi.Project.Api.Core.Entities;
using Imi.Project.Common.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IUsersService
    {
        Task<IActionResult> ListAllAsync(PageParameters pageParameters);
        Task<IActionResult> GetByIdAsync(Guid id);
        Task<IActionResult> DeleteAsync(Guid id);

    }
}
