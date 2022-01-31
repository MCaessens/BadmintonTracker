using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imi.Project.Common;
using Imi.Project.Wpf.Core.Entities;
using Imi.Project.Wpf.Core.Interfaces;
using Imi.Project.Wpf.Core.Services;
using Newtonsoft.Json;

namespace Imi.Project.Wpf.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri($"{SharedConstants.ApiLink}auth/");
        }

        public async Task<BaseApiModel<LoginModel>> LoginAsync(LoginModel loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("login", loginRequest);
            var tokenSerialized = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginModel>(tokenSerialized);
            return new BaseApiModel<LoginModel> {Results = new List<LoginModel> {loginResponse}, Succeeded = response.IsSuccessStatusCode};
        }

        public async Task<bool> LogoutAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.PostAsJsonAsync("logout", "");
            return response.IsSuccessStatusCode;
        }
    }
}