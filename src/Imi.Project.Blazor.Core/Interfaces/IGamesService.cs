using Imi.Project.Blazor.Core.Entities.Games;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Blazor.Core.Interfaces
{
    public interface IGamesService
    {
        Task<IEnumerable<GameModel>> ListAllAsync();
        Task<GameModel> GetByIdAsync(Guid id);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<GameModel> UpdateAsync(GameModel model);
        Task<GameModel> AddGameAsync(GameModel gameModel);
    }
}
