using System;
using System.Threading.Tasks;
using Imi.Project.Mobile.Core.Entities;
using Imi.Project.Mobile.Core.Models;

namespace Imi.Project.Mobile.Infrastructure.Interfaces
{
    public interface IRacketsService
    {
        Task<BaseApiModel<RacketModel>> GetAllRacketsAsync();
        Task<RacketModel> AddRacketAsync(RacketModel racketModel);
        Task<RacketModel> UpdateRacketAsync(RacketModel racketModel);
        Task<bool> DeleteRacketAsync(Guid id);
    }
}