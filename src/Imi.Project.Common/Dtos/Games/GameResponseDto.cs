using System;

namespace Imi.Project.Common.Dtos.Games
{
    public class GameResponseDto : BaseDto
    {
        public string Opponent { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? ShuttleCockId { get; set; }
        public Guid? RacketId { get; set; }
        public int Score { get; set; }
        public int OpponentScore { get; set; }
        public string Status { get; set; }
        public string Winner { get; set; }

    }
}
