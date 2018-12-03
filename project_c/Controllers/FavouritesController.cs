using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project_c.Data;
using project_c.Models;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;

namespace project_c.Controllers
{
    public class FavouritesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private string strFave = "FaveItem";

        public FavouritesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            //tijdelijk opslaan van de inhoud van de favorieten in een variabele
            var SessionFavourites = HttpContext.Session.GetObject<List<CartItem>>(strFave);
            //hiermee wordt de inhoud van de session doorgespeeld naar de view
            ViewBag.Favourites = SessionFavourites;
            return View();


        }

        //functie waarmee een product wordt opgeslagen in de session vanuit de Game tabel in de database
        public IActionResult AddToFavourites(int? id)
        {
            //ingebouwde check voor controle op doorgeven van niet bestaande waarde
            if (id == null)
            {
                return NotFound();
            }
            //als er nog geen ShoppingCart session bestaat moet deze eerst gemaakt worden
            //als deze gemaakt is kan er een game in worden opgeslagen
            if (HttpContext.Session.GetObject<List<CartItem>>(strFave) == null)
            {
                List<CartItem> lsFave = new List<CartItem>
                {
                    new CartItem(_context.Games.Find(id))
                };
                HttpContext.Session.SetObject(strFave, lsFave);
            }
            //als er al wel een session bestaan, kan de game er aan worden toegevoegd
            else
            {
                List<CartItem> lsFave = HttpContext.Session.GetObject<List<CartItem>>(strFave);
                lsFave.Add(new CartItem(_context.Games.Find(id)));
                HttpContext.Session.SetObject(strFave, lsFave);
            }

            return Redirect("https://localhost:44379/Favourites");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int check = IsExistingCheck(id);
            List<CartItem> lsFave = HttpContext.Session.GetObject<List<CartItem>>(strFave);
            lsFave.RemoveAt(check);
            HttpContext.Session.SetObject(strFave, lsFave);
            return Redirect("https://localhost:44379/Favourites");
        }

        private int IsExistingCheck(int? id)
        {
            List<CartItem> lsFave = HttpContext.Session.GetObject<List<CartItem>>(strFave);
            for (int i = 0; i < lsFave.Count; i++)
            {
                if (lsFave[i].Product.Id == id) return i;
            }
            return -1;
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