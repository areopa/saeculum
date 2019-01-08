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
        public DateTime UserDate { get; set; }

        public int UserCount { get; set; }
    }


    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderDateGroup> Users { get; set; }
        public string[] UserDates { get; set; }
        public int[] UserCount { get; set; }
        public (string, int)[] TupleArray { get; set; }


        public async Task OnGetAsync()
        {

            var data3 =
                (from users in _context.Users
                 group users by new { month = users.AccountCreated.Month, year = users.AccountCreated.Year } into d
                 select new OrderDateGroup() { UserDate = new DateTime(d.Key.year, d.Key.month, 1), UserCount = d.Count() }).OrderByDescending(g => g.UserCount);

            Users = await data3.AsNoTracking().ToListAsync();

            List<string> UserDatesList = new List<string> { };
            List<int> UserCountList = new List<int> { };
            List<(string, int)> TupleList = new List<(string, int)>{ };


            foreach (var item in data3)
            {
                string dates = item.UserDate.Day.ToString() + "." + item.UserDate.Month.ToString() + "." + item.UserDate.Year.ToString();
                int counts = item.UserCount;
                TupleList.Add((dates, counts));
                UserDatesList.Add(dates);
                UserCountList.Add(counts);
            }

            TupleList.Sort((x, y) => string.Compare(x.Item1, y.Item1));

            UserDates = UserDatesList.ToArray();
            UserCount = UserCountList.ToArray();

            TupleArray = TupleList.ToArray();
        


            //IQueryable < OrderDateGroup > data =
            //    from users in _context.Users
            //    group users by users.AccountCreated.Date into dateGroup
            //    select new OrderDateGroup()
            //    {
            //        UserDate = dateGroup.Key,
            //        UserCount = dateGroup.Count()
            //    };

            //Users = await data.AsNoTracking().ToListAsync();

            //foreach (var item in Users)
            //{
            //    string dates = item.UserDate.ToString();
            //    int counts = item.UserCount;
            //    UserDatesList.Add(dates);
            //    UserCountList.Add(counts);
            //}
            //UserDates = UserDatesList.ToArray();
            //UserCount = UserCountList.ToArray();
        }
    }
}