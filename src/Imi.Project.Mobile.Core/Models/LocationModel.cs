using System;
using Xamarin.Essentials;

namespace Imi.Project.Mobile.Core.Models
{
    public class LocationModel : BaseModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ImageUrl { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public FileResult Image { get; set; }

        public override string ToString()
        {
            return $"{Name} - {City}";
        }
    }
}
