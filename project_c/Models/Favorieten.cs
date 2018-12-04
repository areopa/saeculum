using project_c.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    public class Favorieten
    {
        //foreign key van de User ook PK
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        //string met gameId's
        public string GameList { get; set; }
    }
}

