using System.Threading.Tasks;
using Imi.Project.Blazor.Core.Entities.Memory;

namespace Imi.Project.Blazor.Core.Interfaces
{
    public interface IUserManager
    {
        Task InitUser(string name);
        Task<User> GetUserId();
    }
}