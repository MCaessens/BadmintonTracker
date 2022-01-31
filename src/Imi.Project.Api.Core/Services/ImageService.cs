using System;
using System.IO;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Imi.Project.Api.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostEnvironment _webHostEnvironment;

        public ImageService(IHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> AddOrUpdateImageAsync<T>(Guid entityId, IFormFile image)
        {
            var pathForDatabase = Path.Combine("images",
                typeof(T).Name.ToLower() + "s");

            var folderPathForImages = Path.Combine(
                _webHostEnvironment.ContentRootPath,
                "wwwroot",
                pathForDatabase);

            if (!Directory.Exists(folderPathForImages))
            {
                Directory.CreateDirectory(folderPathForImages);
            }

            var fileExtension = Path.GetExtension(image.FileName);

            var newFileNameWithExtension = $"{entityId}{fileExtension}";

            var filePath = Path.Combine(folderPathForImages, newFileNameWithExtension);

            if (image.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            var filePathForDatabase = Path.Combine(pathForDatabase, newFileNameWithExtension);

            return filePathForDatabase;
        }

        public void RemoveImage(string databasePath)
        {
            var fullPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", databasePath);
            if (!File.Exists(fullPath)) return;
            File.Delete(fullPath);
        }
    }
}