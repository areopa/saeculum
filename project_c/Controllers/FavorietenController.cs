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
            
            //id van de game die moet worden toegevoegd aan favorieten
            var gameId = gameToAdd.Id;

            var userExists = _context.Favorieten.Any(UserId => UserId.Equals(userId));

            if (userExists)
            {
                var currentUserFavorieten = await _context.Favorieten.FindAsync(userId);
                var currentFavorietenLijst = DeserializeByteToIntList(currentUserFavorieten.GameList);

                currentFavorietenLijst.Add(gameId);
                var listToBeAdded = SerializeIntListToByte(currentFavorietenLijst);

                currentUserFavorieten.GameList = listToBeAdded;
                _context.Update(currentUserFavorieten);
                await _context.SaveChangesAsync();
            }
            else
            {
                List<int> newGameList = new List<int> { gameId };
                var newGameListSerialized = SerializeIntListToByte(newGameList);
                
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

        public virtual Byte[] SerializeIntListToByte(List<int> intList)
        {
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, intList);
                bytes = stream.ToArray();
            }

            return bytes;
        }

        public virtual List<int> DeserializeByteToIntList(Byte[] serializedList)
        {
            List<int> intList = null;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(serializedList))
            {
                
                intList = (formatter.Deserialize(stream) as List<int>);
            }
            return intList;
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