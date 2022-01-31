using System.Collections.Generic;

namespace Imi.Project.Common.Dtos
{
    public class MultipleItemDto<T>
    {
        public InfoDto Info { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
