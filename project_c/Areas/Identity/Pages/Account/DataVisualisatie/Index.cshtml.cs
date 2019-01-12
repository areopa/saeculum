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
        public DateTime OrderDate { get; set; }

        public int OrderCount { get; set; }
    }


    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderDateGroup> Orders0 { get; set; }
        //public IList<OrderDateGroup> Orders1 { get; set; }
        public IList<DateTime> DateList = new List<DateTime>();

        public string[] OrderDates { get; set; }
        public int[] OrderCount { get; set; }


        public async Task OnGetAsync()
        {
            IQueryable<OrderDateGroup> data =
                from orders in _context.Orders
                group orders by new
                    {
                        orders.OrderDateTime.Year,
                        orders.OrderDateTime.Month,
                        orders.OrderDateTime.Day
                    } into dateGroup
                select new OrderDateGroup()
                {
                    OrderDate = new DateTime(dateGroup.Key.Year, dateGroup.Key.Month, dateGroup.Key.Day),
                    OrderCount = dateGroup.Count()
                };


            DateTime FirstDate = data.Min(x => x.OrderDate);
            DateTime LastDate = data.Max(x => x.OrderDate);

            await CreateDates(FirstDate, LastDate);

            Orders0 = await data.ToListAsync();

            await CheckDates();

            

            List<string> OrderDatesList = new List<string> { };
            List<int> OrderCountList = new List<int> { };

            foreach (var item in DateList)
            {
                var match = Orders0.FirstOrDefault(x => x.OrderDate == item);
                if (match != null)
                {
                    OrderDatesList.Add(item.ToShortDateString());
                    OrderCountList.Add(match.OrderCount);

                }
                else
                {
                    
                    OrderDatesList.Add(item.ToShortDateString());
                    OrderCountList.Add(0);
                }
            }

            //foreach (var item in Orders0)
            //{
            //    OrderDatesList.Add(item.OrderDate.ToShortDateString());
            //    OrderCountList.Add(item.OrderCount);
            //}

            OrderDates = OrderDatesList.ToArray();
            OrderCount = OrderCountList.ToArray();

            
            
        }

        public async Task CreateDates(DateTime FirstDate, DateTime LastDate)
        {    
            for (var dt = FirstDate; dt <= LastDate; dt = dt.AddDays(1))
            {
                DateList.Add(dt);
                Console.WriteLine(dt);
            }
        }

        public async Task CheckDates()
        {
            foreach (var item in DateList)
            {
                var match = Orders0.FirstOrDefault(x => x.OrderDate == item);
                if (match == null)
                {

                    Orders0.Append(new OrderDateGroup()
                    {
                        OrderDate = item,
                        OrderCount = 0
                    });
                }
            }
        }

    }
}