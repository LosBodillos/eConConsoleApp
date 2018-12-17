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
    class DatabaseConnection
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly String customerURL = "https://restapi.e-conomic.com/customers?=demo&=demo&demo=true";
        //String ordersSentURL = "https://restapi.e-conomic.com/orders/sent?=demo&=demo&demo=true";

        ApiExtract ApiExtract = new ApiExtract();
        DataManagement DataManagement = new DataManagement();
       

        public void InsertCustomers()
        {

            JObject o = ApiExtract.GetCustomers(customerURL);
            List<SqlCommand> customerInserts = DataManagement.GenerateCustomerInserts(o);

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                try
                {

                    connection.Open();
                    TruncateCustomerTable(connection);
                }
                catch (SqlException e)
                {
                    e.Message.ToString();
                }

                foreach (SqlCommand cmd in customerInserts)
                {
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void TruncateCustomerTable(SqlConnection connection)
        {
            SqlCommand sqlCommand = new SqlCommand { CommandText = "TRUNCATE TABLE Customer;" };
            sqlCommand.Connection = connection;
            sqlCommand.ExecuteNonQuery();
        }

    }
}
