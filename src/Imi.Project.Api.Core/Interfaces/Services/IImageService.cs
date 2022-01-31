using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IImageService
    {
        Task<string> AddOrUpdateImageAsync<T>(Guid entityId, IFormFile image);
        void RemoveImage(string databasePath);
    }
}