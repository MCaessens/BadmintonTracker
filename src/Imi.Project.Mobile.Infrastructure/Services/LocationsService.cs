using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using Imi.Project.Common;
using Imi.Project.Common.Dtos.Locations;
using Imi.Project.Mobile.Core.Entities;
using Imi.Project.Mobile.Core.Helpers;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Core.Services;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Imi.Project.Mobile.Infrastructure.Services
{
    public class LocationsService : ILocationsService
    {
        private HttpClient _httpClient;

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

        public async Task<LocationModel> AddLocationAsync(LocationModel locationModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            using (var content = new MultipartFormDataContent())
            {
                content.Headers.ContentType.MediaType = "multipart/form-data";
                if (locationModel.Image != null)
                {
                    var stream = await locationModel.Image.OpenReadAsync();
                    var imageStream = new StreamContent(stream);
                    imageStream.Headers.ContentType = MediaTypeHeaderValue.Parse(locationModel.Image.ContentType);
                    content.Add(imageStream, "Image", locationModel.Image.FileName);
                    await CachedImage.InvalidateCache(locationModel.ImageUrl, CacheType.All, true);
                }

                content.Add(new StringContent(locationModel.Name), nameof(locationModel.Name));
                content.Add(new StringContent(locationModel.UserId.ToString()), nameof(locationModel.UserId));
                content.Add(new StringContent(locationModel.PostalCode), nameof(locationModel.PostalCode));
                content.Add(new StringContent(locationModel.City), nameof(locationModel.City));
                content.Add(new StringContent(locationModel.Street), nameof(locationModel.Street));
                content.Add(new StringContent(locationModel.Latitude.ToString()), nameof(locationModel.Latitude));
                content.Add(new StringContent(locationModel.Longitude.ToString()), nameof(locationModel.Longitude));

                var response = await _httpClient.PostAsync("", content);
                var serializedEntity = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LocationModel>(serializedEntity);
            }
        }

        public async Task<LocationModel> UpdateLocationAsync(LocationModel locationModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            using (var content = new MultipartFormDataContent())
            {
                content.Headers.ContentType.MediaType = "multipart/form-data";
                if (locationModel.Image != null)
                {
                    var stream = await locationModel.Image.OpenReadAsync();
                    var imageStream = new StreamContent(stream);
                    imageStream.Headers.ContentType = MediaTypeHeaderValue.Parse(locationModel.Image.ContentType);
                    content.Add(imageStream, "Image", locationModel.Image.FileName);
                    await CachedImage.InvalidateCache(locationModel.ImageUrl, CacheType.All, true);
                }

                content.Add(new StringContent(locationModel.Id.ToString()), nameof(locationModel.Id));
                content.Add(new StringContent(locationModel.Name), nameof(locationModel.Name));
                content.Add(new StringContent(locationModel.UserId.ToString()), nameof(locationModel.UserId));
                content.Add(new StringContent(locationModel.PostalCode), nameof(locationModel.PostalCode));
                content.Add(new StringContent(locationModel.City), nameof(locationModel.City));
                content.Add(new StringContent(locationModel.Street), nameof(locationModel.Street));
                content.Add(new StringContent(locationModel.Latitude.ToString()), nameof(locationModel.Latitude));
                content.Add(new StringContent(locationModel.Longitude.ToString()), nameof(locationModel.Longitude));

                var response = await _httpClient.PutAsync("", content);
                var serializedEntity = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LocationModel>(serializedEntity);
            }
        }

        public async Task<bool> DeleteLocationAsync(Guid id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }
    }
}