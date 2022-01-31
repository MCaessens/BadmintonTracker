using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Wpf.Core.Entities
{
    public class GameDisplayModel
    {
        public string Opponent { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public int OpponentScore { get; set; }
        public string Status { get; set; }
        public string Winner { get; set; }
        public LocationModel Location { get; set; }
        public ShuttleCockModel ShuttleCock { get; set; }
        public RacketModel Racket { get; set; }
    }
}
