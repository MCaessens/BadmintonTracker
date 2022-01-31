using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Mapper;
using Imi.Project.Common.Dtos.Games;
using Imi.Project.Common.Dtos.Locations;
using Imi.Project.Common.Dtos.Rackets;
using Imi.Project.Common.Dtos.ShuttleCocks;
using Imi.Project.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Core.Services
{
    public class MeService : IMeService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IRacketRepository _racketRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IShuttleCockRepository _shuttleCockRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageService _imageService;

        public MeService(IGameRepository gameRepository,
            IRacketRepository racketRepository,
            ILocationRepository locationRepository,
            IShuttleCockRepository shuttleCockRepository,
            IHttpContextAccessor httpContextAccessor,
            IImageService imageService)
        {
            _gameRepository = gameRepository;
            _racketRepository = racketRepository;
            _locationRepository = locationRepository;
            _shuttleCockRepository = shuttleCockRepository;
            _httpContextAccessor = httpContextAccessor;
            _imageService = imageService;
        }

        #region Delete

        public async Task<IActionResult> DeleteGameAsync(Guid gameId)
        {
            var deleted = await _gameRepository.DeleteAsync(gameId);

            if (!deleted) return ServiceHelper.NotFound($"Game with id {gameId} could not be deleted");
            return ServiceHelper.Ok();
        }

        public async Task<IActionResult> DeleteLocationAsync(Guid locationId)
        {
            var deleted = await _locationRepository.DeleteAsync(locationId);

            if (!deleted) return ServiceHelper.NotFound($"Location with id {locationId} could not be deleted");
            return ServiceHelper.Ok();
        }

        public async Task<IActionResult> DeleteRacketAsync(Guid racketId)
        {
            var deleted = await _racketRepository.DeleteAsync(racketId);

            if (!deleted) return ServiceHelper.NotFound($"Racket with id {racketId} could not be deleted");
            return ServiceHelper.Ok();
        }

        public async Task<IActionResult> DeleteShuttleCockAsync(Guid shuttleCockId)
        {
            var deleted = await _shuttleCockRepository.DeleteAsync(shuttleCockId);

            if (!deleted) return ServiceHelper.NotFound($"Racket with id {shuttleCockId} could not be deleted");
            return ServiceHelper.Ok();
        }

        #endregion

        #region GetById

        public async Task<IActionResult> GetGameByIdAsync(Guid gameId)
        {
            var game = await _gameRepository.GetAll()
                .FirstOrDefaultAsync(g => g.Id == gameId && g.UserId == GetUserId());

            if (game is null) return ServiceHelper.NotFound($"Game with id: {gameId} was not found");
            return ServiceHelper.Ok(game.MapToDto());
        }

        public async Task<IActionResult> GetLocationByIdAsync(Guid locationId)
        {
            var location = await _locationRepository.GetAll()
                .FirstOrDefaultAsync(g => g.Id == locationId && g.UserId == GetUserId());

            if (location is null) return ServiceHelper.NotFound($"Location with id: {locationId} was not found");
            return ServiceHelper.Ok(location.MapToDto());
        }

        public async Task<IActionResult> GetRacketByIdAsync(Guid racketId)
        {
            var racket = await _racketRepository.GetAll()
                .FirstOrDefaultAsync(g => g.Id == racketId && g.UserId == GetUserId());

            if (racket is null) return ServiceHelper.NotFound($"Racket with id: {racketId} was not found");
            return ServiceHelper.Ok(racket.MapToDto());
        }

        public async Task<IActionResult> GetShuttleCockByIdAsync(Guid shuttleCockId)
        {
            var shuttleCock = await _racketRepository.GetAll()
                .FirstOrDefaultAsync(g => g.Id == shuttleCockId && g.UserId == GetUserId());

            if (shuttleCock is null) return ServiceHelper.NotFound($"ShuttleCock with id: {shuttleCockId} was not found");
            return ServiceHelper.Ok(shuttleCock.MapToDto());
        }

        #endregion

        #region GetAll

        public async Task<IActionResult> GetMyGamesAsync(PageParameters pageParameters)
        {
            var query = _gameRepository.GetAll().Where(g => g.UserId == GetUserId()).OrderBy(g => g.Id);
            var gamesCount = query.Count();
            var games = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            return ServiceHelper.Ok(games.MapToDto(gamesCount));
        }

        public async Task<IActionResult> GetMyLocationsAsync(PageParameters pageParameters)
        {
            var query = _locationRepository.GetAll().Where(l => l.UserId == GetUserId()).OrderBy(l => l.Id);
            var locationCount = query.Count();
            var locations = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            return ServiceHelper.Ok(locations.MapToDto(locationCount));
        }

        public async Task<IActionResult> GetMyRacketsAsync(PageParameters pageParameters)
        {
            var query = _racketRepository.GetAll().Where(r => r.UserId == GetUserId()).OrderBy(r => r.Id);
            var racketCount = query.Count();
            var rackets = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            return ServiceHelper.Ok(rackets.MapToDto(racketCount));
        }

        public async Task<IActionResult> GetMyShuttleCocksAsync(PageParameters pageParameters)
        {
            var query = _shuttleCockRepository.GetAll().Where(l => l.UserId == GetUserId()).OrderBy(l => l.Id);
            var shuttleCount = query.Count();
            var shuttleCocks = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            return ServiceHelper.Ok(shuttleCocks.MapToDto(shuttleCount));
        }

        #endregion

        #region Update

        public async Task<IActionResult> UpdateGameAsync(GameRequestDto gameRequestDto)
        {
            if (gameRequestDto.UserId != GetUserId()) return ServiceHelper.BadRequest();

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

        public async Task<IActionResult> UpdateLocationAsync(LocationRequestDto locationRequestDto)
        {
            if (locationRequestDto.UserId != GetUserId()) return ServiceHelper.BadRequest();
            if (!float.TryParse(locationRequestDto.Longitude, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var longitude))
                return ServiceHelper.BadRequest("Longitude must a number.");
            if (!float.TryParse(locationRequestDto.Latitude, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var latitude))
                return ServiceHelper.BadRequest("Latitude must a number.");

            var location = await _locationRepository.GetByIdAsync(locationRequestDto.Id);
            if (location == null) return ServiceHelper.NotFound();

            location.Street = locationRequestDto.Street;
            location.City = locationRequestDto.City;
            location.Name = locationRequestDto.Name;
            location.PostalCode = locationRequestDto.PostalCode;
            location.Longitude = longitude;
            location.Latitude = latitude;
            if (locationRequestDto.Image != null)
            {
                if (!locationRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest("Given file must be of type: Image");
                _imageService.RemoveImage(location.ImageUrl);
                location.ImageUrl = await _imageService.AddOrUpdateImageAsync<Location>(location.Id, locationRequestDto.Image);
            }

            var updatedLocation = await _locationRepository.UpdateAsync(location);
            if (updatedLocation is null) return ServiceHelper.BadRequest();

            return ServiceHelper.Ok(updatedLocation.MapToDto());
        }

        public async Task<IActionResult> UpdateRacketAsync(RacketRequestDto racketRequestDto)
        {
            if (racketRequestDto.UserId != GetUserId()) return ServiceHelper.BadRequest();
            if (!Enum.TryParse(racketRequestDto.RacketType, out RacketType racketType))
                return ServiceHelper.BadRequest(Constants.WrongRacketTypeGivenErrorMessage);

            var racket = await _racketRepository.GetByIdAsync(racketRequestDto.Id);
            if (racket == null) return ServiceHelper.NotFound();

            racket.Brand = racketRequestDto.Brand;
            racket.Model = racketRequestDto.Model;
            racket.RacketType = racketType;
            if (racketRequestDto.Image != null)
            {
                if (!racketRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest(Constants.MustBeImageErrorMessage);
                _imageService.RemoveImage(racket.ImageUrl);
                racket.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(racket.Id, racketRequestDto.Image);
            }

            var updatedRacket = await _racketRepository.UpdateAsync(racket);
            if (updatedRacket is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(updatedRacket.MapToDto());
        }

        public async Task<IActionResult> UpdateShuttleCockAsync(ShuttleCockRequestDto shuttleCockRequestDto)
        {
            if (shuttleCockRequestDto.UserId != GetUserId()) return ServiceHelper.BadRequest();
            if (!Enum.TryParse(shuttleCockRequestDto.ShuttleType, out ShuttleType shuttleType))
                return ServiceHelper.BadRequest(Constants.WrongShuttleTypeGivenErrorMessage);

            var shuttleCock = await _shuttleCockRepository.GetByIdAsync(shuttleCockRequestDto.Id);
            if (shuttleCock == null) return ServiceHelper.BadRequest();

            shuttleCock.Brand = shuttleCockRequestDto.Brand;
            shuttleCock.Model = shuttleCockRequestDto.Model;
            shuttleCock.ShuttleType = shuttleType;
            if (shuttleCockRequestDto.Image != null)
            {
                if (!shuttleCockRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest(Constants.MustBeImageErrorMessage);
                _imageService.RemoveImage(shuttleCock.ImageUrl);
                shuttleCock.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(shuttleCock.Id, shuttleCockRequestDto.Image);
            }

            var updatedShuttle = await _shuttleCockRepository.UpdateAsync(shuttleCock);
            if (updatedShuttle is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(updatedShuttle.MapToDto());
        }

        #endregion

        #region Add

        public async Task<IActionResult> AddGameAsync(GameRequestDto gameRequestDto)
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

            var game = new Game
            {
                Id = Guid.NewGuid(),
                LocationId = gameRequestDto.LocationId,
                RacketId = gameRequestDto.RacketId,
                UserId = GetUserId(),
                Opponent = gameRequestDto.Opponent,
                ShuttleCockId = gameRequestDto.ShuttleCockId,
                Score = gameRequestDto.Score,
                OpponentScore = gameRequestDto.OpponentScore,
            };

            var addedGame = await _gameRepository.AddAsync(game);
            if (addedGame is null) return ServiceHelper.BadRequest();

            return ServiceHelper.Created(nameof(AddGameAsync), addedGame.MapToDto());
        }

        public async Task<IActionResult> AddShuttleCockAsync(ShuttleCockRequestDto shuttleCockRequestDto)
        {
            if (!Enum.TryParse(shuttleCockRequestDto.ShuttleType, out ShuttleType shuttleType))
                return ServiceHelper.BadRequest(Constants.WrongShuttleTypeGivenErrorMessage);

            var shuttleCock = new ShuttleCock
            {
                Id = Guid.NewGuid(),
                Brand = shuttleCockRequestDto.Brand,
                Model = shuttleCockRequestDto.Model,
                ShuttleType = shuttleType,
                UserId = GetUserId()
            };
            if (shuttleCockRequestDto.Image != null)
            {
                if (!shuttleCockRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest(Constants.MustBeImageErrorMessage);
                shuttleCock.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(shuttleCock.Id, shuttleCockRequestDto.Image);
            }
            else shuttleCock.ImageUrl = "";

            var addedShuttle = await _shuttleCockRepository.AddAsync(shuttleCock);
            if (addedShuttle is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(addedShuttle.MapToDto());
        }

        public async Task<IActionResult> AddRacketAsync(RacketRequestDto racketRequestDto)
        {
            if (!Enum.TryParse(racketRequestDto.RacketType, out RacketType racketType))
                return ServiceHelper.BadRequest(Constants.WrongRacketTypeGivenErrorMessage);

            var racket = new Racket
            {
                Id = Guid.NewGuid(),
                Brand = racketRequestDto.Brand,
                Model = racketRequestDto.Model,
                RacketType = racketType,
                UserId = GetUserId(),
            };
            if (racketRequestDto.Image != null)
            {
                if (!racketRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest();
                racket.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(racket.Id, racketRequestDto.Image);
            }
            else racket.ImageUrl = "";

            var addedRacket = await _racketRepository.AddAsync(racket);
            if (addedRacket is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(addedRacket.MapToDto());
        }

        public async Task<IActionResult> AddLocationAsync(LocationRequestDto locationRequestDto)
        {
            if (!float.TryParse(locationRequestDto.Longitude, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var longitude))
                return ServiceHelper.BadRequest("Longitude must a number.");
            if (!float.TryParse(locationRequestDto.Latitude, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var latitude))
                return ServiceHelper.BadRequest("Latitude must a number.");

            var location = new Location
            {
                Id = Guid.NewGuid(),
                UserId = GetUserId(),
                Street = locationRequestDto.Street,
                City = locationRequestDto.City,
                Name = locationRequestDto.Name,
                PostalCode = locationRequestDto.PostalCode,
                Longitude = longitude,
                Latitude = latitude
            };
            if (locationRequestDto.Image != null)
            {
                if (!locationRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest("Given file must be of type: Image");
                location.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(location.Id, locationRequestDto.Image);
            }
            else location.ImageUrl = "";

            var addedLocation = await _locationRepository.AddAsync(location);
            if (addedLocation is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(addedLocation.MapToDto());
        }

        #endregion

        private Guid GetUserId()
        {
            var result = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == CustomClaimTypes.NameIdentifier).Value);
            return result;
        }
    }
}