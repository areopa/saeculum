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
using Microsoft.EntityFrameworkCore;

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
            if (id == null)
            {
                return NotFound();
            }

            //game die moet worden toegevoegd aan favorieten
            var gameToAdd = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gameToAdd == null)
            {
                return NotFound();
            }

            //huidige user
            var user = await _userManager.GetUserAsync(User);
            //userId van de huidige user
            var userId = user.Id;

            var userExists = _context.Favorieten.Any(e => e.UserId.Equals(userId));

            if (userExists)
            {
                var currentUserFavorieten = await _context.Favorieten.FindAsync(userId);
                var currentFavorietenLijst = DeserializeByteToGameList(currentUserFavorieten.GameList);

                currentFavorietenLijst.Add(gameToAdd);
                var listToBeAdded = SerializeGameListToByte(currentFavorietenLijst);

                currentUserFavorieten.GameList = listToBeAdded;
                _context.Update(currentUserFavorieten);
                await _context.SaveChangesAsync();
            }
            else
            {
                List<Game> newGameList = new List<Game> { gameToAdd };
                var newGameListSerialized = SerializeGameListToByte(newGameList);
                
                Favorieten favorietenToBeAdded = new Favorieten
                {
                    UserId = userId,
                    ApplicationUser = user,
                    GameList = newGameListSerialized
                };
                await _context.Favorieten.AddAsync(favorietenToBeAdded);
                await _context.SaveChangesAsync();
            }

            return Redirect("https://localhost:44379/Games");
        }

        public static Byte[] SerializeGameListToByte(List<Game> gameList)
        {
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, gameList);
                bytes = stream.ToArray();
            }

            return bytes;
        }

        public static List<Game> DeserializeByteToGameList(Byte[] serializedList)
        {
            List<Game> gameList = null;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(serializedList))
            {
                gameList = (formatter.Deserialize(stream) as List<Game>);
            }
            return gameList;
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