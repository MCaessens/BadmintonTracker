using System;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Common.Dtos.Games;
using Imi.Project.Common.Dtos.Locations;
using Imi.Project.Common.Dtos.Rackets;
using Imi.Project.Common.Dtos.ShuttleCocks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Imi.Project.Api.Controllers
{
    [Authorize(Constants.UserPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        private readonly IMeService _meService;
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;

        public MeController(IMeService meService, IImageService imageService, UserManager<User> userManager)
        {
            _meService = meService;
            _imageService = imageService;
            _userManager = userManager;
        }

        // Games
        [HttpGet("games")]
        public async Task<IActionResult> GetMyGames([FromQuery] PageParameters pageParameters)
        {
            var response = await _meService.GetMyGamesAsync(pageParameters);
            return response;
        }

        [HttpGet("games/{id:guid}")]
        public async Task<IActionResult> GetGameById(Guid id)
        {
            var response = await _meService.GetGameByIdAsync(id);
            return response;
        }

        [HttpDelete("games/{id:guid}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var response = await _meService.DeleteGameAsync(id);
            return response;
        }

        [HttpPost("games")]
        public async Task<IActionResult> AddGame(GameRequestDto gameRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = await _meService.AddGameAsync(gameRequestDto);
            return response;
        }

        [HttpPut("games")]
        public async Task<IActionResult> UpdateGame(GameRequestDto gameRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _meService.UpdateGameAsync(gameRequestDto);
            return response;
        }

        // Locations
        [HttpGet("locations")]
        public async Task<IActionResult> GetMyLocations([FromQuery] PageParameters pageParameters)
        {
            var response = await _meService.GetMyLocationsAsync(pageParameters);
            return response;
        }

        [HttpGet("locations/{id}")]
        public async Task<IActionResult> GetLocationById(Guid id)
        {
            var response = await _meService.GetLocationByIdAsync(id);
            return response;
        }

        [HttpDelete("locations/{id}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            var response = await _meService.DeleteLocationAsync(id);
            return response;
        }

        [HttpPost("locations")]
        public async Task<IActionResult> AddLocation([FromForm] LocationRequestDto locationRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _meService.AddLocationAsync(locationRequestDto);
            return response;
        }

        [HttpPut("locations")]
        public async Task<IActionResult> UpdateLocation([FromForm] LocationRequestDto locationRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _meService.UpdateLocationAsync(locationRequestDto);
            return response;
        }

        // Rackets
        [HttpGet("rackets")]
        public async Task<IActionResult> GetMyRackets([FromQuery] PageParameters pageParameters)
        {
            var response = await _meService.GetMyRacketsAsync(pageParameters);
            return response;
        }

        [HttpGet("rackets/{id}")]
        public async Task<IActionResult> GetRacketById(Guid id)
        {
            var response = await _meService.GetRacketByIdAsync(id);
            return response;
        }

        [HttpDelete("rackets/{id}")]
        public async Task<IActionResult> DeleteRacket(Guid id)
        {
            var response = await _meService.DeleteRacketAsync(id);
            return response;
        }

        [HttpPost("rackets")]
        public async Task<IActionResult> AddRacket([FromForm] RacketRequestDto racketRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _meService.AddRacketAsync(racketRequestDto);
            return response;
        }

        [HttpPut("rackets")]
        public async Task<IActionResult> UpdateRacket([FromForm] RacketRequestDto racketRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _meService.UpdateRacketAsync(racketRequestDto);
            return response;
        }

        // ShuttleCocks
        [HttpGet("shuttlecocks")]
        public async Task<IActionResult> GetMyShuttleCocks([FromQuery] PageParameters pageParameters)
        {
            var response = await _meService.GetMyShuttleCocksAsync(pageParameters);
            return response;
        }

        [HttpGet("shuttlecocks/{id:guid}")]
        public async Task<IActionResult> GetShuttleCockId(Guid id)
        {
            var response = await _meService.GetShuttleCockByIdAsync(id);
            return response;
        }

        [HttpDelete("shuttlecocks/{id:guid}")]
        public async Task<IActionResult> DeleteShuttleCock(Guid id)
        {
            var response = await _meService.DeleteShuttleCockAsync(id);
            return response;
        }

        [HttpPost("shuttlecocks")]
        public async Task<IActionResult> AddShuttleCock([FromForm] ShuttleCockRequestDto shuttleCockRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _meService.AddShuttleCockAsync(shuttleCockRequestDto);
            return response;
        }

        [HttpPut("shuttlecocks")]
        public async Task<IActionResult> UpdateShuttleCock([FromForm] ShuttleCockRequestDto shuttleCockRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _meService.UpdateShuttleCockAsync(shuttleCockRequestDto);
            return response;
        }
    }
}