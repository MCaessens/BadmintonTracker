using System.Threading.Tasks;
using Imi.Project.Blazor.Core.Entities.Games;

namespace Imi.Project.Blazor.Core.Interfaces
{
    public interface ILocationsService
    {
        Task<BaseApiModel<LocationModel>> GetAllLocationsAsync();
    }
}
