using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    //extension van de standaard Identity class van de user
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }
        //voornaam
        [PersonalData]
        public string FirstName { get; set; }
        //achternaam
        [PersonalData]
        public string FamilyName { get; set; }
        //geboortedatum
        [PersonalData]
        public DateTime BirthDate { get; set; }
        public DateTime AccountCreated { get; set; }
        public string AccountType { get; set; }
        //collectie van orders die horen bij een user, wordt geïmplementeerd bij OrderHistory
        public virtual IEnumerable<Order> Orders { get; set; }
        //bij een ApplicationUser hoort 1 Favorietenlijst
        public virtual Favorieten Favorieten { get; set; }
    }
}
