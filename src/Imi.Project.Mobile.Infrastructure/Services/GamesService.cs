using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imi.Project.Common;
using Imi.Project.Common.Dtos.Games;
using Imi.Project.Mobile.Core.Entities;
using Imi.Project.Mobile.Core.Helpers;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Core.Services;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Imi.Project.Mobile.Infrastructure.Services
{
    public class GamesService : IGamesService
    {
        private HttpClient _httpClient;

        public GamesService()
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri($"{SharedConstants.ApiLink}me/games/");
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
        }

        public async Task<BaseApiModel<GameModel>> GetAllGamesAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.GetStringAsync("");
            var deserializedObj = JsonConvert.DeserializeObject<BaseApiModel<GameResponseDto>>(response);
            deserializedObj.Succeeded = deserializedObj.Results != null;
            return deserializedObj.MapToModel();
        }

        public async Task<GameModel> AddGameAsync(GameModel gameModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.PostAsJsonAsync("", gameModel.MapToRequest());
            var serializedEntity = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GameModel>(serializedEntity);
        }

        public async Task<GameModel> UpdateGameAsync(GameModel gameModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.PutAsJsonAsync("", gameModel.MapToRequest());
            var serializedEntity = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GameModel>(serializedEntity);
        }

        public async Task<bool> DeleteGameAsync(Guid id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }
    }
}