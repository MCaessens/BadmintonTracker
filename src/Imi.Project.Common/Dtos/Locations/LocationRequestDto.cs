using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Imi.Project.Common.Dtos.Locations
{
    public class LocationRequestDto : BaseDto
    {
        public Guid? UserId { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30, MinimumLength = 2)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(150, MinimumLength = 3)]
        public string Street { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public IFormFile Image { get; set; }
    }
}