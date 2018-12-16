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

    public class DefinitiveList
    {
        public int gameId { get; set; }

        public string gameTitle { get; set; }

        public string gameGenre { get; set; }

        public int gamePegi { get; set; }

        public decimal gamePrice { get; set; }

        public int gameCount { get; set; }
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
        public IList<DefinitiveList> DefinitiveList { get; set; }

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

            var innerJoinQuery = 
                from gameCount in data
                join game in _context.Games on gameCount.GameTitle equals game.Id
                select new DefinitiveList() { gameId = game.Id,
                             gameTitle = game.Title,
                             gameGenre = game.Genre,
                             gamePegi = game.Pegi,
                             gamePrice = game.Price,
                             gameCount = gameCount.GameCount };

            DefinitiveList = await innerJoinQuery.AsNoTracking().ToListAsync();
            Game = await data.AsNoTracking().ToListAsync();
            GamesList = await _context.Games.ToListAsync();
        }
    }
}
