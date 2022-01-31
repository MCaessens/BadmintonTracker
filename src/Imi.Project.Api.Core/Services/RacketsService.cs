using System;
using System.Linq;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Mapper;
using Imi.Project.Common.Dtos.Rackets;
using Imi.Project.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Core.Services
{
    public class RacketsService : IRacketsService
    {
        private readonly IRacketRepository _racketRepository;
        private readonly IImageService _imageService;

        public RacketsService(IRacketRepository racketRepository, IImageService imageService)
        {
            _racketRepository = racketRepository;
            _imageService = imageService;
        }

        public async Task<IActionResult> AddAsync(RacketRequestDto racketRequestDto)
        {
            if (!Enum.TryParse(racketRequestDto.RacketType, out RacketType racketType))
                return ServiceHelper.BadRequest(Constants.WrongRacketTypeGivenErrorMessage);

            var racket = new Racket
            {
                Id = Guid.NewGuid(),
                Brand = racketRequestDto.Brand,
                Model = racketRequestDto.Model,
                RacketType = racketType,
                UserId = racketRequestDto.UserId
            };
            if (racketRequestDto.Image != null)
            {
                if (!racketRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest(Constants.MustBeImageErrorMessage);
                racket.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(racket.Id, racketRequestDto.Image);
            }
            else racket.ImageUrl = "";

            var addedRacket = await _racketRepository.AddAsync(racket);
            if (addedRacket is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(addedRacket.MapToDto());
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleted = await _racketRepository.DeleteAsync(id);

            if (!deleted) return ServiceHelper.NotFound();
            return ServiceHelper.Ok();
        }

        public async Task<IActionResult> ListAllAsync(PageParameters pageParameters)
        {
            var rackets = await _racketRepository.ListAllAsync(pageParameters);

            if (!rackets.Any()) return ServiceHelper.NotFound();
            return ServiceHelper.Ok(rackets.MapToDto(_racketRepository.GetItemCount()));
        }

        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var racket = await _racketRepository.GetByIdAsync(id);
            if (racket == null) return ServiceHelper.NotFound($"Game with Id: {id} was not found. Please try again.");
            return ServiceHelper.Ok(racket.MapToDto());
        }

        public async Task<IActionResult> UpdateAsync(RacketRequestDto racketRequestDto)
        {
            if (!Enum.TryParse(racketRequestDto.RacketType, out RacketType racketType))
                return ServiceHelper.BadRequest(Constants.WrongRacketTypeGivenErrorMessage);

            var racket = await _racketRepository.GetByIdAsync(racketRequestDto.Id);
            if (racket == null) return ServiceHelper.NotFound();

            racket.Brand = racketRequestDto.Brand;
            racket.Model = racketRequestDto.Model;
            racket.RacketType = racketType;

            if (racketRequestDto.Image != null)
            {
                if (!racketRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest(Constants.MustBeImageErrorMessage);
                racket.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(racket.Id, racketRequestDto.Image);
            }

            var updatedRacket = await _racketRepository.UpdateAsync(racket);
            if (updatedRacket is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(updatedRacket.MapToDto());
        }

        public async Task<IActionResult> GetRacketsByUserId(Guid id, PageParameters pageParameters)
        {
            var query = _racketRepository.GetAll().OrderBy(g => g.Id).Where(r => r.UserId == id);
            var racketCount = query.Count();
            var rackets = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            if (!rackets.Any()) return ServiceHelper.NotFound($"No games with racketId: {id} were found.");
            return ServiceHelper.Ok(rackets.MapToDto(racketCount));
        }
    }
}