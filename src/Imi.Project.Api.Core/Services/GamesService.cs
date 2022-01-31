using System;
using System.Linq;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Mapper;
using Imi.Project.Common.Dtos.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Core.Services
{
    public class GamesService : IGamesService
    {
        private readonly IGameRepository _gameRepository;

        public GamesService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<IActionResult> AddAsync(GameRequestDto gameRequestDto)
        {
            var game = new Game
            {
                LocationId = gameRequestDto.LocationId,
                RacketId = gameRequestDto.RacketId,
                UserId = gameRequestDto.UserId,
                Opponent = gameRequestDto.Opponent,
                ShuttleCockId = gameRequestDto.ShuttleCockId,
                Score = gameRequestDto.Score,
                OpponentScore = gameRequestDto.OpponentScore,
            };

            var addedGame = await _gameRepository.AddAsync(game);
            if (addedGame is null) ServiceHelper.BadRequest();
            return ServiceHelper.Ok(addedGame.MapToDto());
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleted = await _gameRepository.DeleteAsync(id);

            if (!deleted) return ServiceHelper.BadRequest($"Game with id {id} could not be deleted");
            return ServiceHelper.Ok();
        }

        public async Task<IActionResult> ListAllAsync(PageParameters pageParameters)
        {
            var games = await _gameRepository.ListAllAsync(pageParameters);

            if (!games.Any()) return ServiceHelper.NotFound();
            return ServiceHelper.Ok(games.MapToDto(_gameRepository.GetItemCount()));
        }

        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if (game is null) return ServiceHelper.NotFound($"Game with Id: {id} was not found. Please try again.");
            return ServiceHelper.Ok(game.MapToDto());
        }

        public async Task<IActionResult> UpdateAsync(GameRequestDto gameRequestDto)
        {
            if (gameRequestDto.Score > 21 || gameRequestDto.OpponentScore > 21)
            {
                if (gameRequestDto.Score < 20 || gameRequestDto.OpponentScore < 20)
                    return ServiceHelper.BadRequest("Game is finished or invalid score");
                if (gameRequestDto.Score >= 20 || gameRequestDto.OpponentScore >= 20)
                {
                    if (Math.Abs(gameRequestDto.Score - gameRequestDto.OpponentScore) > 2)
                        return ServiceHelper.BadRequest("Game is finished or invalid score");
                }
            }

            var game = await _gameRepository.GetByIdAsync(gameRequestDto.Id);

            game.LocationId = gameRequestDto.LocationId;
            game.Opponent = gameRequestDto.Opponent;
            game.OpponentScore = gameRequestDto.OpponentScore;
            game.RacketId = gameRequestDto.RacketId;
            game.Score = gameRequestDto.Score;
            game.ShuttleCockId = gameRequestDto.ShuttleCockId;

            var updatedGame = await _gameRepository.UpdateAsync(game);
            if (updatedGame is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(updatedGame.MapToDto());
        }

        public async Task<IActionResult> GetGamesByLocationId(Guid locationId, [FromQuery] PageParameters pageParameters)
        {
            var query = _gameRepository.GetAll().OrderBy(g => g.Id).Where(g => g.LocationId == locationId);
            var gamesCount = query.Count();
            var games = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            if (!games.Any()) return ServiceHelper.NotFound($"There were no games with with locationId: {locationId}");
            return ServiceHelper.Ok(games.MapToDto(gamesCount));
        }

        public async Task<IActionResult> GetGamesByRacketId(Guid racketId, [FromQuery] PageParameters pageParameters)
        {
            var query = _gameRepository.GetAll().OrderBy(g => g.Id).Where(g => g.RacketId == racketId);
            var gamesCount = query.Count();
            var games = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            if (!games.Any()) return ServiceHelper.NotFound($"There were no games with with racketId: {racketId}");
            return ServiceHelper.Ok(games.MapToDto(gamesCount));
        }

        public async Task<IActionResult> GetGamesByShuttleCockId(Guid shuttleCockId, [FromQuery] PageParameters pageParameters)
        {
            var query = _gameRepository.GetAll().OrderBy(g => g.Id).Where(g => g.ShuttleCockId == shuttleCockId);
            var gamesCount = query.Count();
            var games = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            if (!games.Any()) return ServiceHelper.NotFound($"There were no games with with shuttleCockId: {shuttleCockId}");
            return ServiceHelper.Ok(games.MapToDto(gamesCount));
        }

        public async Task<IActionResult> GetGamesByUserId(Guid userId, [FromQuery] PageParameters pageParameters)
        {
            var query = _gameRepository.GetAll().OrderBy(g => g.Id).Where(g => g.UserId == userId);
            var gamesCount = query.Count();
            var games = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            if (!games.Any()) return ServiceHelper.NotFound($"There were no games with with userId: {userId}");
            return ServiceHelper.Ok(games.MapToDto(gamesCount));
        }
    }
}