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
        public async Task<ActionResult> CreateAdmin([Bind("UserName, Email, FirstName, FamilyName, BirthDate")] ApplicationUser user)
        {

            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user);
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

    }
}