using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project_c.Data;
using project_c.Models;

namespace project_c.Areas.Identity.Pages.Account.Favorieten
{
    public class FavorietenModel : PageModel
    {
        private readonly project_c.Data.ApplicationDbContext _context;

        public FavorietenModel(project_c.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Favorieten> Favorieten { get;set; }

        public async Task OnGetAsync()
        {
            Favorieten = await _context.Favorieten
                .Include(f => f.ApplicationUser).ToListAsync();

        }
    }
}
