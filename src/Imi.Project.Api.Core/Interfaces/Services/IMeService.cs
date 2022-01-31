using System;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Common.Dtos.Games;
using Imi.Project.Common.Dtos.Locations;
using Imi.Project.Common.Dtos.Rackets;
using Imi.Project.Common.Dtos.ShuttleCocks;
using Microsoft.AspNetCore.Mvc;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IMeService
    {
        Task<IActionResult> GetMyGamesAsync(PageParameters pageParameters);
        Task<IActionResult> GetMyShuttleCocksAsync(PageParameters pageParameters);
        Task<IActionResult> GetMyRacketsAsync(PageParameters pageParameters);
        Task<IActionResult> GetMyLocationsAsync(PageParameters pageParameters);

        Task<IActionResult> GetGameByIdAsync(Guid gameId);
        Task<IActionResult> GetShuttleCockByIdAsync(Guid shuttleCockId);
        Task<IActionResult> GetRacketByIdAsync(Guid racketId);
        Task<IActionResult> GetLocationByIdAsync(Guid locationId);

        Task<IActionResult> DeleteGameAsync(Guid gameId);
        Task<IActionResult> DeleteShuttleCockAsync(Guid shuttleCockId);
        Task<IActionResult> DeleteRacketAsync(Guid racketId);
        Task<IActionResult> DeleteLocationAsync(Guid locationId);

        Task<IActionResult> UpdateGameAsync(GameRequestDto gameRequestDto);
        Task<IActionResult> UpdateShuttleCockAsync(ShuttleCockRequestDto shuttleCockRequestDto);
        Task<IActionResult> UpdateRacketAsync(RacketRequestDto racketRequestDto);
        Task<IActionResult> UpdateLocationAsync(LocationRequestDto locationRequestDto);

        Task<IActionResult> AddGameAsync(GameRequestDto gameRequestDto);
        Task<IActionResult> AddShuttleCockAsync(ShuttleCockRequestDto shuttleCockRequestDto);
        Task<IActionResult> AddRacketAsync(RacketRequestDto racketRequestDto);
        Task<IActionResult> AddLocationAsync(LocationRequestDto locationRequestDto);
    }
}