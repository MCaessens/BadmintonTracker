using System.Threading.Tasks;
using Imi.Project.Blazor.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;

namespace Imi.Project.Blazor.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IJSRuntime _js;

        public TokenService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SaveToken(string token)
        {
            await _js.InvokeAsync<object>("localStorage.setItem", "auth-token", token);
        }

        public async Task<string> GetToken()
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", "auth-token");
        }
    }
}