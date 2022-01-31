using System.Collections.Generic;
using System.IO;
using System.Linq;
using Imi.Project.Blazor.Core.Entities;
using Imi.Project.Blazor.Core.Interfaces;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Imi.Project.Blazor.Core.Services
{
    public class LeaderboardManager : ILeaderboardManager
    {
        public LeaderboardManager(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            LoadLeaderboard();
        }

        private readonly IHostEnvironment _hostEnvironment;
        public List<LeaderboardItem> LeaderBoardItems { get; set; } = new List<LeaderboardItem>();
        
        public void AddItem(LeaderboardItem item)
        {
            if (!LeaderBoardItems.Any())
            {
                LeaderBoardItems.Add(item);
                SaveLeaderBoard();
                return;
            }
            
            var foundItem = LeaderBoardItems.FirstOrDefault(i => i.Id == item.Id);
            var maxAttempts = LeaderBoardItems.Min(leaderBoardItem => leaderBoardItem.Attempts);
            if (foundItem is null)
            {
                if(item.Attempts < maxAttempts) LeaderBoardItems.Add(item);
            }
            else if (item.Attempts < foundItem.Attempts)
            {
                var index = LeaderBoardItems.IndexOf(foundItem);
                LeaderBoardItems.RemoveAt(index);
                LeaderBoardItems.Add(item);
            }

            LeaderBoardItems = LeaderBoardItems.OrderBy(leaderboardItem => leaderboardItem.Attempts).Take(10).ToList();
            SaveLeaderBoard();
        }

        private void SaveLeaderBoard()
        {
            var serializedLeaderboard = JsonConvert.SerializeObject(LeaderBoardItems);
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "leaderboard.json");
            File.WriteAllText(path, serializedLeaderboard);
        }

        public void LoadLeaderboard()
        {
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "leaderboard.json");
            if (!File.Exists(path)) return;
            var serializedLeaderboard = File.ReadAllText(path);
            var retrievedLeaderboard = JsonConvert.DeserializeObject<List<LeaderboardItem>>(serializedLeaderboard);
            LeaderBoardItems = retrievedLeaderboard.Take(10).OrderBy(i => i.Attempts).ToList();
        }
    }
}