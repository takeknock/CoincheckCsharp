using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Collections.Specialized;

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

        public string getOrderbooks()
        {

            string orderbookTarget = target + "/api/order_books";
            string response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(orderbookTarget);
            }

            return response;
        }

        //public string getExchangeRate(string order, string pair, double amount, double price)
        //{
        //    string exchangeRateTarget = target + "/api/exchange/orders/rate";
        //    string parameters = "&order_type=" + order + "&pair=" + pair + "&amount=" + amount.ToString() + "&price=" + price.ToString();
        //    string thisTarget = exchangeRateTarget + parameters;
        //    string response;

        //    NameValueCollection nvc = new NameValueCollection();
        //    nvc.Add("order_type", order);
        //    nvc.Add("pair", pair);
        //    nvc.Add("amount", amount.ToString());
        //    nvc.Add("price", price.ToString());
            
        //    using (WebClient client = new WebClient())
        //    {
        //        client.QueryString = nvc;
        //        response = client.DownloadString(thisTarget);
        //    }


        //        return response;
        //}
    }
}
