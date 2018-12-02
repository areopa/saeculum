using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using project_c.Data;
using project_c.Models;
using ReflectionIT.Mvc.Paging;

namespace project_c.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GamesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Games
        
        public async Task<IActionResult> Index(string searchstring, string filterGenre1, string filterGenre2, string filterGenre3, string filterGenre4, string filterGenre5, string filterGenre6, string filterGenre7, string filterGenre8, string filterGenre9, string filterGenre10, string filterGenre11, string filterGenre12, string filterGenre13, string filterGenre14, string filterGenre15, string filterGenre16, string filterGenre17, string filterGenre18, string filterPegi, string filterPegi13, string filterPegi14, string filterPegi15, string filterPegi16, string filterPegi17, string filterPegi18, int page = 1, string sortExpression = "Title")
        {
            await AdminCheck();
            var games = _context.Games.AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchstring))
            {
                games = games.Where(m => m.Title.Contains(searchstring));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre1))
            {
                games = games.Where(s => s.Genre.Contains("Action"));
            }

            if (!string.IsNullOrWhiteSpace(filterGenre2))
            {
                games = games.Where(s => s.Genre.Contains("Adventure"));
            }

            if (!string.IsNullOrWhiteSpace(filterGenre3))
            {
                games = games.Where(s => s.Genre.Contains("Animation"));
            }

            if (!string.IsNullOrWhiteSpace(filterGenre4))
            {
                games = games.Where(s => s.Genre.Contains("Children"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre5))
            {
                games = games.Where(s => s.Genre.Contains("Comedy"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre6))
            {
                games = games.Where(s => s.Genre.Contains("Crime"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre7))
            {
                games = games.Where(s => s.Genre.Contains("Documentary"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre8))
            {
                games = games.Where(s => s.Genre.Contains("Drama"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre9))
            {
                games = games.Where(s => s.Genre.Contains("Fantasy"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre10))
            {
                games = games.Where(s => s.Genre.Contains("Film-Noir"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre11))
            {
                games = games.Where(s => s.Genre.Contains("Horror"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre12))
            {
                games = games.Where(s => s.Genre.Contains("Musical"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre13))
            {
                games = games.Where(s => s.Genre.Contains("Mystery"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre14))
            {
                games = games.Where(s => s.Genre.Contains("Romance"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre15))
            {
                games = games.Where(s => s.Genre.Contains("Sci-Fi"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre16))
            {
                games = games.Where(s => s.Genre.Contains("Thriller"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre17))
            {
                games = games.Where(s => s.Genre.Contains("War"));
            }
            if (!string.IsNullOrWhiteSpace(filterGenre18))
            {
                games = games.Where(s => s.Genre.Contains("Western"));
            }

            if (!string.IsNullOrWhiteSpace(filterPegi))
            {
                games = games.Where(s => s.Pegi.ToString().Contains(filterPegi));
            }
            //if (!string.IsNullOrWhiteSpace(filterPegi12))
            //{
            //    games = games.Where(s => s.Pegi.ToString().Contains("12"));
            //}
            //if (!string.IsNullOrWhiteSpace(filterPegi13))
            //{
            //    games = games.Where(s => s.Pegi.ToString().Contains("13"));
            //}
            //if (!string.IsNullOrWhiteSpace(filterPegi14))
            //{
            //    games = games.Where(s => s.Pegi.ToString().Contains("14"));
            //}
            //if (!string.IsNullOrWhiteSpace(filterPegi15))
            //{
            //    games = games.Where(s => s.Pegi.ToString().Contains("15"));
            //}
            //if (!string.IsNullOrWhiteSpace(filterPegi16))
            //{
            //    games = games.Where(s => s.Pegi.ToString().Contains("16"));
            //}
            //if (!string.IsNullOrWhiteSpace(filterPegi17))
            //{
            //    games = games.Where(s => s.Pegi.ToString().Contains("18"));
            //}
            //if (!string.IsNullOrWhiteSpace(filterPegi18))
            //{
            //    games = games.Where(s => s.Pegi.ToString().Contains("18"));
            //}



            var model = await PagingList.CreateAsync(games, 25, page, sortExpression, "Title");

            model.RouteValue = new RouteValueDictionary {
                { "searchstring", searchstring},
                {"filterGenre1" , filterGenre1},
                {"filterGenre2" , filterGenre2},
                {"filterGenre3" , filterGenre3},
                {"filterGenre4" , filterGenre4},
                {"filterGenre5" , filterGenre5},
                {"filterGenre6" , filterGenre6},
                {"filterGenre7" , filterGenre7},
                {"filterGenre8" , filterGenre8},
                {"filterGenre9" , filterGenre9},
                {"filterGenre10" , filterGenre10},
                {"filterGenre11" , filterGenre11},
                {"filterGenre12" , filterGenre12},
                {"filterGenre13" , filterGenre13},
                {"filterGenre14" , filterGenre14},
                {"filterGenre15" , filterGenre15},
                {"filterGenre16" , filterGenre16},
                {"filterGenre17" , filterGenre17},
                {"filterGenre18" , filterGenre18},
                {"filterPegi" , filterPegi },
                {"12" , 12},
                {"13" , 13},
                {"14" , 14},
                {"15" , 15},
                {"16" , 16},
                {"17" , 17},
                {"18" , 18},
            };



            return View(model);
        
    }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        public async Task<ActionResult> Create()
        {
            await AdminCheck();
            if(ViewBag.IsAdmin == "true")
            {
                return View();
            }
            else
            {
                return Redirect("https://localhost:44379/Games");
            }
            
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Title,Genre,Pegi,Description,Price,ProducingCompany")] Game game)
        {

            if (ModelState.IsValid)
            {
                _context.Entry(game).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Title,Genre,Pegi,Description,Price,ProducingCompany")] Game game)
        {

            if (ModelState.IsValid)
            {
                _context.Games.Add(game);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        [NonAction]
        public async Task AdminCheck()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userMail = await _userManager.GetEmailAsync(user);
                if (userMail == "admin@hotmail.com")
                {
                    ViewBag.IsAdmin = "true";
                }
                else
                {
                    ViewBag.IsAdmin = "false";
                }
            }
            else
            {
                ViewBag.IsAdmin = "false";
            }
        }
    }
}
