using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_c.Data;
using project_c.Models;

namespace project_c.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public const string thumbnailSourceNumber = "0";
        public string thumbNailSourceBase = "https://localhost:44379/images/thumbnails/GAME";
        public string thumbNailSourceEnd = ".jpg";

        public class MostWantedGame
        {
            public int gameId { get; set; }

            public string gameTitle { get; set; }

            public string gameGenre { get; set; }

            public int gamePegi { get; set; }

            public decimal gamePrice { get; set; }

            public int gameCount { get; set; }
        }

        public class GameListGroup
        {
            public int GameTitle { get; set; }

            public int GameCount { get; set; }
        }



        public IActionResult Index()
        {
            var data = _context.GameOrder.AsNoTracking()
                .AsQueryable();

            var mostWanted = from games in data
                             group games by games.GameId into gameGroup
                             select new GameListGroup()
                             {
                                 GameTitle = gameGroup.Key,
                                 GameCount = gameGroup.Count()
                             };
            

            var innerJoinQuery =
                         from gameCount in mostWanted
                         join game in _context.Games on gameCount.GameTitle equals game.Id
                         select new MostWantedGame()
                         {
                             gameId = game.Id,
                             gameTitle = game.Title,
                             gameGenre = game.Genre,
                             gamePegi = game.Pegi,
                             gamePrice = game.Price,
                             gameCount = gameCount.GameCount
                         };

            MostWanted newList = new MostWanted
            {
                MostWantedList = innerJoinQuery
            };

            PickNextThumbnail();
            return View(newList);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Informatie over ons bedrijf.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Onze contactgegevens.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        public void PickNextThumbnail()
        {
            string numberstring = HttpContext.Session.GetString(thumbnailSourceNumber);
            int nextNumber = Convert.ToInt32(numberstring);
            nextNumber++;
            ViewBag.thumbnailSource = thumbNailSourceBase + nextNumber + thumbNailSourceEnd;
            if (nextNumber >= 15)
            {
                nextNumber = 0;
            }
            HttpContext.Session.SetString(thumbnailSourceNumber, nextNumber.ToString());
        }
    }
}
