using System;
using System.Threading.Tasks;
using Imi.Project.Mobile.Core.Entities;
using Imi.Project.Mobile.Core.Models;

namespace Imi.Project.Mobile.Infrastructure.Interfaces
{
    public interface IGamesService
    {
        Task<BaseApiModel<GameModel>> GetAllGamesAsync();
        Task<GameModel> AddGameAsync(GameModel gameModel);
        Task<GameModel> UpdateGameAsync(GameModel gameModel);
        Task<bool> DeleteGameAsync(Guid id);
    }
}