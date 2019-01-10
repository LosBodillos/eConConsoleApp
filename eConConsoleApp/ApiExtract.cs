using Newtonsoft.Json.Linq;
using RestSharp;
using System;


namespace eConConsoleApp
{
    class ApiExtract
    {

        // metode der henter data fra API og gemmer i et JSON objekt
        public JObject GetCustomers(String url)
        {
            var client = new RestClient(url); // der oprettes en rest client til at varetage requestopgaver. Den skal kende url adressen til API så den ved hvem den skal snakke med
            var request = new RestRequest(Method.GET); // vi opretter en request og fortæller at det skal være en GET metode 
            IRestResponse response = client.Execute(request); // vi laver en variabel der kan opsamle svaret fra API. Vi får vores client til at sende vores request afsted
            JObject o = JObject.Parse(response.Content); // Vi ved at API returnerer JSON så vi opretter et JSON objekt til at gemme indholdet af responsen/svaret fra API.

            return o;
        }

    }
}
