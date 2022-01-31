using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using Imi.Project.Common;
using Imi.Project.Common.Dtos.Rackets;
using Imi.Project.Mobile.Core.Entities;
using Imi.Project.Mobile.Core.Helpers;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Core.Services;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Imi.Project.Mobile.Infrastructure.Services
{
    public class RacketsService : IRacketsService
    {
        private HttpClient _httpClient;

        public RacketsService()
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri($"{SharedConstants.ApiLink}me/rackets/");
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
        }

        public async Task<BaseApiModel<RacketModel>> GetAllRacketsAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.GetStringAsync("");
            var deserializedObj = JsonConvert.DeserializeObject<BaseApiModel<RacketResponseDto>>(response);
            deserializedObj.Succeeded = deserializedObj.Results != null;
            return deserializedObj.MapToModel();
        }

        public async Task<RacketModel> AddRacketAsync(RacketModel racketModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            using (var content = new MultipartFormDataContent())
            {
                content.Headers.ContentType.MediaType = "multipart/form-data";
                if (racketModel.Image != null)
                {
                    var stream = await racketModel.Image.OpenReadAsync();
                    var imageStream = new StreamContent(stream);
                    imageStream.Headers.ContentType = MediaTypeHeaderValue.Parse(racketModel.Image.ContentType);
                    content.Add(imageStream, "Image", racketModel.Image.FileName);
                    await CachedImage.InvalidateCache(racketModel.ImageUrl, CacheType.All, true);
                }

                content.Add(new StringContent(racketModel.Model), nameof(racketModel.Model));
                content.Add(new StringContent(racketModel.Brand), nameof(racketModel.Brand));
                content.Add(new StringContent(racketModel.UserId.ToString()), nameof(racketModel.UserId));
                content.Add(new StringContent(racketModel.RacketType.ToString()), nameof(racketModel.RacketType));

                var response = await _httpClient.PostAsync("", content);
                var serializedEntity = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RacketModel>(serializedEntity);
            }
        }

        public async Task<RacketModel> UpdateRacketAsync(RacketModel racketModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            using (var content = new MultipartFormDataContent())
            {
                content.Headers.ContentType.MediaType = "multipart/form-data";
                if (racketModel.Image != null)
                {
                    var stream = await racketModel.Image.OpenReadAsync();
                    var imageStream = new StreamContent(stream);
                    imageStream.Headers.ContentType = MediaTypeHeaderValue.Parse(racketModel.Image.ContentType);
                    content.Add(imageStream, "Image", racketModel.Image.FileName);
                    await CachedImage.InvalidateCache(racketModel.ImageUrl, CacheType.All, true);
                }

                content.Add(new StringContent(racketModel.Id.ToString()), nameof(racketModel.Id));
                content.Add(new StringContent(racketModel.Model), nameof(racketModel.Model));
                content.Add(new StringContent(racketModel.Brand), nameof(racketModel.Brand));
                content.Add(new StringContent(racketModel.UserId.ToString()), nameof(racketModel.UserId));
                content.Add(new StringContent(racketModel.RacketType.ToString()), nameof(racketModel.RacketType));

                var response = await _httpClient.PutAsync("", content);
                var serializedEntity = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RacketModel>(serializedEntity);
            }
        }

        public async Task<bool> DeleteRacketAsync(Guid id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.DeleteAsync(id.ToString());
            return response.IsSuccessStatusCode;
        }
    }
}