using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imi.Project.Blazor.Core.Entities.Games;
using Imi.Project.Blazor.Core.Helpers;
using Imi.Project.Blazor.Core.Interfaces;
using Imi.Project.Common;
using Imi.Project.Common.Dtos.Games;
using Newtonsoft.Json;

namespace Imi.Project.Blazor.Core.Services
{
    public class GamesService : IGamesService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public GamesService(ITokenService tokenService)
        {
            _tokenService = tokenService;
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri($"{SharedConstants.ApiLink}me/games/");
        }

        public async Task<IEnumerable<GameModel>> ListAllAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
            var response = await _httpClient.GetStringAsync("");
            var deserializedObj = JsonConvert.DeserializeObject<BaseApiModel<GameResponseDto>>(response);
            if (deserializedObj.Results == null || !deserializedObj.Results.Any()) return null;
            return deserializedObj.Results.MapToModel();
        }

        public async Task<GameModel> GetByIdAsync(Guid id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
            var response = await _httpClient.GetStringAsync(id.ToString());
            var deserializedObj = JsonConvert.DeserializeObject<GameResponseDto>(response);
            return deserializedObj?.MapToModel();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<GameModel> UpdateAsync(GameModel model)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
            var response = await _httpClient.PutAsJsonAsync("", model.MapToRequest());

            var serializedGame = await response.Content.ReadAsStringAsync();
            var deserializedObj = JsonConvert.DeserializeObject<GameResponseDto>(serializedGame);
            return deserializedObj.MapToModel();
        }

        public async Task<GameModel> AddGameAsync(GameModel gameModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
            var response = await _httpClient.PostAsJsonAsync("", gameModel.MapToRequest());

            var serializedGame = await response.Content.ReadAsStringAsync();
            var deserializedObj = JsonConvert.DeserializeObject<GameResponseDto>(serializedGame);
            return deserializedObj.MapToModel();
        }
    }
}