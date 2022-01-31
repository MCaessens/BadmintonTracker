using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imi.Project.Common;
using Imi.Project.Common.Dtos.Games;
using Imi.Project.Wpf.Core.Entities;
using Imi.Project.Wpf.Core.Helpers;
using Imi.Project.Wpf.Core.Interfaces;
using Imi.Project.Wpf.Core.Services;
using Newtonsoft.Json;

namespace Imi.Project.Wpf.Infrastructure.Services
{
    public class GamesService : IGamesService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocationsService _locationsService;
        private readonly IShuttleCocksService _shuttleCocksService;
        private readonly IRacketsService _racketsService;

        public GamesService(ILocationsService locationsService,
            IRacketsService racketsService,
            IShuttleCocksService shuttleCocksService)
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri($"{SharedConstants.ApiLink}me/games/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            _locationsService = locationsService;
            _racketsService = racketsService;
            _shuttleCocksService = shuttleCocksService;
        }

        public async Task<BaseApiModel<GameModel>> GetAllGamesAsync()
        {
            var response = await _httpClient.GetStringAsync("");
            var deserializedObj = JsonConvert.DeserializeObject<BaseApiModel<GameResponseDto>>(response);
            if (deserializedObj.Results == null || !deserializedObj.Results.Any()) return null;
            return deserializedObj.MapToModel();
        }

        public async Task<GameModel> GetGameByIdAsync(Guid guid)
        {
            var response = await _httpClient.GetStringAsync(guid.ToString());
            var deserializedObj = JsonConvert.DeserializeObject<GameResponseDto>(response);
            if (deserializedObj is null) return null;
            return deserializedObj.MapToModel();
        }

        public async Task<GameModel> AddGameAsync(GameModel gameModel)
        {
            var response = await _httpClient.PostAsJsonAsync("", gameModel.MapToRequest());

            var serializedGame = await response.Content.ReadAsStringAsync();
            var deserializedObj = JsonConvert.DeserializeObject<GameResponseDto>(serializedGame);
            return deserializedObj.MapToModel();
        }

        public async Task<GameModel> UpdateGameAsync(GameModel gameModel)
        {
            var response = await _httpClient.PutAsJsonAsync("", gameModel.MapToRequest());

            var serializedGame = await response.Content.ReadAsStringAsync();
            var deserializedObj = JsonConvert.DeserializeObject<GameResponseDto>(serializedGame);
            return deserializedObj.MapToModel();
        }

        public async Task<bool> DeleteGameAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }
    }
}