﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_c.Data;
using project_c.Models;

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

        //functie voor het toevoegen van Games aan favorieten
        public async Task<IActionResult> AddFavoriet(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            //var favorieten = _context.;

            Game Game = _context.Games.Find(id);
            string userId = user.Id;
            int gameToAdd = Game.Id;
            bool checkUser = FavorietenExists(userId);

            Favorieten Favorietenlijst = new Favorieten
            {
                UserId = userId,
                GameList = "1,2"
            };

            if (checkUser)
            {
                Favorieten oldOrder = _context.Favorieten.Find(userId);


                List<int> favoArray = OmzettenNaarArray(oldOrder.GameList);
                favoArray.Append(gameToAdd);
                string stringList = OmzettenNaarString(favoArray);
                
                oldOrder.GameList = stringList;

                _context.Update(oldOrder);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Favorieten.Add(Favorietenlijst);
                await _context.SaveChangesAsync();
            }


            return Redirect("https://localhost:44379/Games");
        }

        private bool FavorietenExists(string userId)
        {
            return _context.Favorieten.Any(e => e.UserId == userId);
        }

        private bool ItemExists(int game)
        {
            return false;
        }

        public static bool CheckUser()
        {
            bool check = false;
            return check;
        }

        public static string OmzettenNaarString(List<int> stringListToAdd)
        {

            string lijst = string.Join(",", stringListToAdd);
            return lijst;
        }

        public static List<int> OmzettenNaarArray(string arrayToAdd)
        {
            var readableArray = arrayToAdd.Split(',').Select(Int32.Parse).ToList();
            return readableArray;
        }

        // GET: Favorieten
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Favorieten.Include(f => f.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Favorieten/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }



        // POST: Favorieten/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,GameList")] Favorieten favorieten)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favorieten);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", favorieten.UserId);
            return View(favorieten);
        }



        //// GET: Favorieten/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var favorieten = await _context.Favorieten
        //        .Include(f => f.ApplicationUser)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (favorieten == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(favorieten);
        //}

        //// POST: Favorieten/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var favorieten = await _context.Favorieten.FindAsync(id);
        //    _context.Favorieten.Remove(favorieten);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


    }
}