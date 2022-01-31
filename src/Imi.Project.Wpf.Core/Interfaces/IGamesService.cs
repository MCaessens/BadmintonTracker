using Imi.Project.Wpf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Wpf.Core.Interfaces
{
    public interface IGamesService
    {
        Task<BaseApiModel<GameModel>> GetAllGamesAsync();
        Task<GameModel> AddGameAsync(GameModel gameModel);
        Task<GameModel> UpdateGameAsync(GameModel gameModel);
        Task<bool> DeleteGameAsync(Guid id);
        Task<GameModel> GetGameByIdAsync(Guid guid);
    }
}
