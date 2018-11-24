using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace project_c.Controllers
{
    //deze class zorgt ervoor dat je complexe objecten kan opslaan in een session
    public static class SessionExtensions
    {
        //serializen van een complex object
        //opslaan van een complex object in de vorm van een Json bestand zodat deze in een session opgeslagen kan worden
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        //deserializen van een complex object
        //lezen van een Json bestand dat gemaakt is met de functie SetObject()
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
