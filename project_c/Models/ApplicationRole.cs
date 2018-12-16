using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName)
        {

        }

        public ApplicationRole(string roleName, string description, DateTime creationDate) : base(roleName)
        {
            this.Description = description;
            this.CreationDate = creationDate;
        }

        //beschrijving van de UserRole
        public string Description { get; set; }
        //datum van creatie van de UserRole
        public DateTime CreationDate { get; set; }
    }
}
