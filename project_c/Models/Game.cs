using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    //model voor de gameproduct
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Pegi { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProducingCompany { get; set; }
    }
}