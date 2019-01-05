using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static project_c.Controllers.HomeController;

namespace project_c.Models
{
    public class MostWanted
    {
        public IQueryable<MostWantedGame> MostWantedList { get; set; }
    }
}
