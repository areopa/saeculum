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
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private string strCart = "CartItem";
        //deze sessionvariabele houdt bij of er een emailadres handmatig moet worden ingevuld om de order te plaatsen en versturen
        public const string DifferentEmailBool = "false";
        //deze sessionvariabele houdt bij welk emailadres moet worden gebruikt voor het versturen van de keys
        public const string DestinationEmail = "";

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Hier kom je terug nadat je order is betaald en verwerkt
        public IActionResult Index()
        {
            //Cart wordt verwijderd uit de session
            HttpContext.Session.Remove(strCart);
            //de session variablele over de email wordt teruggezet naar default
            HttpContext.Session.SetString(DifferentEmailBool, "false");
            //De session variabele die de vervangede email bewaard wordt teruggezet naar een empty string
            HttpContext.Session.SetString(DestinationEmail, "");
            return View();
        }

        //Actionresult nadat je doorgaat vanuit je winkelwagen
        public async Task<IActionResult> UserDetailsPage()
        {
            //var die de user opvraagt
            var user = await _userManager.GetUserAsync(User);
            //als er daadwerkelijk een user is ingelogd voer je uit:
            if (user != null)
            {
                //deze bool is voor de Razor View bedoeld. Het bepaalt of de gebruiker de ingelogde versie ziet of de ongeregistreerde
                ViewBag.Bool = "true";
                //Als de user is ingelogd wordt zijn email hier opgevraagd
                var userMail = await _userManager.GetEmailAsync(user);
                //viewbag om het emailadres te tonen in de Razor view
                ViewBag.Email = userMail;
            }
            else
            {
                //De session wordt hier aangepast naar true. Er moet in dit geval WEL een alternatief emailadres worden ingevuld.
                HttpContext.Session.SetString(DifferentEmailBool, "true");
                //deze bool is voor de Razor View bedoeld. Het bepaalt of de gebruiker de ingelogde versie ziet of de ongeregistreerde
                ViewBag.Bool = "false";
            }

            return View();
        }

        //actionresult als je een ander Email wilt invullen of als je niet bent ingelogd
        public IActionResult DifferentEmail()
        {
            return View();
        }

        //De Actionresult die het emailadres opslaat als de gebruiker niet is ingelogd of ervoor kiest om ergens anders naartoe te sturen.
        [HttpPost]
        public ActionResult EmailChanged(DifferentEmail model)
        {
            if (ModelState.IsValid)
            {
                //viewbag wordt aangepast met de ingevoerde email
                ViewBag.Email = model.Email;
                //beide sessions worden aangepast zodat het ingevoerde adres straks verder wordt verwerkt
                HttpContext.Session.SetString(DifferentEmailBool, "true");
                HttpContext.Session.SetString(DestinationEmail, model.Email);
            }

            return View();
        }

        //Dit is de PayPal mockup view
        public IActionResult Payment()
        {
            return View();
        }

        //actionresult wanneer je als geregistreerde en ingelogde gebruiker doorgaat naar betaling
        public IActionResult UserPayment()
        {
            //hier draait het allemaal om. Deze session moet naar false worden gezet om een vervelende bug tegen te gaan waarbij de useremail niet wordt opgevraagd
            HttpContext.Session.SetString(DifferentEmailBool, "false");
            //return redirect naar de Payment view, dus de PayPal mockup
            return Redirect("https://localhost:44379/Order/Payment");
        }

        //Hier worden de orders verwerkt en toegevoegd aan de database
        public async Task<IActionResult> ProcessOrder()
        {
            //Cart lijst wordt opgevraagd uit de session
            List<CartItem> lsCart = HttpContext.Session.GetObject<List<CartItem>>(strCart);
            //User wordt opgevraagd uit de usermanager
            var user = await _userManager.GetUserAsync(User);

            //Als er geen gebruiker is, dus je bent niet ingelogd
            if (user == null)
            {
                //DEZE var moet worden veranderd naar de ID van de ongeregistreerde user. Deze user moet wel eerst worden aangemaakt handmatig!***
                var userId = "2a4aff3b-1d4f-4e11-8101-577b1eda19c8";
                //Email moet ook worden aangepast als je dat anders hebt staan in je DB
                var userMail = "ongeregistreerd@hotmail.com";
                //viewbag met email wordt vervangen door de ongeregistreerde email
                ViewBag.Email = userMail;
                //orderprijs, vanzelfsprekend
                var orderPrice = lsCart.Sum(x => x.Product.Price);

                //dit wordt de inhoud van de toe te voegen order
                Order order = new Order
                {
                    UserId = userId,
                    OrderDateTime = DateTime.Now,
                    OrderMail = userMail,
                    Price = orderPrice
                };
                //order wordt toegevoegd en opgeslagen in de DB bij ONGEREGISTREERDE GEBRUIKER
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                //deze string bepaalt naar welk email uiteindelijk de gamekeys worden verstuurd
                string finalEmail = HttpContext.Session.GetString(DestinationEmail);
                //deze string wordt op dit moment niet gebruikt. Ik heb hem erin gedaan om de gamekeys misschien op de website zelf te kunnen tonen ipv alleen per mail. Dan wordt deze string compleet gevuld met de keys.
                string allKeys = "";

                //loop die IEDERE game verwerkt
                foreach (var cart in lsCart)
                {
                    //mappen van de relatie tussen games en order
                    AddGames(cart, order);
                    //Hier defineer ik de parameters voor de mail***********************************************
                    //string gamekey wordt gevormd door de cart.Product en DateTime als string samen te voegen. Zo krijg je zelfs bij dezelfde games een UNIEKE key
                    string gameKey = cart.Product.ToString() + DateTime.Now.ToString();
                    //Hier wordt de string omgezet naar een code
                    gameKey = Crypto.Hash(gameKey);
                    //de ongebruikte string wordt hier uitgebreid met de nieuwe gamekey
                    allKeys = allKeys + gameKey + " <br/><br/> ";
                    //string om de gamename naar de emailfunctie te sturen als argument
                    string gameName = cart.Product.Title;
                    //einde parameters***********************************************
                    //Hier wordt de email verstuurd***********************************************************************Comment de volgende line als je geen emails wilt krijgen bij het bestellen iedere keer***
                    SendEmail(finalEmail, gameName, gameKey);
                }
                //redirect naar de order
                return Redirect("https://localhost:44379/Order");
            }
            //In geval dat de user WEL is ingelogd
            else
            {
                //userid wordt opgevraagd uit usermanager
                var userId = await _userManager.GetUserIdAsync(user);
                //email wordt opgevraagd uit usermanager
                var userMail = await _userManager.GetEmailAsync(user);
                //viewbag wordt aangepast
                ViewBag.Email = userMail;
                var orderPrice = lsCart.Sum(x => x.Product.Price);

                //dit wordt de inhoud van de toe te voegen order
                Order order = new Order
                {
                    UserId = userId,
                    OrderDateTime = DateTime.Now,
                    OrderMail = userMail,
                    Price = orderPrice
                };
                //order wordt opgeslagen bij de ingelogde gebruiker
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                //deze string bepaalt naar welk email uiteindelijk de gamekeys worden verstuurd
                string finalEmail = "";
                if (HttpContext.Session.GetString(DifferentEmailBool) == "true")
                {
                    finalEmail = HttpContext.Session.GetString(DestinationEmail);
                }
                else
                {
                    finalEmail = userMail;
                }
                string allKeys = "";
                foreach (var cart in lsCart)
                {
                    //mappen van de relatie tussen games en order
                    AddGames(cart, order);
                    //Hier defineer ik de parameters voor de mail***********************************************
                    //string gamekey wordt gevormd door de cart.Product en DateTime als string samen te voegen. Zo krijg je zelfs bij dezelfde games een UNIEKE key
                    string gameKey = cart.Product.ToString() + DateTime.Now.ToString();
                    //Hier wordt de string omgezet naar een code
                    gameKey = Crypto.Hash(gameKey);
                    //de ongebruikte string wordt hier uitgebreid met de nieuwe gamekey
                    allKeys = allKeys + gameKey + " <br/><br/> ";
                    //string om de gamename naar de emailfunctie te sturen als argument
                    string gameName = cart.Product.Title;
                    //einde parameters***********************************************
                    //Hier wordt de email verstuurd***********************************************************************Comment de volgende line als je geen emails wilt krijgen bij het bestellen iedere keer***
                    SendEmail(finalEmail, gameName, gameKey);
                }
                //redirect naar de order
                return Redirect("https://localhost:44379/Order");
            }

        }

        //functie voor het mappen van de relatie tussen games en order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void AddGames(CartItem cartItem, Order order)
        {
            GameOrder go = new GameOrder();
            Game product = new Game();

            product = _context.Games.Find(cartItem.Product.Id);
            go.Order = order;
            go.Game = product;

            _context.GameOrder.Add(go);
            await _context.SaveChangesAsync();
        }

        //nonaction method. Dit is geen actionresult etc. gewoon een functie die moet worden aageroepen
        [NonAction]
        //void Sensmail die 3 argumenten nodig heeft. 1. De email waar het bericht naartoe moet worden gestuurd     2.de naam van de game       3.de gamekey
        public void SendEmail(string emailTo, string gameName, string gameKey)
        {
            //var die bepaalt vanaf welk emailadres het bericht wordt gestuurd en wat er als naam komt in het bericht
            var fromEmail = new MailAddress("jirowebshop@gmail.com", "JIRO GAMEKEYS");
            //de geadresseerde, waar de keys naartoe gaan
            var toEmail = new MailAddress(emailTo);
            //wachtwoord van onze email
            var fromEmailPassword = "wachtwoord1!";
            //subject van de email
            string subject = "Uw GAMEKEY!";

            //de body van de email. Dit bevat alle tekst. Hier zitten dus ook de gamekeys bij
            string body = "Dit is de GameKey van de game: " + "<br/><br/>" + gameName + "<br/>" + gameKey + "<br/><br/><br/>" + "Activeer uw game op de betreffende Platform!" + "<br/><br/>" + "Veel speelplezier" + "<br/><br/><br/>" + "Jiro WebShop";

            //hier wordt de connectie met onze email gelegd
            var smtp = new SmtpClient
            {
                //standaard host en port etc voor gmail
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            //hier wordt het bericht gevormd door de afzender en ontvanger als argument door te geven
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                //subject van de message wordt ingevoerd zoals eerder aangegeven. Dit kan eventueel hergebruikt worden bij andere email functies**
                Subject = subject,
                //body wordt overgenomen net als hierboven subject
                Body = body,
                //Dit zorgt ervoor dat we de <br/> kunnen gebruiken om een regel lager te typen etc. Andere html is eventueel ook mogelijk.
                IsBodyHtml = true
            })
                //Deze functie verstuurt het bericht
                smtp.Send(message);
        }

    }
}