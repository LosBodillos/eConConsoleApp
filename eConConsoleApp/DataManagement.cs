using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConConsoleApp
{
    class DataManagement
    {

        public List<SqlCommand> GenerateCustomerInserts(JObject o)
        {


            if (o.ContainsKey("collection"))
            {
                List<SqlCommand> commands = new List<SqlCommand>();

                JToken jToken = o.GetValue("collection");

                foreach (JToken jt in jToken)
                {
                    var customerId = jt.Value<int>("customerNumber");
                    var name = jt.Value<String>("name");


                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO Customer (customerId, name) VALUES (@customerId, @name)";
                    cmd.Parameters.AddWithValue("customerId", customerId);
                    cmd.Parameters.AddWithValue("name", name);

                    commands.Add(cmd);

                }

                return commands;
            }
            else
            {
                return null;
            }
        }
    }
}
