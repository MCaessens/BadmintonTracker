using System;
using System.Collections.Generic;
using System.Linq;
using Imi.Project.Blazor.Core.Entities.Memory;
using Imi.Project.Blazor.Core.Interfaces;

namespace Imi.Project.Blazor.Core.Services
{
    public class GameManager : IGameManager
    {
        public GameManager(IMemoryCardRepository memoryCardRepository)
        {
            _memoryCardRepository = memoryCardRepository;
            CurrentGames = new List<MemoryGameInstance>();
        }
        private readonly IMemoryCardRepository _memoryCardRepository;
        private List<MemoryGameInstance> CurrentGames { get; }

        public List<MemoryCard> GetPlayingCards()
        {
            var rand = new Random();
            var memoryCards =  _memoryCardRepository.GetAllCards()
                                                   .ToList();
            return memoryCards.OrderBy(c => rand.Next()).ToList();
        }
        public MemoryGameInstance StartNewInstance(User user)
        {
            var memoryGameInstance = new MemoryGameInstance
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                PlayCards = GetPlayingCards(),
                Username = user.Username
            };
            CurrentGames.Add(memoryGameInstance);
            return memoryGameInstance;
        }

        public MemoryGameInstance GetInstanceByUserId(Guid id)
        {
            var memoryInstance = CurrentGames.FirstOrDefault(mi => mi.UserId == id);
            return memoryInstance;
        }
    }
}
