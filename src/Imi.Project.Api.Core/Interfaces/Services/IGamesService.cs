using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Common.Dtos.Games;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IGamesService 
    {
        Task<IActionResult> ListAllAsync(PageParameters pageParameters);
        Task<IActionResult> GetByIdAsync(Guid id);
        Task<IActionResult> AddAsync(GameRequestDto game);
        Task<IActionResult> DeleteAsync(Guid id);
        Task<IActionResult> UpdateAsync(GameRequestDto game);
        Task<IActionResult> GetGamesByLocationId(Guid id, PageParameters pageParameters);
        Task<IActionResult> GetGamesByRacketId(Guid id, PageParameters pageParameters);
        Task<IActionResult> GetGamesByShuttleCockId(Guid id, PageParameters pageParameters);
        Task<IActionResult> GetGamesByUserId(Guid id, PageParameters pageParameters);
    }
}
