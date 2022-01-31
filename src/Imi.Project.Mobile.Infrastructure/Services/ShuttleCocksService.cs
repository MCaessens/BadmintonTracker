using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using Imi.Project.Common;
using Imi.Project.Common.Dtos.ShuttleCocks;
using Imi.Project.Mobile.Core.Entities;
using Imi.Project.Mobile.Core.Helpers;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Core.Services;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Imi.Project.Mobile.Infrastructure.Services
{
    public class ShuttleCocksService : IShuttleCocksService
    {
        private HttpClient _httpClient;

        public ShuttleCocksService()
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri($"{SharedConstants.ApiLink}me/shuttlecocks/");
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
        }

        public async Task<BaseApiModel<ShuttleCockModel>> GetAllShuttleCocksAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.GetStringAsync("");
            var deserializedObj = JsonConvert.DeserializeObject<BaseApiModel<ShuttleCockResponseDto>>(response);
            deserializedObj.Succeeded = deserializedObj.Results != null;
            return deserializedObj.MapToModel();
        }

        public async Task<ShuttleCockModel> AddShuttleCockAsync(ShuttleCockModel shuttleCockModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            using (var content = new MultipartFormDataContent())
            {
                content.Headers.ContentType.MediaType = "multipart/form-data";
                if (shuttleCockModel.Image != null)
                {
                    var stream = await shuttleCockModel.Image.OpenReadAsync();
                    var imageStream = new StreamContent(stream);
                    imageStream.Headers.ContentType = MediaTypeHeaderValue.Parse(shuttleCockModel.Image.ContentType);
                    content.Add(imageStream, "Image", shuttleCockModel.Image.FileName);
                    await CachedImage.InvalidateCache(shuttleCockModel.ImageUrl, CacheType.All, true);
                }

                content.Add(new StringContent(shuttleCockModel.Model), nameof(shuttleCockModel.Model));
                content.Add(new StringContent(shuttleCockModel.Brand), nameof(shuttleCockModel.Brand));
                content.Add(new StringContent(shuttleCockModel.UserId.ToString()), nameof(shuttleCockModel.UserId));
                content.Add(new StringContent(shuttleCockModel.ShuttleType.ToString()), nameof(shuttleCockModel.ShuttleType));

                var response = await _httpClient.PostAsync("", content);
                var serializedEntity = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ShuttleCockModel>(serializedEntity);
            }
        }

        public async Task<bool> DeleteShuttleCockAsync(Guid id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<ShuttleCockModel> UpdateShuttleCockAsync(ShuttleCockModel shuttleCockModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.GetToken());
            using (var content = new MultipartFormDataContent())
            {
                content.Headers.ContentType.MediaType = "multipart/form-data";
                if (shuttleCockModel.Image != null)
                {
                    var stream = await shuttleCockModel.Image.OpenReadAsync();
                    var imageStream = new StreamContent(stream);
                    imageStream.Headers.ContentType = MediaTypeHeaderValue.Parse(shuttleCockModel.Image.ContentType);
                    content.Add(imageStream, "Image", shuttleCockModel.Image.FileName);
                    await CachedImage.InvalidateCache(shuttleCockModel.ImageUrl, CacheType.All, true);
                }

                content.Add(new StringContent(shuttleCockModel.Id.ToString()), nameof(shuttleCockModel.Id));
                content.Add(new StringContent(shuttleCockModel.Model), nameof(shuttleCockModel.Model));
                content.Add(new StringContent(shuttleCockModel.Brand), nameof(shuttleCockModel.Brand));
                content.Add(new StringContent(shuttleCockModel.UserId.ToString()), nameof(shuttleCockModel.UserId));
                content.Add(new StringContent(shuttleCockModel.ShuttleType.ToString()), nameof(shuttleCockModel.ShuttleType));

                var response = await _httpClient.PutAsync("", content);
                var serializedEntity = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ShuttleCockModel>(serializedEntity);
            }
        }
    }
}