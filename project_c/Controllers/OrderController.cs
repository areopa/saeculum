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

namespace project_c.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Remove("Cart");
            return View();
        }

        public async Task<IActionResult> UserDetailsPage()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var userMail = await _userManager.GetEmailAsync(user);
                ViewBag.Email = userMail;
            }

            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }


        public async Task<IActionResult> ProcessOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            List<CartItem> lsCart = HttpContext.Session.GetObject<List<CartItem>>("Cart");

            var userId = await _userManager.GetUserIdAsync(user);
            var userMail = await _userManager.GetEmailAsync(user);
            ViewBag.Email = userMail;
            var orderPrice = lsCart.Sum(x => x.Product.Price);
            var productId = 0;

            GameOrder go = new GameOrder();
            Game product = new Game();

            Order order = new Order
            {
                UserId = userId,
                OrderDateTime = DateTime.Now,
                OrderMail = userMail,
                Price = orderPrice
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            string allKeys = "";
            foreach (var cart in lsCart)
            {
                product = _context.Games.Find(cart.Product.Id);
                //Hier defineer ik de parameters voor de mail***********************************************
                string gameKey = product.ToString() + DateTime.Now.ToString();
                gameKey = Crypto.Hash(gameKey);
                allKeys = allKeys + gameKey + " <br/><br/> ";
                string gameName = product.Title;
                //einde parameters***********************************************
                go.Order = order;
                go.Game = product;
                _context.GameOrder.Add(go);
                await _context.SaveChangesAsync();
                //Hier wordt de email verstuurd***********************************************
                SendEmail(userMail, gameName, gameKey);
            }





            return Redirect("https://localhost:44342/Order");
        }


        [NonAction]
        public void SendEmail(string emailTo, string gameName, string gameKey)
        {
            var fromEmail = new MailAddress("jirowebshop@gmail.com", "JIRO GAMEKEYS");
            var toEmail = new MailAddress(emailTo);
            var fromEmailPassword = "wachtwoord1!";
            string subject = "Uw GAMEKEY!";

            string body = "Dit is de GameKey van de game: " + "<br/><br/>" + gameName + "<br/>" + gameKey + "<br/><br/><br/>" + "Activeer uw game op de betreffende Platform!" + "<br/><br/>" + "Veel speelplezier" + "<br/><br/><br/>" + "Jiro WebShop";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

    }
}