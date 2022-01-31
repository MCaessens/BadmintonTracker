using System;
using System.Collections.Generic;
using System.Text;

namespace Imi.Project.Blazor.Core.Entities.Memory
{
    public class MemoryCard
    {
        public Guid Id { get; set; }
        public int CardNumber { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsTapped { get; set; } = false;
    }
}
