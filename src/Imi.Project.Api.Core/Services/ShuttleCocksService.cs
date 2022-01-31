using System;
using System.Linq;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Mapper;
using Imi.Project.Common.Dtos.ShuttleCocks;
using Imi.Project.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Core.Services
{
    public class ShuttleCocksService : IShuttleCocksService
    {
        private readonly IShuttleCockRepository _shuttleCockRepository;
        private readonly IImageService _imageService;

        public ShuttleCocksService(IShuttleCockRepository shuttleCockRepository, IImageService imageService)
        {
            _shuttleCockRepository = shuttleCockRepository;
            _imageService = imageService;
        }

        public async Task<IActionResult> AddAsync(ShuttleCockRequestDto shuttleCockRequestDto)
        {
            if (!Enum.TryParse<ShuttleType>(shuttleCockRequestDto.ShuttleType, out ShuttleType shuttleType))
                return ServiceHelper.BadRequest(Constants.WrongShuttleTypeGivenErrorMessage);

            var shuttleCock = new ShuttleCock
            {
                Id = Guid.NewGuid(),
                Brand = shuttleCockRequestDto.Brand,
                Model = shuttleCockRequestDto.Model,
                ShuttleType = shuttleType,
                UserId = shuttleCockRequestDto.UserId
            };
            if (shuttleCockRequestDto.Image != null)
            {
                if (!shuttleCockRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest(Constants.MustBeImageErrorMessage);
                shuttleCock.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(shuttleCock.Id, shuttleCockRequestDto.Image);
            }
            else shuttleCock.ImageUrl = "";

            var addedShuttle = await _shuttleCockRepository.AddAsync(shuttleCock);
            if (addedShuttle is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(addedShuttle.MapToDto());
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleted = await _shuttleCockRepository.DeleteAsync(id);

            if (!deleted) return ServiceHelper.NotFound();
            return ServiceHelper.Ok();
        }

        public async Task<IActionResult> ListAllAsync(PageParameters pageParameters)
        {
            var shuttleCocks = await _shuttleCockRepository.ListAllAsync(pageParameters);

            if (!shuttleCocks.Any()) return ServiceHelper.NotFound();
            return ServiceHelper.Ok(shuttleCocks.MapToDto(_shuttleCockRepository.GetItemCount()));
        }

        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var shuttleCock = await _shuttleCockRepository.GetByIdAsync(id);
            if (shuttleCock is null) return ServiceHelper.NotFound();
            return ServiceHelper.Ok(shuttleCock.MapToDto());
        }

        public async Task<IActionResult> UpdateAsync(ShuttleCockRequestDto shuttleCockRequestDto)
        {
            if (!Enum.TryParse<ShuttleType>(shuttleCockRequestDto.ShuttleType, out ShuttleType shuttleType))
                return ServiceHelper.BadRequest(Constants.WrongShuttleTypeGivenErrorMessage);

            var shuttleCock = await _shuttleCockRepository.GetByIdAsync(shuttleCockRequestDto.Id);
            if (shuttleCock == null) return ServiceHelper.BadRequest();

            shuttleCock.Brand = shuttleCockRequestDto.Brand;
            shuttleCock.Model = shuttleCockRequestDto.Model;
            shuttleCock.ShuttleType = shuttleType;
            if (shuttleCockRequestDto.Image != null)
            {
                if (!shuttleCockRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest(Constants.MustBeImageErrorMessage);
                shuttleCock.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(shuttleCock.Id, shuttleCockRequestDto.Image);
            }

            var updatedShuttle = await _shuttleCockRepository.UpdateAsync(shuttleCock);
            if (updatedShuttle is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(updatedShuttle.MapToDto());
        }

        public async Task<IActionResult> GetShuttleCocksByUserId(Guid id, PageParameters pageParameters)
        {
            var query = _shuttleCockRepository.GetAll().OrderBy(g => g.Id).Where(r => r.UserId == id);
            var shuttleCount = query.Count();
            var shuttleCocks = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            if (!shuttleCocks.Any()) return ServiceHelper.NotFound();
            return ServiceHelper.Ok(shuttleCocks.MapToDto(shuttleCount));
        }
    }
}