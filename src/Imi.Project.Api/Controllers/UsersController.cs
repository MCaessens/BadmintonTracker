using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Imi.Project.Common.Dtos.Users;
using Imi.Project.Api.Core.Mapper;

namespace Imi.Project.Api.Controllers
{
    [Authorize(Constants.AdminPolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILocationsService _locationsService;
        private readonly IRacketsService _racketsService;
        private readonly IShuttleCocksService _shuttleCocksService;
        private readonly IGamesService _gamesService;
        public UsersController(IUsersService usersService,
                               ILocationsService locationsService,
                               IRacketsService racketsService,
                               IShuttleCocksService shuttleCocksService,
                               IGamesService gamesService)
        {
            _usersService = usersService;
            _locationsService = locationsService;
            _racketsService = racketsService;
            _shuttleCocksService = shuttleCocksService;
            _gamesService = gamesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParameters pageParameters)
        {
            var response = await _usersService.ListAllAsync(pageParameters);
            return response;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _usersService.GetByIdAsync(id);
            return response;
        }
        [HttpGet("{id:guid}/games")]
        public async Task<IActionResult> GetGamesByUserId(Guid id, [FromQuery] PageParameters pageParameters)
        {
            var response = await _gamesService.GetGamesByUserId(id, pageParameters);
            return response;
        }
        [HttpGet("{id:guid}/locations")]
        public async Task<IActionResult> GetLocationsByUserId(Guid id, [FromQuery] PageParameters pageParameters)
        {
            var response = await _locationsService.GetLocationsByUserId(id, pageParameters);
            return response;
        }
        [HttpGet("{id:guid}/rackets")]
        public async Task<IActionResult> GetRacketsByUserId(Guid id, [FromQuery] PageParameters pageParameters)
        {
            var response = await _racketsService.GetRacketsByUserId(id, pageParameters);
            return response;
        }
        [HttpGet("{id:guid}/shuttlecocks")]
        public async Task<IActionResult> GetShuttleCocksByUserId(Guid id, [FromQuery] PageParameters pageParameters)
        {
            var response = await _shuttleCocksService.GetShuttleCocksByUserId(id, pageParameters);
            return response;
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _usersService.GetByIdAsync(id);
            return response;
        }
    }
}
