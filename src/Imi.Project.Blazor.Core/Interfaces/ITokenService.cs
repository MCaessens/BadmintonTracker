using System.Threading.Tasks;

namespace Imi.Project.Blazor.Core.Interfaces
{
    public interface ITokenService
    {
        Task SaveToken(string token);
        Task<string> GetToken();
    }
}