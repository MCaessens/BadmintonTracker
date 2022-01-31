using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Mapper;
using Imi.Project.Common.Dtos.Locations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Core.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IImageService _imageService;

        public LocationsService(ILocationRepository locationRepository, IImageService imageService)
        {
            _locationRepository = locationRepository;
            _imageService = imageService;
        }

        public async Task<IActionResult> AddAsync(LocationRequestDto locationRequestDto)
        {
            if (!float.TryParse(locationRequestDto.Longitude, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var longitude))
                return ServiceHelper.BadRequest("Longitude must a number.");
            if (!float.TryParse(locationRequestDto.Latitude, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var latitude))
                return ServiceHelper.BadRequest("Latitude must a number.");

            var newId = Guid.NewGuid();
            var location = new Location
            {
                Id = newId,
                UserId = locationRequestDto.UserId,
                Street = locationRequestDto.Street,
                City = locationRequestDto.City,
                Name = locationRequestDto.Name,
                PostalCode = locationRequestDto.PostalCode,
                Longitude = longitude,
                Latitude = latitude
            };
            if (locationRequestDto.Image != null)
            {
                if (!locationRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest(Constants.MustBeImageErrorMessage);
                location.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(location.Id, locationRequestDto.Image);
            }
            else location.ImageUrl = "";

            var addedLocation = await _locationRepository.AddAsync(location);
            if (addedLocation is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(addedLocation.MapToDto());
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleted = await _locationRepository.DeleteAsync(id);

            if (!deleted) ServiceHelper.NotFound($"Location with id {id} could not be deleted");
            return ServiceHelper.Ok();
        }

        public async Task<IActionResult> ListAllAsync(PageParameters pageParameters)
        {
            var locations = await _locationRepository.ListAllAsync(pageParameters);

            if (!locations.Any()) return ServiceHelper.NotFound();
            return ServiceHelper.Ok(locations.MapToDto(_locationRepository.GetItemCount()));
        }

        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var location = await _locationRepository.GetByIdAsync(id);

            if (location is null) return ServiceHelper.NotFound($"Location with Id: {id} was not found. Please try again.");
            return ServiceHelper.Ok(location.MapToDto());
        }

        public async Task<IActionResult> UpdateAsync(LocationRequestDto locationRequestDto)
        {
            if (!float.TryParse(locationRequestDto.Longitude, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var longitude))
                return ServiceHelper.BadRequest("Longitude must a number.");
            if (!float.TryParse(locationRequestDto.Latitude, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var latitude))
                return ServiceHelper.BadRequest("Latitude must a number.");

            var location = await _locationRepository.GetByIdAsync(locationRequestDto.Id);
            if (location == null) return ServiceHelper.NotFound();

            location.Street = locationRequestDto.Street;
            location.City = locationRequestDto.City;
            location.Name = locationRequestDto.Name;
            location.PostalCode = locationRequestDto.PostalCode;
            location.Longitude = longitude;
            location.Latitude = latitude;
            if (locationRequestDto.Image != null)
            {
                if (!locationRequestDto.Image.ContentType.Contains("image")) return ServiceHelper.BadRequest(Constants.MustBeImageErrorMessage);
                location.ImageUrl = await _imageService.AddOrUpdateImageAsync<Racket>(location.Id, locationRequestDto.Image);
            }

            var updatedLocation = await _locationRepository.UpdateAsync(location);
            if (updatedLocation is null) return ServiceHelper.BadRequest();
            return ServiceHelper.Ok(updatedLocation.MapToDto());
        }

        public async Task<IActionResult> GetLocationsByUserId(Guid id, PageParameters pageParameters)
        {
            var query = _locationRepository.GetAll().OrderBy(g => g.Id).Where(l => l.UserId == id);
            var locationCount = query.Count();
            var locations = await query.Skip((pageParameters.Page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();

            if (!locations.Any()) return ServiceHelper.NotFound();
            return ServiceHelper.Ok(locations.MapToDto(locationCount));
        }
    }
}