using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Api.Core.Entities
{
    public class Location : BaseEntity
    {
        public Guid? UserId { get; set; }
        public User User { get; set; }
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
        [StringLength(100, MinimumLength = 3)]
        public string Street { get; set; }
        public string ImageUrl { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public ICollection<Game> RelatedGames { get; set; }
    }
}
