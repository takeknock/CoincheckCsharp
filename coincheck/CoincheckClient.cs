using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;


namespace Coincheck
{ 
    public class CoincheckClient
    {
        private string target;

        public CoincheckClient()
        {
            target = "https://coincheck.com";
        }

        public string getTicker()
        {
            string tickerTarget = target + "/api/ticker";
            string response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(tickerTarget);
            }

            return response;
        }       

        public string getTrades()
        {
            string tradesTarget = target + "/api/trades";
            string response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(tradesTarget);
            }

            return response;
        }

        public string 
    }
}
