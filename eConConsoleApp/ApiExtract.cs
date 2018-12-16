using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConConsoleApp
{
    class ApiExtract
    {


        public JObject GetCustomers(String url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            JObject o = JObject.Parse(response.Content);

            return o;
        }

    }
}
