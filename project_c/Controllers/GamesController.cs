using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
            //await FavorietCheck();
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

        // GET: GamesAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: GamesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: GamesAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Pegi,Description,Price,ProducingCompany")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
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
        //functie waarmee gecheckt wordt of er een admin ingelogd is voor de weergave van specifieke knoppen
        [NonAction]
        public async Task AdminCheck()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("Superadmin"))
                {
                    ViewBag.IsAdmin = "true";
                    ViewBag.IsSuperAdmin = "true";
                }
                else
                {
                    if (userRoles.Contains("Admin"))
                    {
                        ViewBag.IsAdmin = "true";
                        ViewBag.IsSuperAdmin = "false";
                    }
                    else
                    {
                        ViewBag.IsAdmin = "false";
                        ViewBag.IsSuperAdmin = "false";
                    }
                }
            }
        }
        ////checkt of een game in de favorietenlijst staat of niet
        //[NonAction]
        //public async Task FavorietCheck()
        //{
        //    List<Game> gameList = null;
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user != null)
        //    {
        //        Favorieten favorietenClass = null;
        //        favorietenClass = await _context.Favorieten.FindAsync(user.Id);
        //        var favorietenLijst = DeserializeByteToGameList(favorietenClass.GameList);
        //        ViewBag.IsFavoriet = favorietenLijst;
        //        //var dinges = _context.Favorieten.Contains(user);
        //    }
        //    else
        //    {
        //        ViewBag.IsFavoriet = gameList;
        //    }
        //}

        ////fucntie waarmee een bytestring wordt omgezet in een GameList
        //public static List<Game> DeserializeByteToGameList(Byte[] serializedList)
        //{
        //    List<Game> gameList = null;
        //    IFormatter formatter = new BinaryFormatter();
        //    using (MemoryStream stream = new MemoryStream(serializedList))
        //    {
        //        gameList = (formatter.Deserialize(stream) as List<Game>);
        //    }
        //    return gameList;
        //}

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
