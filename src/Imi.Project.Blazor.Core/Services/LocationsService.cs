using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imi.Project.Blazor.Core.Entities.Games;
using Imi.Project.Blazor.Core.Helpers;
using Imi.Project.Blazor.Core.Interfaces;
using Imi.Project.Common;
using Imi.Project.Common.Dtos.Locations;
using Newtonsoft.Json;

namespace Imi.Project.Blazor.Core.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public LocationsService(ITokenService tokenService)
        {
            _tokenService = tokenService;
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri($"{SharedConstants.ApiLink}me/locations/");
        }

        public async Task<BaseApiModel<LocationModel>> GetAllLocationsAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
            var response = await _httpClient.GetStringAsync("");
            var deserializedObj = JsonConvert.DeserializeObject<BaseApiModel<LocationResponseDto>>(response);
            deserializedObj.Succeeded = deserializedObj.Results != null;
            return deserializedObj.MapToModel();
        }
    }
}