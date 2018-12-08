using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_c.Data;

namespace project_c.Models
{
    public class Favorieten
    {
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Byte[] Favorietenlijst { get; set; }
    }
}
