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

        public Ticker getTicker()
        {
            String tickerTarget = target + "/api/ticker";
            String response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(tickerTarget);
            }

            String quatationRemoved = response.Trim('{').Trim('}').Trim(new Char[] { '"' });
            String[] str = quatationRemoved.Split(',');
            Dictionary<String, double> result = new Dictionary<string, double>();

            Ticker ticker = new Ticker();
            foreach(String s in str)
            {
                char[] removedChar = new char[] { '"', '"'};
                String t = s.Trim(new Char[] { '"' });
                foreach(char c in removedChar)
                {
                    t = t.Replace(c.ToString(), "");
                }
                String[] t2 = t.Split(':');
                double value = Convert.ToDouble(t2[1]);
                result.Add(t2[0], value);
            }

            ticker._ask = result["ask"];
            ticker._bid = result["bid"];
            ticker._high = result["high"];
            ticker._low = result["low"];
            ticker._volume = result["volume"];
            ticker._timestamp = Convert.ToInt32(result["timestamp"]);

            return new Ticker(ticker);
        }       
    }
}
