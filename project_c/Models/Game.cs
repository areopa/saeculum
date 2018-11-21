using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    //entitymodel voor de game, het product
    public class Game
    {
        //PK
        public int Id { get; set; }
        //titel van de Game
        public string Title { get; set; }
        //Genre van de Game
        public string Genre { get; set; }
        //Pegi leeftijd van de Game
        public int Pegi { get; set; }
        //Beschrijvende tekst van de Game
        public string Description { get; set; }
        //Prijs van de Game
        public decimal Price { get; set; }
        //Bedrijf dat de Game geproduceerd heeft
        public string ProducingCompany { get; set; }
        //collectie van orders die horen bij een Game
        public virtual IEnumerable<GameOrder> Orders { get; set; }
    }
}