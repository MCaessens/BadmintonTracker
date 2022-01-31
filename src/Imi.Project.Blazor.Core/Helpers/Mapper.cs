using System;
using System.Collections.Generic;
using System.Linq;
using Imi.Project.Blazor.Core.Entities.Games;
using Imi.Project.Common.Dtos.Games;
using Imi.Project.Common.Dtos.Locations;
using Imi.Project.Common.Dtos.Rackets;
using Imi.Project.Common.Dtos.ShuttleCocks;
using Imi.Project.Common.Enums;

namespace Imi.Project.Blazor.Core.Helpers
{
    public static class Mapper
    {
        // MapToModel
        public static GameModel MapToModel(this GameResponseDto game)
        {
            var model = new GameModel
            {
                Id = game.Id,
                LocationId = game.LocationId,
                Opponent = game.Opponent,
                OpponentScore = game.OpponentScore,
                RacketId = game.RacketId,
                Score = game.Score,
                ShuttleCockId = game.ShuttleCockId,
                Status = game.Status,
                UserId = game.UserId,
                UserName = game.UserName,
                Winner = game.Winner
            };
            return model;
        }

        public static IEnumerable<GameModel> MapToModel(this IEnumerable<GameResponseDto> games)
        {
            return games.Select(g => g.MapToModel());
        }

        public static BaseApiModel<GameModel> MapToModel(this BaseApiModel<GameResponseDto> games)
        {
            var model = new BaseApiModel<GameModel>
            {
                Info = games.Info,
                Results = games.Results.MapToModel(),
                Succeeded = games.Succeeded
            };
            return model;
        }

        public static LocationModel MapToModel(this LocationResponseDto location)
        {
            var model = new LocationModel
            {
                Id = location.Id,
                City = location.City,
                ImageUrl = location.ImageUrl,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Name = location.Name,
                PostalCode = location.PostalCode,
                Street = location.Street,
                UserId = (Guid) location.UserId
            };
            return model;
        }

        public static IEnumerable<LocationModel> MapToModel(this IEnumerable<LocationResponseDto> locations)
        {
            return locations.Select(l => l.MapToModel());
        }

        public static BaseApiModel<LocationModel> MapToModel(this BaseApiModel<LocationResponseDto> locations)
        {
            var model = new BaseApiModel<LocationModel>
            {
                Info = locations.Info,
                Results = locations.Results.MapToModel(),
                Succeeded = locations.Succeeded
            };
            return model;
        }

        public static RacketModel MapToModel(this RacketResponseDto racket)
        {
            var model = new RacketModel
            {
                Id = racket.Id,
                Brand = racket.Brand,
                ImageUrl = racket.ImageUrl,
                Model = racket.Model,
                RacketType = (RacketType) Enum.Parse(typeof(RacketType), racket.RacketType),
                UserId = racket.UserId,
            };
            return model;
        }

        public static IEnumerable<RacketModel> MapToModel(this IEnumerable<RacketResponseDto> rackets)
        {
            return rackets.Select(r => r.MapToModel());
        }

        public static BaseApiModel<RacketModel> MapToModel(this BaseApiModel<RacketResponseDto> rackets)
        {
            var model = new BaseApiModel<RacketModel>
            {
                Info = rackets.Info,
                Results = rackets.Results.MapToModel(),
                Succeeded = rackets.Succeeded
            };
            return model;
        }

        public static ShuttleCockModel MapToModel(this ShuttleCockResponseDto shuttleCock)
        {
            var model = new ShuttleCockModel
            {
                Brand = shuttleCock.Brand,
                Id = shuttleCock.Id,
                ImageUrl = shuttleCock.ImageUrl,
                Model = shuttleCock.Model,
                ShuttleType = (ShuttleType) Enum.Parse(typeof(ShuttleType), shuttleCock.ShuttleType),
                UserId = shuttleCock.UserId,
            };
            return model;
        }

        public static IEnumerable<ShuttleCockModel> MapToModel(this IEnumerable<ShuttleCockResponseDto> shuttleCocks)
        {
            return shuttleCocks.Select(sc => sc.MapToModel());
        }

        public static BaseApiModel<ShuttleCockModel> MapToModel(this BaseApiModel<ShuttleCockResponseDto> shuttleCocks)
        {
            var model = new BaseApiModel<ShuttleCockModel>
            {
                Info = shuttleCocks.Info,
                Results = shuttleCocks.Results.MapToModel(),
                Succeeded = shuttleCocks.Succeeded,
            };
            return model;
        }

        public static GameRequestDto MapToRequest(this GameModel game)
        {
            var request = new GameRequestDto
            {
                Id = game.Id,
                LocationId = game.LocationId,
                Opponent = game.Opponent,
                OpponentScore = game.OpponentScore,
                RacketId = game.RacketId,
                Score = game.Score,
                ShuttleCockId = game.ShuttleCockId,
                UserId = game.UserId,
            };
            return request;
        }

        //public static LocationApiRequest MapToRequest(this LocationModel locationModel, FileStream image)
        //{
        //    var request = new LocationApiRequest
        //    {
        //        Id = locationModel.Id,
        //        City = locationModel.City,
        //        Image = image,
        //        Latitude = locationModel.Latitude,
        //        Longitude = locationModel.Longitude,
        //        Name = locationModel.Name,
        //        PostalCode = locationModel.PostalCode,
        //        Street = locationModel.Street,
        //        UserId = locationModel.UserId
        //    };
        //    return request;
        //}
    }
}