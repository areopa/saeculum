using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    //de model voor een ander email. Ik had geen idee hoe ik user input uit de form krijg en dat in een variabele stop, dus maar zo gedaan
    public class DifferentEmail
    {
        //alleen de email van de form is nodig, dus die staat hier gemodelleerd
        [Required]
        public string Email { get; set; }
    }
}
