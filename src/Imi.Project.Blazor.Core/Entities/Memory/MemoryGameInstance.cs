using System;
using System.Collections.Generic;
using System.Linq;

namespace Imi.Project.Blazor.Core.Entities.Memory
{
    public class MemoryGameInstance
    {
        public MemoryGameInstance()
        {
            SelectedMemoryCards = new List<MemoryCard>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public List<MemoryCard> SelectedMemoryCards { get; }
        public List<MemoryCard> PlayCards { get; set; }
        public int Attempts { get; set; }
        public int Score { get; set; }
        public int SetsPlayed { get; set; }
        public bool GameEnded { get; set; }
        public EventHandler OnSetEnded { get; set; }
        public EventHandler OnTurnEnded { get; set; }
        public EventHandler<GameEndEventArgs> OnGameEnded { get; set; } 
 
        public void SelectCard(MemoryCard card)
        {
            if (SelectedMemoryCards.Contains(card)) return;
            
            card.IsTapped = true;
            SelectedMemoryCards.Add(card);
        }
        private bool AreCardsEqual()
        {
            return SelectedMemoryCards.First().CardNumber == SelectedMemoryCards.Last().CardNumber;
        }
        public void PlayTurn()
        {
            var cardsAreEqual = AreCardsEqual();
            if (cardsAreEqual)
            {
                IncreaseScore();
                RemoveSelectedCards();
            }

            Attempts++;
            if (PlayCards.Count == 0)
            {
                SetsPlayed++;
                InvokeOnSetEnded();
            }
            ClearSelection();
            InvokeOnTurnEnded();
            if (SetsPlayed == 3) InvokeOnGameEnded();
            }
        private void ClearSelection()
        {
            SelectedMemoryCards.ForEach(c => c.IsTapped = false);
            SelectedMemoryCards.Clear();
        }
        private void RemoveSelectedCards()
        {
            PlayCards.RemoveAll(c => SelectedMemoryCards.Contains(c));
            SelectedMemoryCards.Clear();
        }

        private void IncreaseScore()
        {
            Score += 100;
        }
        public void ResetGame()
        {
            GameEnded = false;
            Attempts = 0;
            Score = 0;
            SetsPlayed = 0;
            InvokeOnTurnEnded();
        }

        private void InvokeOnTurnEnded()
        {
            OnTurnEnded?.Invoke(this, EventArgs.Empty);
        }   
        private void InvokeOnGameEnded()
        {
            GameEnded = true;
            var leaderboardItem = new LeaderboardItem
            {
                Id = Guid.NewGuid(),
                Attempts = Attempts,
                GamesPlayed = SetsPlayed,
                Score = Score,
                UserName = Username
            };
            var args = new GameEndEventArgs(leaderboardItem);
            OnGameEnded?.Invoke(this, args);
        }

        private void InvokeOnSetEnded()
        {
            OnSetEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}