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
        public DateTime? UserDate { get; set; }

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


        public async Task OnGetAsync()
        {
            IQueryable<OrderDateGroup> data =
                from users in _context.Users
                group users by users.AccountCreated.Date into dateGroup
                select new OrderDateGroup()
                {
                    UserDate = dateGroup.Key,
                    UserCount = dateGroup.Count()
                };

            Users = await data.AsNoTracking().ToListAsync();

            List<string> UserDatesList = new List<string> { };
            List<int> UserCountList = new List<int> { };


            foreach (var item in Users)
            {
                string dates = item.UserDate.ToString();
                int counts = item.UserCount;
                UserDatesList.Add(dates);
                UserCountList.Add(counts);
            }
            UserDates = UserDatesList.ToArray();
            UserCount = UserCountList.ToArray();
        }
    }
}