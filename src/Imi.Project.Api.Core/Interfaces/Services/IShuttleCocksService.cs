using Imi.Project.Api.Core.Entities;
using Imi.Project.Common.Dtos.ShuttleCocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IShuttleCocksService
    {
        Task<IActionResult> ListAllAsync(PageParameters pageParameters);
        Task<IActionResult> GetByIdAsync(Guid id);
        Task<IActionResult> AddAsync(ShuttleCockRequestDto shuttleCockRequestDto);
        Task<IActionResult> DeleteAsync(Guid id);
        Task<IActionResult> UpdateAsync(ShuttleCockRequestDto shuttleCockRequestDto);
        Task<IActionResult> GetShuttleCocksByUserId(Guid id, PageParameters pageParameters);
    }
}
