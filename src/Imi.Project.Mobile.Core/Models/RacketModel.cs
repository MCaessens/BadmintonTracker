using System;
using Imi.Project.Common.Enums;
using Xamarin.Essentials;

namespace Imi.Project.Mobile.Core.Models
{
    public class RacketModel : BaseModel
    {
        public Guid UserId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public RacketType RacketType { get; set; }
        public FileResult Image { get; set; }
        public override string ToString()
        {
            return $"{Brand} {Model}";
        }
    }
}
