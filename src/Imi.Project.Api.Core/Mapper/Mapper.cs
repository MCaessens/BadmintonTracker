using System;
using System.Collections.Generic;
using System.Linq;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Common.Dtos;
using Imi.Project.Common.Dtos.Games;
using Imi.Project.Common.Dtos.Locations;
using Imi.Project.Common.Dtos.Rackets;
using Imi.Project.Common.Dtos.ShuttleCocks;
using Imi.Project.Common.Dtos.Users;
using Microsoft.AspNetCore.Http;

namespace Imi.Project.Api.Core.Mapper
{
    public static class Mapper
    {
        public static GameResponseDto MapToDto(this Game game)
        {
            var gameDto = new GameResponseDto
            {
                Id = game.Id,
                Opponent = game.Opponent,
                OpponentScore = game.OpponentScore,
                Score = game.Score,
                LocationId = game.LocationId,
                RacketId = game.RacketId,
                ShuttleCockId = game.ShuttleCockId,
                UserId = (Guid)game.UserId,
                UserName = $"{game.User.FirstName} {game.User.LastName}"
            };
            if (gameDto.Score >= 21 || gameDto.OpponentScore >= 21)
            {
                if (gameDto.Score <= 19 || gameDto.OpponentScore <= 19)
                {
                    gameDto.Status = "Finished";
                    if (gameDto.Score > gameDto.OpponentScore) gameDto.Winner = gameDto.UserName;
                    else gameDto.Winner = gameDto.Opponent;
                }
                if (gameDto.Score >= 20 || gameDto.OpponentScore >= 20)
                {
                    if (Math.Abs(gameDto.Score - gameDto.OpponentScore) >= 2)
                    {
                        gameDto.Status = "Finished";
                        if (gameDto.Score > gameDto.OpponentScore) gameDto.Winner = gameDto.UserName;
                        else gameDto.Winner = gameDto.Opponent;
                    }
                    else
                    {
                        gameDto.Winner = "TBA";
                        gameDto.Status = "In Progress";
                    }
                }
            }
            else
            {
                gameDto.Winner = "TBA";
                gameDto.Status = "In Progress";
            }
            return gameDto;
        }

        public static MultipleItemDto<GameResponseDto> MapToDto(this IEnumerable<Game> games, int gamesCount)
        {
            var info = CreatePageInfo(gamesCount, games.Count());
            var result = new MultipleItemDto<GameResponseDto>
            {
                Info = info,
                Results = games.Select(g => g.MapToDto())
            };
            return result;
        }

        public static LocationResponseDto MapToDto(this Location location)
        {
            return new LocationResponseDto
            {
                Id = location.Id,
                City = location.City,
                PostalCode = location.PostalCode,
                Street = location.Street,
                ImageUrl = GetFullImageUrl(location.ImageUrl),
                Name = location.Name,
                UserId = (Guid)location.UserId,
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }
        public static MultipleItemDto<LocationResponseDto> MapToDto(this IEnumerable<Location> locations, int locationsCount)
        {
            var info = CreatePageInfo(locationsCount, locations.Count());
            var result = new MultipleItemDto<LocationResponseDto>
            {
                Info = info,
                Results = locations.Select(g => g.MapToDto())
            };
            return result;
        }

        public static RacketResponseDto MapToDto(this Racket racket)
        {
            return new RacketResponseDto
            {
                Id = racket.Id,
                Brand = racket.Brand,
                ImageUrl = GetFullImageUrl(racket.ImageUrl),
                Model = racket.Model,
                RacketType = racket.RacketType.ToString(),
                UserId = (Guid)racket.UserId
            };
        }
        public static MultipleItemDto<RacketResponseDto> MapToDto(this IEnumerable<Racket> rackets, int racketCount)
        {
            var info = CreatePageInfo(racketCount, rackets.Count());
            var result = new MultipleItemDto<RacketResponseDto>
            {
                Info = info,
                Results = rackets.Select(g => g.MapToDto())
            };
            return result;
        }
        public static ShuttleCockResponseDto MapToDto(this ShuttleCock shuttleCock)
        {
            return new ShuttleCockResponseDto
            {
                Id = shuttleCock.Id,
                Brand = shuttleCock.Brand,
                ImageUrl = GetFullImageUrl(shuttleCock.ImageUrl),
                Model = shuttleCock.Model,
                ShuttleType = shuttleCock.ShuttleType.ToString(),
                UserId = (Guid)shuttleCock.UserId
            };
        }
        public static MultipleItemDto<ShuttleCockResponseDto> MapToDto(this IEnumerable<ShuttleCock> shuttleCocks, int shuttleCount)
        {
            var info = CreatePageInfo(shuttleCount, shuttleCocks.Count());
            var result = new MultipleItemDto<ShuttleCockResponseDto>
            {
                Info = info,
                Results = shuttleCocks.Select(g => g.MapToDto())
            };
            return result;
        }

        public static UserResponseDto MapToDto(this User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                EmailAddress = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
        public static MultipleItemDto<UserResponseDto> MapToDto(this IEnumerable<User> users, int userCount)
        {
            var info = CreatePageInfo(userCount, users.Count());
            var result = new MultipleItemDto<UserResponseDto>
            {
                Info = info,
                Results = users.Select(g => g.MapToDto())
            };
            return result;
        }
        private static string GetFullImageUrl(string image)
        {
            if (string.IsNullOrEmpty(image))
            {
                return null;
            }

            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();

            var scheme = httpContextAccessor.HttpContext.Request.Scheme;
            var url = httpContextAccessor.HttpContext.Request.Host.Value;

            var fullImageUrl = $"{scheme}://{url}/{image.Replace("\\", "/")}";

            return fullImageUrl;
        }
        private static InfoDto CreatePageInfo(int baseEntityCount, int entityCount)
        {
            return new InfoDto
            {
                PageAmount = (int)Math.Ceiling(baseEntityCount/ (float)Constants.PageSize),
                ItemCount = entityCount
            };
        }
    }
}
