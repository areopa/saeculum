using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Areas.Admin.Models
{
    public class GameAmountList
    {
        public int gameId { get; set; }

        public string gameTitle { get; set; }

        public string gameGenre { get; set; }

        public int gamePegi { get; set; }

        public decimal gamePrice { get; set; }

        public int gameCount { get; set; }
    }
}
