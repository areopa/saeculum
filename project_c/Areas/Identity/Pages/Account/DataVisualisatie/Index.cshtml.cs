using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project_c.Data;
using project_c.Models;
using project_c;


namespace project_c.Areas.Identity.Pages.Account.DataVisualisatie
{


    public class OrderDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }

        public int OrderCount { get; set; }
    }


public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderDateGroup> Orders { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<OrderDateGroup> data =
                from orders in _context.Orders
                group orders by orders.OrderDateTime.Date into dateGroup
                select new OrderDateGroup()
                {
                    OrderDate = dateGroup.Key,
                    OrderCount = dateGroup.Count()
                };

            Orders = await data.AsNoTracking().ToListAsync();
        }
    }
}