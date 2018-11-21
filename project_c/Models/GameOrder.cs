using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    //model waarmee een many to many relationship kan worden gemaakt tussen game en order
    //relaties tussen de game en order worden hier gemapped
    public class GameOrder
    {
        //PK
        public int Id { get; set; }
        //Id van de Order
        public int OrderId { get; set; }
        //Order
        public Order Order { get; set; }
        //Id van de Game
        public int GameId { get; set; }
        //Game
        public Game Game { get; set; }
    }
}
