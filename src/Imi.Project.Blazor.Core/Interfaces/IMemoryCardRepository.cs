using Imi.Project.Blazor.Core.Entities.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imi.Project.Blazor.Core.Interfaces
{
    public interface IMemoryCardRepository
    {
        IEnumerable<MemoryCard> GetAllCards();
    }
}
