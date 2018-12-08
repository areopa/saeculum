using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project_c.Data;
using project_c.Models;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace project_c.Controllers
{
    public class FavorietenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavorietenController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //add to favorieten functie
        public async Task<IActionResult> AddToFavorieten(int? id)
        {
            //huidige user
            var user = await _userManager.GetUserAsync(User);
            //userId van de huidige user
            var userId = user.Id;
            //game die moet worden toegevoegd aan favorieten
            Game gameToAdd = await _context.Games.FindAsync(id);

            List<Game> GameList = new List<Game>
            {
                gameToAdd
            };

            //MyObject obj = new MyObject();
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, GameList);
                bytes = stream.ToArray();
            }

            Favorieten NewFavorieten = new Favorieten
            {
                UserId = userId,
                ApplicationUser = user,
                GameList = bytes
            };

            _context.Favorieten.Add(NewFavorieten);
            await _context.SaveChangesAsync();




            return Redirect("https://localhost:44379/Games");
        }





        // GET: Favorieten
        public ActionResult Index()
        {
            return View();
        }

        // GET: Favorieten/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Favorieten/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Favorieten/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Favorieten/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Favorieten/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Favorieten/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Favorieten/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}