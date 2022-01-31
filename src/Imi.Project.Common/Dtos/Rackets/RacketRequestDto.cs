using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Common.Dtos.Rackets
{
    public class RacketRequestDto : BaseDto
    {
        public Guid? UserId { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string Brand { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string Model { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        public string RacketType { get; set; }

    }
}
