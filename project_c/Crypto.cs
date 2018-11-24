using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Web;

namespace project_c
{
    //Definitie van de Class voor het versleutelen van gegevens
    public class Crypto
    {
        //Hash is de functie die je uitvoert (bijv:     string variabele = Crypto.Hash(WaardeOmTeVersleutelen);
        //Neemt maar 1 argument, dat is de waarde die wordt versleuteld (value)
        public static string Hash(string value)
        {
            //deze functie versleutelt de input
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}
