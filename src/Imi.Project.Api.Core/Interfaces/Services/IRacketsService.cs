using Imi.Project.Api.Core.Entities;
using Imi.Project.Common.Dtos.Rackets;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IRacketsService
    {
        Task<IActionResult> ListAllAsync(PageParameters pageParameters);
        Task<IActionResult> GetByIdAsync(Guid id);
        Task<IActionResult> AddAsync(RacketRequestDto racketRequestDto);
        Task<IActionResult> DeleteAsync(Guid id);
        Task<IActionResult> UpdateAsync(RacketRequestDto racketRequestDto);
        Task<IActionResult> GetRacketsByUserId(Guid id, PageParameters pageParameters);
    }
}
