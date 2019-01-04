using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
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
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            await AdminCheck();
            return View();
        }

        public async Task<IActionResult> CreateAdmin()
        {
            await AdminCheck();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAdmin([Bind("UserName, Email, FirstName, FamilyName, BirthDate, AccountType")] ApplicationUser user)
        {

            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user);
                user.AccountType = "Admin";
                user.UserName = user.Email;
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, "Wachtwoord1!");
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

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


        public async Task<IActionResult> AddDummyOrders()
        {
            await AdminCheck();
            var user1 = await _userManager.FindByEmailAsync("ongeregistreerd@ongeregistreerd.ongeregistreerd");
            var user2 = await _userManager.FindByEmailAsync("donald@duck.voorbeeld");
            var user3 = await _userManager.FindByEmailAsync("dagobert@duck.voorbeeld");
            var user4 = await _userManager.FindByEmailAsync("lara@croft.voorbeeld");
            var user5 = await _userManager.FindByEmailAsync("doutzen@krous.voorbeeld");

            List<ApplicationUser> UserList = new List<ApplicationUser> { user1, user2, user3, user4, user5 };

            Random random = new Random();

            var order = new Order { };

            foreach (var item in UserList)
            {
                int randomNumber = random.Next(2, 8);
                for (int i = 0; i < randomNumber; i++)
                {
                    order = await AddOrders(item);

                    int randomNumber1 = random.Next(1, 3);
                    for (int j = 0; j < randomNumber1; j++)
                    {
                        await AddGames(order);
                    }
                }
            }

            return View(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Order> AddOrders(ApplicationUser user)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 1000);
            var randPrice = random.Next(10, 60);
            var endDate = new DateTime(2018, 09, 01);
            var startDate = new DateTime(2019, 01, 04);
            var randMonth = random.Next(9, 13);
            var randDay = random.Next(1, 30);

            Order order = new Order
            {
                UserId = user.Id,
                OrderDateTime = new DateTime(2018, randMonth, randDay),
                OrderMail = user.Email,
                Price = randPrice
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task AddGames(Order order)
        {
            GameOrder go = new GameOrder();
            Game product = new Game();

            Random random = new Random();
            int? randomNumber = random.Next(1, 1000);

            product = await _context.Games
                .FindAsync(randomNumber);
                
            go.Order = order;
            go.Game = product;

            _context.GameOrder.Add(go);
            await _context.SaveChangesAsync();
        }



        //public async Task<IActionResult> AddDummyOrders()
        //{
        //    await AdminCheck();

        //    var user1 = await _userManager.FindByEmailAsync("ongeregistreerd@ongeregistreerd.ongeregistreerd");
        //    var user2 = await _userManager.FindByEmailAsync("donald@duck.voorbeeld");
        //    var user3 = await _userManager.FindByEmailAsync("dagobert@duck.voorbeeld");
        //    var user4 = await _userManager.FindByEmailAsync("lara@croft.voorbeeld");
        //    var user5 = await _userManager.FindByEmailAsync("doutzen@krous.voorbeeld");

        //    List<ApplicationUser> UserList = new List<ApplicationUser> { user1, user2, user3, user4, user5 };

        //    Random random = new Random();


        //    foreach (var item in UserList)
        //    {
        //        int randomNumber = random.Next(1, 3);
        //        for (int i = 0; i < randomNumber; i++)
        //        {
        //            AddOrders(item);
        //        }
        //    }
        //    return View(nameof(Index));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async void AddOrders(ApplicationUser user)
        //{

        //    Random random = new Random();
        //    int randomNumber = random.Next(1, 1000);
        //    var randPrice = random.Next(10, 60);
        //    var endDate = new DateTime(2018, 09, 01);
        //    var startDate = new DateTime(2019, 01, 04);
        //    var randMonth = random.Next(9, 12);
        //    var randDay = random.Next(1, 29);

        //    Order order = new Order
        //    {
        //        UserId = user.Id,
        //        OrderDateTime = new DateTime(2018, randMonth, randDay),
        //        OrderMail = user.Email,
        //        Price = randPrice
        //    };

        //    order wordt opgeslagen bij de ingelogde gebruiker
        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();

        //    int randomNumber2 = random.Next(1, 2);
        //    for (int i = 0; i < randomNumber2; i++)
        //    {
        //        AddGames(order);
        //    }

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async void AddGames(Order order)
        //{
        //    GameOrder go = new GameOrder();
        //    Game product = new Game();

        //    Random random = new Random();
        //    int randomNumber = random.Next(1, 1000);

        //    product = _context.Games.Find(_context.Games.Find(randomNumber));
        //    go.Order = order;
        //    go.Game = product;

        //    _context.GameOrder.Add(go);
        //    await _context.SaveChangesAsync();
        //}
    }
}