using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project_c.Data;
using project_c.Models;

namespace project_c.Areas.Identity.Pages.Account.Favorieten
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public IList<Game> GamesIdList { get; set; }

        public async Task OnGetAsync()
        {
            List<int> GamesId;
            var user = await _userManager.GetUserAsync(User);
            var Favorieten = await _context.Favorieten
                            .FindAsync(user.Id);

            var GameList = Favorieten.GameList;


            GamesId = DeserializeByteToIntList(Favorieten.GameList);

            foreach (var thing in GamesId)
            {
                var value = thing;
                GamesIdList.Add(await _context.Games.FindAsync(value));
            }
            
        }

        public virtual List<int> DeserializeByteToIntList(Byte[] serializedList)
        {
            List<int> intList = null;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(serializedList))
            {

                intList = (formatter.Deserialize(stream) as List<int>);
            }
            return intList;
        }
    }
}
