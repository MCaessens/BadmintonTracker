using System;
using System.Collections.Generic;
using Imi.Project.Blazor.Core.Entities.Memory;

namespace Imi.Project.Blazor.Core.Interfaces
{
    public interface IGameManager
    {
        MemoryGameInstance StartNewInstance(User user);
        MemoryGameInstance GetInstanceByUserId(Guid id);
        List<MemoryCard> GetPlayingCards();
    }
}
