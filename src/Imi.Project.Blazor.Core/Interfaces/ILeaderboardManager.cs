using System.Collections.Generic;
using Imi.Project.Blazor.Core.Entities;

namespace Imi.Project.Blazor.Core.Interfaces
{
    public interface ILeaderboardManager
    {
        void AddItem(LeaderboardItem item);
        void LoadLeaderboard();
        List<LeaderboardItem> LeaderBoardItems { get; set; }
    }
}