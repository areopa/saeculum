﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_c.Data;
using project_c.Models;

namespace project_c.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        //naam van de session state met de inhoud van de ShoppingCart
        private string strCart = "CartItem";

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        //pagina met de weergave van de inhoud van de ShoppingCart
        public IActionResult Index()
        {
            //tijdelijk opslaan van de inhoud van de cart in een variabele
            var SessionContents = HttpContext.Session.GetObject<List<Game>>(strCart);
            //hiermee wordt de inhoud van de session doorgespeeld naar de view
            ViewBag.Contents = SessionContents;

            return View();
        }

        //functie waarmee een product wordt opgeslagen in de session vanuit de Game tabel in de database
        public IActionResult OrderNow(int? id)
        {
            //ingebouwde check voor controle op doorgeven van niet bestaande waarde
            if (id == null)
            {
                return NotFound();
            }
            //als er nog geen ShoppingCart session bestaat moet deze eerst gemaakt worden
            //als deze gemaakt is kan er een game in worden opgeslagen
            if (HttpContext.Session.GetObject<List<Game>>(strCart) == null)
            {
                List<Game> lsCart = new List<Game>
                {
                    _context.Games.Find(id)
                };
                HttpContext.Session.SetObject(strCart, lsCart);
            }
            //als er al wel een session bestaan, kan de game er aan worden toegevoegd
            else
            {
                List<Game> lsCart = HttpContext.Session.GetObject<List<Game>>(strCart);
                lsCart.Add(_context.Games.Find(id));
                HttpContext.Session.SetObject(strCart, lsCart);
            }

            return Redirect("https://localhost:44379/ShoppingCart");
        }

        //voordat een item kan worden verwijderd eerst controleren of dit product wel bestaat in de Cart
        //als het product is gevonden wordt de index van de lijst doorgegeven
        //zo weet de delete function op welke index het product wordt verwijderd
        private int IsExistingCheck(int? id, int index)
        {
            List<Game> lsCart = HttpContext.Session.GetObject<List<Game>>(strCart);
            for (int i = 0; i < lsCart.Count; i++)
            {
                Game game = lsCart[id];
                if (lsCart[i].Id == id && lsCart.BinarySearch(game) == index) return i;
            }
            return -1;
        }


        public IActionResult Delete(int? id, int index)
        {
            if (id == null)
            {
                return NotFound();
            }
            int check = IsExistingCheck(id);
            List<Game> lsCart = HttpContext.Session.GetObject<List<Game>>(strCart);
            lsCart.RemoveAt(check);
            HttpContext.Session.SetObject(strCart, lsCart);
            return Redirect("https://localhost:44379/ShoppingCart");
        }
    }
}