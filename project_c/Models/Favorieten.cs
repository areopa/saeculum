using project_c.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_c.Models
{
    public class Favorieten
    {
        //PK
        public int Id { get; set; }
        //foreign key van de User
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        //string met gameId's
        public string GameList { get; set; }
    }
}

////functie voor het toevoegen van Games aan favorieten
//public async Task<IActionResult> AddFavoriet(int? id)
//{
//    var user = await _userManager.GetUserAsync(User);
//    var favorieten = _context.;

//    Game Game = _context.Games.Find(id);
//    string userId = await _userManager.GetUserIdAsync(user);
//    string stringList = OmzettenNaarString();

//    Favorietenlijst Favorietenlijst = new Favorietenlijst
//    {
//        UserId = userId,
//        GameList = stringList
//    };

//    if (userId == searchUser.UserId)
//    {
//        _context.Update(Favorietenlijst);
//        await _context.SaveChangesAsync();
//    }
//    else
//    {
//        _context.Favorietenlijsten.Add(Favorietenlijst);
//        await _context.SaveChangesAsync();
//    }

//    return Redirect("https://localhost:44379/Games");
//}

//public static string OmzettenNaarString()
//{
//    string lijst = "5,6,7";
//    return lijst;
//}

//public static List<int> OmzettenNaarArray()
//{
//    return new List<int> { 1, 2, 4 };
//}