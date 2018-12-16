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
        //String customerURL = "https://restapi.e-conomic.com/customers?=demo&=demo&demo=true";
        //String ordersSentURL = "https://restapi.e-conomic.com/orders/sent?=demo&=demo&demo=true";
        //string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        static void Main(string[] args)
        {
            ApiExtract ApiExtract = new ApiExtract();
            DataManagement DataManagement = new DataManagement();
            String customerURL = "https://restapi.e-conomic.com/customers?=demo&=demo&demo=true";
            string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            JObject o = ApiExtract.GetCustomers(customerURL);
            List<SqlCommand> customerInserts = DataManagement.GenerateCustomerInserts(o);

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                try{

                    connection.Open();
                }
                catch (SqlException e)
                {
                    e.Message.ToString();
                }
                

                foreach (SqlCommand cmd in customerInserts)
                {
                    
                }

                connection.Close();
            }

        }

       
    }
}
