using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project_c.Data;
using project_c.Models;

namespace project_c.Areas.Identity.Pages.Account.DVGames
{
    public class GameListGroup
    {
        public int GameTitle { get; set; }

        public int GameCount { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly project_c.Data.ApplicationDbContext _context;

        public IndexModel(project_c.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GameListGroup> Game { get;set; }
        public IList<Game> GamesList { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<GameListGroup> data =
                from games in _context.GameOrder
                group games by games.GameId into gameGroup


                select new GameListGroup()
                {
                    GameTitle = gameGroup.Key,
                    GameCount = gameGroup.Count()
                };

            Game = await data.AsNoTracking().ToListAsync();
            GamesList = await _context.Games.ToListAsync();
        }
    }
}
