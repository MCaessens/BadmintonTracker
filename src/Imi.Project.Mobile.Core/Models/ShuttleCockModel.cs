using System;
using Imi.Project.Common.Enums;
using Xamarin.Essentials;

namespace Imi.Project.Mobile.Core.Models
{
    public class ShuttleCockModel : BaseModel
    {
        public Guid UserId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public ShuttleType ShuttleType { get; set; }
        public FileResult Image { get; set; }
        public override string ToString()
        {
            return $"{Brand} {Model}";
        }
    }
}
