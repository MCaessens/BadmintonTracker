using Imi.Project.Blazor.Core.Entities.Games;
using Imi.Project.Blazor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imi.Project.Blazor.Core.Services
{
    public class MockGamesService : IGamesService
    {
        private static List<GameModel> Games { get; set; } = new List<GameModel>
            {
                new GameModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Opponent = "Louis Caessens",
                    RacketId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    UserName = "Marco Caessens",
                    Status = "IN PROGRESS"
                },
                new GameModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Opponent = "Felien Braeckevelt",
                    RacketId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    UserName = "Marco Caessens",
                    Status = "IN PROGRESS"
                },
                new GameModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Opponent = "Filip Bruyr",
                    RacketId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    UserName = "Marco Caessens",
                    Status = "IN PROGRESS"
                },
                new GameModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Opponent = "Amber Lippens",
                    RacketId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    UserName = "Marco Caessens",
                    Status = "IN PROGRESS"
                },
                new GameModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Opponent = "Stefaan Turpyn",
                    RacketId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    UserName = "Marco Caessens",
                    Status = "IN PROGRESS"
                },
                new GameModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    Opponent = "Wesley Caessens",
                    RacketId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    UserName = "Marco Caessens",
                    Status = "IN PROGRESS"
                },
                new GameModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    Opponent = "Louis Caessens",
                    RacketId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    UserName = "Marco Caessens",
                    Status = "IN PROGRESS"
                },
                new GameModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    Opponent = "Tine Franchois",
                    RacketId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    UserName = "Marco Caessens",
                    Status = "IN PROGRESS"
                },
                new GameModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    Opponent = "Tine Franchois",
                    RacketId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    UserName = "Marco Caessens",
                    Status = "IN PROGRESS"
                },
                new GameModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    Opponent = "John Doe",
                    RacketId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    ShuttleCockId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    UserName = "Marco Caessens",
                    Status = "IN PROGRESS"
                }
            };
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var gameModelToRemove = Games.SingleOrDefault(g => g.Id == id);
            await Task.FromResult(Games.Remove(gameModelToRemove));
            return true;
        }

        public async Task<IEnumerable<GameModel>> ListAllAsync()
        {
            return await Task.FromResult(Games.ToList());
        }

        public async Task<GameModel> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(Games.SingleOrDefault(g => g.Id == id));
        }

        public async Task<GameModel> UpdateAsync(GameModel dto)
        {
            if (dto.Score < 0 || dto.Score > 30 || dto.OpponentScore < 0 || dto.OpponentScore > 30) return null;
            
            var gameToDelete = Games.FirstOrDefault(g => g.Id == dto.Id);
            if (gameToDelete == null) return null;
            
            gameToDelete.LocationId = dto.LocationId;
            gameToDelete.Opponent = dto.Opponent;
            gameToDelete.OpponentScore = dto.OpponentScore;
            gameToDelete.RacketId = dto.RacketId;
            gameToDelete.ShuttleCockId = dto.ShuttleCockId;
            gameToDelete.Score = dto.Score;
            gameToDelete.Status = dto.Status;
            
            await Task.FromResult(Games.Remove(gameToDelete));
            Games.Add(gameToDelete);
            
            return dto;
        }

        public async Task<GameModel> AddGameAsync(GameModel gameModel)
        {
            if (gameModel.Score < 0 || gameModel.Score > 30 || gameModel.OpponentScore < 0 || gameModel.OpponentScore > 30) return null;

            gameModel.Id = Guid.NewGuid();
            gameModel.UserName = "Marco Caessens";

            await Task.Delay(100); 
            Games.Add(gameModel);

            return gameModel;
        }
    }
}
