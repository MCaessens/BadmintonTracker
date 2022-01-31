using Imi.Project.Api.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Imi.Project.Common.Enums;
using Imi.Project.Common.Dtos.Rackets;
using Imi.Project.Api.Core.Mapper;

namespace Imi.Project.Api.Controllers
{
    [Authorize(Constants.AdminPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class RacketsController : ControllerBase
    {
        private readonly IRacketsService _racketsService;
        private readonly IGamesService _gamesService;
        private readonly IImageService _imageService;
        public RacketsController(IRacketsService racketsService, IGamesService gamesService, IImageService imageService)
        {
            _racketsService = racketsService;
            _gamesService = gamesService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParameters pageParameters)
        {
            var response = await _racketsService.ListAllAsync(pageParameters);
            return response;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _racketsService.GetByIdAsync(id);
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
            var response = await _racketsService.DeleteAsync(id);
            return response;
        }
        [HttpPost]
        public async Task<IActionResult> AddRacket([FromForm] RacketRequestDto racketRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _racketsService.AddAsync(racketRequestDto);
            return response;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRacket([FromForm] RacketRequestDto racketRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _racketsService.UpdateAsync(racketRequestDto);
            return response;
        }
    }
}
