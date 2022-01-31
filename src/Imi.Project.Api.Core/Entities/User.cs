using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Api.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        public Guid AccountIntegrityId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public ICollection<Game> Games { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<Racket> Rackets { get; set; }
        public ICollection<ShuttleCock> ShuttleCocks { get; set; }
    }
}
