using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Mapper;
using Imi.Project.Common.Dtos.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Imi.Project.Api.Controllers
{
    [Authorize(Constants.AdminPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;
        public GamesController(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParameters pageParameters)
        {
            var response = await _gamesService.ListAllAsync(pageParameters);
            return response;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _gamesService.GetByIdAsync(id);
            return response;
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _gamesService.DeleteAsync(id);
            return response;
        }
        [HttpPost]
        public async Task<IActionResult> AddGame(GameRequestDto gameRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _gamesService.AddAsync(gameRequestDto);
            return response;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGame(GameRequestDto gameRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _gamesService.UpdateAsync(gameRequestDto);
            return response;
        }
    }
}
