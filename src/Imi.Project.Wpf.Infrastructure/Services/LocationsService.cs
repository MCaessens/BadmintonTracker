using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Imi.Project.Common;
using Imi.Project.Common.Dtos.Locations;
using Imi.Project.Wpf.Core.Entities;
using Imi.Project.Wpf.Core.Helpers;
using Imi.Project.Wpf.Core.Interfaces;
using Imi.Project.Wpf.Core.Services;
using Newtonsoft.Json;

namespace Imi.Project.Wpf.Infrastructure.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly HttpClient _httpClient;

        public LocationsService()
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri($"{SharedConstants.ApiLink}me/locations/");
        }

        public async Task<BaseApiModel<LocationModel>> GetAllLocationsAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.GetStringAsync("");
            var deserializedObj = JsonConvert.DeserializeObject<BaseApiModel<LocationResponseDto>>(response);
            deserializedObj.Succeeded = deserializedObj.Results != null;
            return deserializedObj.MapToModel();
        }
    }
}