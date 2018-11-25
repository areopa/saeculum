using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    public class DifferentEmail
    {
        [Required]
        public string Email { get; set; }
    }
}
