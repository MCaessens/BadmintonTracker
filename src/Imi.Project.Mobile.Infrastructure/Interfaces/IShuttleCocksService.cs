using System;
using System.Threading.Tasks;
using Imi.Project.Mobile.Core.Entities;
using Imi.Project.Mobile.Core.Models;

namespace Imi.Project.Mobile.Infrastructure.Interfaces
{
    public interface IShuttleCocksService
    {
        Task<BaseApiModel<ShuttleCockModel>> GetAllShuttleCocksAsync();
        Task<ShuttleCockModel> AddShuttleCockAsync(ShuttleCockModel gameRequestModel);
        Task<ShuttleCockModel> UpdateShuttleCockAsync(ShuttleCockModel gameRequestModel);
        Task<bool> DeleteShuttleCockAsync(Guid id);
    }
}