using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imi.Project.Common;
using Imi.Project.Mobile.Core.Entities;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Core.Services;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Imi.Project.Mobile.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri($"{SharedConstants.ApiLink}auth/");
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
        }

        public async Task<BaseApiModel<LoginModel>> LoginAsync(LoginModel loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("login", loginRequest);
            var tokenSerialized = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginModel>(tokenSerialized);
            return new BaseApiModel<LoginModel> {Results = new List<LoginModel> {loginResponse}, Succeeded = response.IsSuccessStatusCode};
        }

        public async Task<bool> RegisterAsync(RegisterModel registerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("register", registerRequest);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LogoutAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.PostAsJsonAsync("logout", "");
            return response.IsSuccessStatusCode;
        }
    }
}