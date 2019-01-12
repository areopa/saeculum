using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    [Serializable]
    //entitymodel voor de game, het product
    public class Game
    {
        //PK
        public int Id { get; set; }
        //titel van de Game
        [Required]
        public string Title { get; set; }
        //Genre van de Game
        [Required]
        public string Genre { get; set; }
        //Pegi leeftijd van de Game
        [Required]
        public int Pegi { get; set; }
        //Beschrijvende tekst van de Game
        [Required]
        public string Description { get; set; }
        //Prijs van de Game
        [Required]
        public decimal Price { get; set; }
        //Bedrijf dat de Game geproduceerd heeft
        [Required]
        public string ProducingCompany { get; set; }
        //collectie van orders die horen bij een Game
        public virtual List<GameOrder> Orders { get; set; }
    }
}