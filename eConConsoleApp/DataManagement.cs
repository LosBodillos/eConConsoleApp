using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace eConConsoleApp
{
    class DataManagement
    {
        // metode der laver sql inserts - returnerer en liste af SQL prepared statements med inserts
        public List<SqlCommand> GenerateCustomerInserts(JObject o) 
        {


            if (o.ContainsKey("collection")) // JSONobjekter består af en til flere keys (x) med tilknyttede værdier (y). loopet leder efter en key med navnet "collection"
            {
                List<SqlCommand> commands = new List<SqlCommand>(); // tom liste klar til sql inserts

                JToken jToken = o.GetValue("collection"); //Værdien der er knyttet til key "collection" er en JTOKEN (som er en liste med nye keys og values)

                foreach (JToken jt in jToken) // når vi åbner vores JToken op er der "små" tokens som ikke er lister men bare keys med værdier tilknyttet. Vi laver et loop hvor vi vil have alle key navne og values ud (loopet tager ÉN AD GANGEN!!)
                {
                    var customerId = jt.Value<int>("customerNumber"); // key er customer id
                    var name = jt.Value<String>("name"); // value er customer name


                    SqlCommand cmd = new SqlCommand(); // ny tom  kommando/query
                    cmd.CommandText = "INSERT INTO Customer (customerId, name) VALUES (@customerId, @name)"; // vores sql text laves til et prepared statement, dvs vi skriver ikke vores values direkte ind i sql kommandoen
                    cmd.Parameters.AddWithValue("customerId", customerId); // værdi sendes med som value på den viste plads- sikrer at uanset hvad værdien er så påvirker det ikke database
                    cmd.Parameters.AddWithValue("name", name); // værdi sendes med som value på den viste plads - sikrer at uanset hvad værdien er så påvirker det ikke database

                    commands.Add(cmd); // tilføj sql query til vores liste

                }

                return commands; // loopet er færdig med at gå igennem alle tokens og indsætte sql queries i listen for hver token. Den samlede liste returneres.
            }
            else
            {
                return null;
            }
        }
    }
}
