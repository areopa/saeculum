using Microsoft.AspNetCore.Identity;
using project_c.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Data
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context,
                                            UserManager<ApplicationUser> userManager,
                                            RoleManager<ApplicationRole> roleManager)
        {
            //hiermee wordt verzekerd dat de database aangemaakt is
            //de database wordt pas aangemaakt door het programma een keer uit te voeren
            context.Database.EnsureCreated();

            string adminId1 = "";

            string role0 = "Superadmin";
            string desc0 = "Dit is de rol van superadministrator";

            string role1 = "Admin";
            string desc1 = "Dit is de rol van administrator";

            string role2 = "Gebruiker";
            string desc2 = "Dit is de rol van gebruiker";

            string password = "Wachtwoord1!";

            if (await roleManager.FindByNameAsync(role0) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role0, desc0, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }

            //maken van de superadmin
            if (await roleManager.FindByNameAsync("jirowebshop@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "jirowebshop@gmail.com",
                    Email = "jirowebshop@gmail.com",
                    FirstName = "Jiro",
                    FamilyName = "Webshop",
                    BirthDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role0);
                }
                adminId1 = user.Id;
            }
            //maken van de ongeregistreerde user
            if (await userManager.FindByNameAsync("donald@duck.voorbeeld") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "ongeregistreerd@ongeregistreerd.ongeregistreerd",
                    Email = "ongeregistreerd@ongeregistreerd.ongeregistreerd",
                    FirstName = "Ongeregistreerd",
                    FamilyName = "Ongeregistreerd",
                    BirthDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }

            //maken van een voorbeelduser
            if (await userManager.FindByNameAsync("donald@duck.voorbeeld") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "donald@duck.dummy",
                    Email = "donald@duck.dummy",
                    FirstName = "Donald",
                    FamilyName = "Duck",
                    BirthDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }
            //maken van een voorbeelduser
            if (await userManager.FindByNameAsync("dagobert@duck.voorbeeld") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "dagobert@duck.voorbeeld",
                    Email = "dagobert@duck.voorbeeld",
                    FirstName = "Dagobert",
                    FamilyName = "Duck",
                    BirthDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }
            //maken van een voorbeelduser
            if (await userManager.FindByNameAsync("lara@croft.voorbeeld") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "lara@croft.voorbeeld",
                    Email = "lara@croft.voorbeeld",
                    FirstName = "Lara",
                    FamilyName = "Croft",
                    BirthDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }
            //maken van een voorbeelduser
            if (await userManager.FindByNameAsync("doutzen@krous.voorbeeld") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "doutzen@krous.voorbeeld",
                    Email = "doutzen@krous.voorbeeld",
                    FirstName = "Doutzen",
                    FamilyName = "Krous",
                    BirthDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }


        }
    }
}
