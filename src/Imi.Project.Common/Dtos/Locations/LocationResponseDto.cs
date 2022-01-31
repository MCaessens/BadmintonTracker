using System;

namespace Imi.Project.Common.Dtos.Locations
{
    public class LocationResponseDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ImageUrl { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}
