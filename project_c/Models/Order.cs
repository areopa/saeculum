using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_c.Data;

namespace project_c.Models
{
    //entity model voor order
    public class Order
    {
        //PK
        public int Id { get; set; }
        //UserId van de user die de bestelling plaatst
        public string UserId { get; set; }
        //de user die hoort bij een order
        public ApplicationUser ApplicationUser { get; set; }
        //datum en tijd van de bestelling
        public DateTime OrderDateTime { get; set; }
        //mailadres dat hoort bij de bestelling, hoeft niet hetzelfde mailadres als dat van de user te zijn
        public string OrderMail { get; set; }
        //totale prijs van de bestelling
        public decimal Price { get; set; }
        //collectie van games die horen bij een Order
        public virtual IEnumerable<GameOrder> Games { get; set; }
    }
}
