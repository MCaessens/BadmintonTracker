using Imi.Project.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Wpf.Core.Entities
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
