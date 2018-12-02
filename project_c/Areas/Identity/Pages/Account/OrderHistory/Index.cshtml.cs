using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project_c.Data;
using project_c.Models;

namespace project_c.Areas.Identity.Pages.Account.OrderHistory
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Order> Order { get; set; }
        public IList<Game> Game { get; set; }
        public IList<GameOrder> GameOrder { get; set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Orders
                .Include(o => o.ApplicationUser).ToListAsync();

            GameOrder = await _context.GameOrder.ToListAsync();
            Game = await _context.Games.ToListAsync();


        }
    }
}