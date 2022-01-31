using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Imi.Project.Common.Dtos.Locations;
using Imi.Project.Api.Core.Mapper;

namespace Imi.Project.Api.Controllers
{
    [Authorize(Constants.AdminPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService _locationsService;
        private readonly IGamesService _gamesService;
        private readonly IImageService _imageService;

        public LocationsController(ILocationsService locationsService, IGamesService gamesService, IImageService imageService)
        {
            _locationsService = locationsService;
            _gamesService = gamesService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParameters pageParameters)
        {
            var response = await _locationsService.ListAllAsync(pageParameters);
            return response;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _locationsService.GetByIdAsync(id);
            return response;
        }
        [HttpGet("{id:guid}/games")]
        public async Task<IActionResult> GetByGamesByLocationId(Guid id, [FromQuery] PageParameters pageParameters)
        {
            var response = await _gamesService.GetGamesByLocationId(id, pageParameters);
            return response;
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _locationsService.DeleteAsync(id);
            return response;
        }
        [HttpPost]
        public async Task<IActionResult> AddLocation([FromForm] LocationRequestDto locationRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _locationsService.AddAsync(locationRequestDto);
            return response;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateLocation([FromForm] LocationRequestDto locationRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _locationsService.UpdateAsync(locationRequestDto);
            return response;
        }
    }
}
