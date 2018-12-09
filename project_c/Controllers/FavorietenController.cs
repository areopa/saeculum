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
            //nullexception voor de id
            if (id == null)
            {
                return NotFound();
            }

            //game die moet worden toegevoegd aan favorieten
            var gameToAdd = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            //nullexception voor de game
            if (gameToAdd == null)
            {
                return NotFound();
            }

            //huidige user
            var user = await _userManager.GetUserAsync(User);
            //userId van de huidige user
            var userId = user.Id;

            //check (bool) om te zien of er een al een favorietenlijst bestaat van een user
            var userExists = _context.Favorieten.Any(e => e.UserId.Equals(userId));

            //game toevoegen aan een bestaande favorietenlijst
            if (userExists)
            {
                //favorietenclass van de user
                var userFavorieten = await _context.Favorieten.FindAsync(userId);
                //favorietenlijst van de user (deserialized)
                var userFavorietenlijst = DeserializeByteToGameList(userFavorieten.GameList);

                //toevoegen van de een game aan de deserialized GameList
                userFavorietenlijst.Add(gameToAdd);
                //serializen van de nieuwe favorietenlijst
                var newList = SerializeGameListToByte(userFavorietenlijst);
                //assignment van de nieuwe serialized list aan de favorieten class van de user
                userFavorieten.GameList = newList;
                //toevoegen van de updated favorietenclass aan de db
                _context.Update(userFavorieten);
                await _context.SaveChangesAsync();
            }
            //nieuwe favorietenlijst maken
            else
            {
                //maken van een nieuwe gamelist met de toe te voegen game er in
                List<Game> newGameList = new List<Game> { gameToAdd };
                //serializen van de nieuwe gamelist
                var newGameListSerialized = SerializeGameListToByte(newGameList);
                
                //maken van de nieuwe favorietenclass
                Favorieten favorietenToBeAdded = new Favorieten
                {
                    UserId = userId,
                    ApplicationUser = user,
                    GameList = newGameListSerialized
                };
                //toevoegen van de nieuwe favorietenclass aan de db
                await _context.Favorieten.AddAsync(favorietenToBeAdded);
                await _context.SaveChangesAsync();
            }
            return Redirect("https://localhost:44379/Games");
        }

        public async Task<IActionResult> RemoveFavoriet(int? id)
        {
            //nullexception voor de id
            if (id == null)
            {
                return NotFound();
            }
            //game die moet worden toegevoegd aan favorieten
            var gameToRemove = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            //nullexception voor de game
            if (gameToRemove == null)
            {
                return NotFound();
            }
            //huidige user
            var user = await _userManager.GetUserAsync(User);
            //userId van de huidige user
            var userId = user.Id;
            //check (bool) om te zien of er een al een favorietenlijst bestaat van een user
            var userExists = _context.Favorieten.Any(e => e.UserId.Equals(userId));

            if (userExists)
            {
                //favorietenclass van de user
                var userFavorieten = await _context.Favorieten.FindAsync(userId);
                //favorietenlijst van de user (deserialized)
                var userFavorietenlijst = DeserializeByteToGameList(userFavorieten.GameList);

                //toevoegen van de een game aan de deserialized GameList
                userFavorietenlijst.Remove(gameToRemove);
                //serializen van de nieuwe favorietenlijst
                var newList = SerializeGameListToByte(userFavorietenlijst);
                //assignment van de nieuwe serialized list aan de favorieten class van de user
                userFavorieten.GameList = newList;
                //toevoegen van de updated favorietenclass aan de db
                _context.Update(userFavorieten);
                await _context.SaveChangesAsync();
            }
            //er ging iets mis met het opzoeken van de user in de favorieten
            else
            {
                return NotFound();
            }

                return Redirect("https://localhost:44379/Games");
        }

        //functie waarmee de GameList wordt omgezet in een bytestring
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

        //fucntie waarmee een bytestring wordt omgezet in een GameList
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



        //// GET: Favorieten/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Favorieten/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof());
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}