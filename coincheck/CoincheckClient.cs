using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace Coincheck
{ 
    public class CoincheckClient
    {
        private HttpClient client;
        private string target;
        CoincheckClient()
        {
            client = new HttpClient();
            target = "https://coincheck.com";
        }
        
    }
}
