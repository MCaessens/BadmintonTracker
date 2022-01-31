using Imi.Project.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Api.Core.Entities
{
    public class Racket : BaseEntity
    {
        public Guid? UserId { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string Brand { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        public RacketType RacketType { get; set; }
        public ICollection<Game> RelatedGames { get; set; }
    }
}
