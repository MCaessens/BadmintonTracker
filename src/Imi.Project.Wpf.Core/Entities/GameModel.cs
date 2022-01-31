using System;
using System.Collections.Generic;
using System.Text;

namespace Imi.Project.Wpf.Core.Entities
{
    public class GameModel : BaseModel
    {
        public string Opponent { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public int OpponentScore { get; set; }
        public string Status { get; set; }
        public string Winner { get; set; }
        public Guid UserId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? ShuttleCockId { get; set; }
        public Guid? RacketId { get; set; }

        public override string ToString()
        {
            return $"{UserName} vs {Opponent}\n{Score} vs {OpponentScore}";
        }
    }
}
