using System;
using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Api.Core.Entities
{
    public class Game : BaseEntity
    {
        public User User { get; set; }
        public Guid? UserId { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string Opponent { get; set; }
        public Guid? LocationId { get; set; }
        public Location Location { get; set; }
        public Guid? ShuttleCockId { get; set; }
        public ShuttleCock ShuttleCock { get; set; }
        public Guid? RacketId { get; set; }
        public Racket Racket { get; set; }
        public int Score { get; set; }
        public int OpponentScore { get; set; }
    }
}
