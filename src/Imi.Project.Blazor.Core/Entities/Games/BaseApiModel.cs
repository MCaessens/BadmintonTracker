using System.Collections.Generic;

namespace Imi.Project.Blazor.Core.Entities.Games
{
    public class BaseApiModel<T>
    {
        public bool Succeeded { get; set; }
        public InfoModel Info { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
