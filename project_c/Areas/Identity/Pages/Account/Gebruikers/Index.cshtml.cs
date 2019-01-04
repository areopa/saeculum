using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project_c.Data;

namespace project_c.Areas.Identity.Pages.Account.Gebruikers
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
        public string[] OrderDates { get; set; }
        public int[] OrderCount { get; set; }


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

            List<string> OrderDatesList = new List<string> { };
            List<int> OrderCountList = new List<int> { };


            foreach (var item in Orders)
            {
                string dates = item.OrderDate.ToString();
                int counts = item.OrderCount;
                OrderDatesList.Add(dates);
                OrderCountList.Add(counts);
            }
            OrderDates = OrderDatesList.ToArray();
            OrderCount = OrderCountList.ToArray();
        }
    }
}