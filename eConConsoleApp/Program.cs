using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConConsoleApp
{
    class Program
    {
        ApiExtract ApiExtract = new ApiExtract();
        DataManagement DataManagement = new DataManagement();

        String customerURL = "https://restapi.e-conomic.com/customers?=demo&=demo&demo=true";
        String ordersSentURL = "https://restapi.e-conomic.com/orders/sent?=demo&=demo&demo=true";

        static void Main(string[] args)
        {

            
        }

        private List<SqlCommand> GetCustomerData(String URL)
        {
            JObject o = ApiExtract.GetCustomers(URL);
            List<SqlCommand> customerList = DataManagement.GenerateCustomerInserts(o);
            return customerList;
        }
    }
}
