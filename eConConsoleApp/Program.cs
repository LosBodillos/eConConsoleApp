using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace eConConsoleApp
{
    class Program
    {
        // vores "kør program" metode
        static void Main(string[] args) 
        {
            DatabaseConnection databaseConnection = new DatabaseConnection(); // laver en lokal variabel af klassen

            databaseConnection.InsertCustomers(); // kalder metoden der indsætter alle customers i E-conomic i databasen

        }

       
    }
}
