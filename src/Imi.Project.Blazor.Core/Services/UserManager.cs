using System;
using System.Threading.Tasks;
using Imi.Project.Blazor.Core.Entities.Memory;
using Imi.Project.Blazor.Core.Interfaces;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Imi.Project.Blazor.Core.Services
{
    public class UserManager : IUserManager
    {
        private readonly IJSRuntime _js;

        public UserManager(IJSRuntime js)
        {
            _js = js;
        }

        public async Task InitUser(string name)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = name
            };
            var serializedUser = JsonConvert.SerializeObject(user);
            await _js.InvokeAsync<object>("localStorage.setItem", "userInfo", serializedUser);
        }

        public async Task<User> GetUserId()
        {
            var serializedUser = await _js.InvokeAsync<string>("localStorage.getItem", "userInfo");
            if (string.IsNullOrEmpty(serializedUser)) return null;
            var user = JsonConvert.DeserializeObject<User>(serializedUser);
            return user;
        }
    }
}