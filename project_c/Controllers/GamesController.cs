using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index(string searchstring, string filterGenre1, string filterGenre2, string filterGenre3, string filterGenre4, string filterGenre5, string filterGenre6, string filterGenre7, string filterGenre8, string filterGenre9, string filterGenre10, string filterGenre11, string filterGenre12, string filterGenre13, string filterGenre14, string filterGenre15, string filterGenre16, string filterGenre17, string filterGenre18, string filterPegi12, string filterPegi13, string filterPegi14, string filterPegi15, string filterPegi16, string filterPegi17, string filterPegi18, int page = 1, string sortExpression = "Title")
        {
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

            if (!string.IsNullOrWhiteSpace(filterPegi12))
            {
                games = games.Where(s => s.Pegi.ToString().Contains("12"));
            }
            if (!string.IsNullOrWhiteSpace(filterPegi13))
            {
                games = games.Where(s => s.Pegi.ToString().Contains("13"));
            }
            if (!string.IsNullOrWhiteSpace(filterPegi14))
            {
                games = games.Where(s => s.Pegi.ToString().Contains("14"));
            }
            if (!string.IsNullOrWhiteSpace(filterPegi15))
            {
                games = games.Where(s => s.Pegi.ToString().Contains("15"));
            }
            if (!string.IsNullOrWhiteSpace(filterPegi16))
            {
                games = games.Where(s => s.Pegi.ToString().Contains("16"));
            }
            if (!string.IsNullOrWhiteSpace(filterPegi17))
            {
                games = games.Where(s => s.Pegi.ToString().Contains("18"));
            }
            if (!string.IsNullOrWhiteSpace(filterPegi18))
            {
                games = games.Where(s => s.Pegi.ToString().Contains("18"));
            }



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
                {"filterPegi12" , filterPegi12},
                {"filterPegi13" , filterPegi13},
                {"filterPegi14" , filterPegi14},
                {"filterPegi15" , filterPegi15},
                {"filterPegi16" , filterPegi16},
                {"filterPegi17" , filterPegi17},
                {"filterPegi18" , filterPegi18},
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
    }
}
