using System;

namespace Imi.Project.Blazor.Core.Entities.Games
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

        public override string ToString()
        {
            return $"{Name} - {City}";
        }
    }
}
