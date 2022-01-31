using System;

namespace Imi.Project.Blazor.Core.Entities.Memory
{
    public class GameEndEventArgs : EventArgs
    {
        public LeaderboardItem NewEntry { get; set; }

        public GameEndEventArgs(LeaderboardItem newEntry)
        {
            NewEntry = newEntry;
        }
    }
}