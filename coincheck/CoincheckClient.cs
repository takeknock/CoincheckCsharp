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
        private String target;

        public CoincheckClient()
        {
            target = "https://coincheck.com";
        }

        public String getTicker()
        {
            String tickerTarget = target + "/api/ticker";
            String response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(tickerTarget);
            }

            return response;
        }       
    }
}
