using System.Collections.Generic;

namespace Imi.Project.Mobile.Core.Entities
{
    public class BaseApiModel<T>
    {
        public bool Succeeded { get; set; }
        public Info Info { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
