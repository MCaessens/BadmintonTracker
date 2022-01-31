using System;
using System.Threading.Tasks;
using Imi.Project.Mobile.Core.Entities;
using Imi.Project.Mobile.Core.Models;

namespace Imi.Project.Mobile.Infrastructure.Interfaces
{
    public interface ILocationsService
    {
        Task<BaseApiModel<LocationModel>> GetAllLocationsAsync();
        Task<LocationModel> AddLocationAsync(LocationModel locationModel);
        Task<LocationModel> UpdateLocationAsync(LocationModel locationModel);
        Task<bool> DeleteLocationAsync(Guid id);
    }
}