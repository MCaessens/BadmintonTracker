using System;

namespace Imi.Project.Common.Dtos.ShuttleCocks
{
    public class ShuttleCockResponseDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public string ShuttleType { get; set; }
    }
}
