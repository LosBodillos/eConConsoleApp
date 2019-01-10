using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace eConConsoleApp
{
    class DatabaseConnection
    {
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; // instans variabel med information om "adressen" til vores DB
        private readonly string customerURL = "https://restapi.e-conomic.com/customers?=demo&=demo&demo=true"; // vores HTTP request til E-conomic hvor vi fortæller hvad data vi gerne vil have

        ApiExtract ApiExtract = new ApiExtract(); // global variabel der kalder en anden klasse
        DataManagement DataManagement = new DataManagement(); //global variabel der kalder en anden klasse

        // metode der åbner db og indsætter customer data
        public void InsertCustomers()
        {

            JObject o = ApiExtract.GetCustomers(customerURL); // klader metode der returnerer en liste af kunder pakket i JSON format
            List<SqlCommand> customerInserts = DataManagement.GenerateCustomerInserts(o); // kalder metode der udpakker JSON og laver liste af sql inserts med kundeinformation

            TransactionOptions to = new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }; // her bestemmer vi hvordan vi vil håndtere samtidighed. vi vælger isolationsniveauet for vore scope.
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, to)) // Laver et ransaktionsscope hvori vi kan execute kommandoer, alle kommandoer i scopet vil blive eksekveret som én enhed eller rullet tilbage som én enhed
            {

                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING)) // laver en connectionvariabel med DB adresse som vi kan bruge i hele "using" scopet
                {
                    try  // vores forsøg på at åbne en forbindelse er "pakket ind" i en try catch
                    {

                        connection.Open();
                        ClearTables(connection); // kalder en privat metode der renser de ønskede databasetabeller. Kan ses længere nede
                    }
                    catch (SqlException e)
                    {
                        e.Message.ToString();
                    }

                    foreach (SqlCommand cmd in customerInserts) // dette loop executer hver enkelt sqlCommand vi har i vores liste
                    {
                        cmd.Connection = connection; // fortæller hvad connection den skal bruge
                        cmd.ExecuteNonQuery(); // sender query afsted
                    }

                    connection.Close(); // luk forbindelsen til databasen
                }

             scope.Complete(); // complete committer transaktionen. er der sket en exceptionen inde i scopet klades complete ikke og transaktionen laver roll back
            }
        }

        // metode der tømmer alle tabeller i eCon_db
        private void ClearTables(SqlConnection connection) 
        {
            SqlCommand sqlCommand1 = new SqlCommand { CommandText = "Delete from sale" }; // laver sql query
            SqlCommand sqlCommand2 = new SqlCommand { CommandText = "Delete from Customer" };
            sqlCommand1.Connection = connection; // fortæller at den skal bruge den specifikke connection
            sqlCommand2.Connection = connection;
            sqlCommand1.ExecuteNonQuery(); // eksekverer query
            sqlCommand2.ExecuteNonQuery();
        }

    }
}
