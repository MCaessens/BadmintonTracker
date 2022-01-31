using System;
using Imi.Project.Common.Enums;

namespace Imi.Project.Blazor.Core.Entities.Games
{
    public class ShuttleCockModel : BaseModel
    {
        public Guid UserId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public ShuttleType ShuttleType { get; set; }
        public override string ToString()
        {
            return $"{Brand} {Model}";
        }
    }
}
