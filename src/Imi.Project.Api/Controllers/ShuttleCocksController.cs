using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Imi.Project.Common.Enums;
using Imi.Project.Common.Dtos.ShuttleCocks;
using Imi.Project.Api.Core.Mapper;

namespace Imi.Project.Api.Controllers
{
    [Authorize(Constants.AdminPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class ShuttleCocksController : ControllerBase
    {
        private readonly IShuttleCocksService _shuttleCocksService;
        private readonly IGamesService _gamesService;
        private readonly IImageService _imageService;

        public ShuttleCocksController(IShuttleCocksService shuttleCocksService, IGamesService gamesService, IImageService imageService)
        {
            _shuttleCocksService = shuttleCocksService;
            _gamesService = gamesService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParameters pageParameters)
        {
            var response = await _shuttleCocksService.ListAllAsync(pageParameters);
            return response;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _shuttleCocksService.GetByIdAsync(id);
            return response;
        }
        [HttpGet("{id:guid}/games")]
        public async Task<IActionResult> GetGamesByRacketId(Guid id, [FromQuery] PageParameters pageParameters)
        {
            var response = await _gamesService.GetGamesByRacketId(id, pageParameters);
            return response;
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _shuttleCocksService.DeleteAsync(id);
            return response;
        }
        [HttpPost]
        public async Task<IActionResult> AddShuttleCock([FromForm] ShuttleCockRequestDto shuttleCockRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _shuttleCocksService.AddAsync(shuttleCockRequestDto);
            return response;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateShuttleCock([FromForm] ShuttleCockRequestDto shuttleCockRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _shuttleCocksService.UpdateAsync(shuttleCockRequestDto);
            return response;
        }
    }
}
 