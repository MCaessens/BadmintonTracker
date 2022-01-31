using System;
using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Common.Dtos.Games
{
    public class GameRequestDto : BaseDto
    {
        public Guid? UserId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? ShuttleCockId { get; set; }
        public Guid? RacketId { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string Opponent { get; set; }
        public int Score { get; set; }
        public int OpponentScore { get; set; }
    }
}
