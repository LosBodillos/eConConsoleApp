using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConConsoleApp
{
    class Program
    {
      
        static void Main(string[] args)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();

            databaseConnection.InsertCustomers();

        }

       
    }
}
