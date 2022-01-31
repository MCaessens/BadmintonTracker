using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Imi.Project.Api.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IShuttleCockRepository _shuttleCockRepository;
        private readonly IRacketRepository _racketRepository;
        public UsersService(IUserRepository userRepository,
                            IGameRepository gameRepository,
                            ILocationRepository locationRepository,
                            IShuttleCockRepository shuttleCockRepository,
                            IRacketRepository racketRepository)
        {
            _userRepository = userRepository;
            _gameRepository = gameRepository;
            _locationRepository = locationRepository;
            _shuttleCockRepository = shuttleCockRepository;
            _racketRepository = racketRepository;
        }
        public async Task<bool> AddAsync(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            List<bool> results = new List<bool>
            {
                await _gameRepository.DeleteMultipleByUserIdAsync(id),
                await _locationRepository.DeleteMultipleByUserIdAsync(id),
                await _shuttleCockRepository.DeleteMultipleByUserIdAsync(id),
                await _racketRepository.DeleteMultipleByUserIdAsync(id),
                await _userRepository.DeleteAsync(id)
            };
            var deleted = results.All(b => b == true);
            if (!deleted) return ServiceHelper.NotFound();
            return ServiceHelper.Ok();
        }

        public async Task<IActionResult> ListAllAsync(PageParameters pageParameters)
        {
            var users = await _userRepository.ListAllAsync(pageParameters);

            if (!users.Any()) return ServiceHelper.NotFound();
            return ServiceHelper.Ok(users.MapToDto(_userRepository.GetItemCount()));
        }

        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null) return ServiceHelper.NotFound();
            return ServiceHelper.Ok(user.MapToDto());
        }

        public async Task<bool> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }
    }
}
