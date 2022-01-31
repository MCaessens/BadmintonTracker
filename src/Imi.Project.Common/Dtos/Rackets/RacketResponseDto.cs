using System;

namespace Imi.Project.Common.Dtos.Rackets
{
    public class RacketResponseDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public string RacketType { get; set; }
    }
}
