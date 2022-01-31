using System;
using System.Collections.Generic;
using System.Text;

namespace Imi.Project.Wpf.Core.Entities
{
    public class BaseApiModel<T>
    {
        public bool Succeeded { get; set; }
        public InfoModel Info { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
