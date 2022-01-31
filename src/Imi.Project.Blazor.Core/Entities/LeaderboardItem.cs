using System;

namespace Imi.Project.Blazor.Core.Entities
{
    public class LeaderboardItem
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int Attempts { get; set; }
        public int Score { get; set; }
        public int GamesPlayed { get; set; }
    }
}