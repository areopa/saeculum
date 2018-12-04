using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using project_c.Models;


namespace project_c.Data
{
    //extension van de standaard Identity class van de user
    public class ApplicationUser : IdentityUser
    {
        //voornaam
        [PersonalData]
        public string FirstName { get; set; }
        //achternaam
        [PersonalData]
        public string FamilyName { get; set; }
        //geboortedatum
        [PersonalData]
        public DateTime BirthDate { get; set; }
        //collectie van orders die horen bij een user, wordt geïmplementeerd bij OrderHistory
        public virtual IEnumerable<Order> Orders { get; set; }
        public Favorieten Favorieten { get; set; }
    }
}
