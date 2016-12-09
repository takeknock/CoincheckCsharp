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
        private string _target;
        private List<string> _gettablePair = new List<string>() { "btc_jpy", "eth_jpy", "etc_jpy", "dao_jpy", "lsk_jpy", "fct_jpy", "xmr_jpy", "rep_jpy", "xrp_jpy", "zec_jpy", "eth_btc", "etc_btc", "lsk_btc", "fct_btc", "xmr_btc", "rep_btc", "xrp_btc", "zec_btc" };

        private string _key;
        private string _secret;

        public CoincheckClient()
        {
            _target = "https://coincheck.com/";
        }

        public CoincheckClient(string key, string secret)
        {
            _key = key;
            _secret = secret;
        }

        public string getTicker()
        {
            string tickerTarget = _target + "api/ticker";
            string response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(tickerTarget);
            }

            return response;
        }       

        public string getTrades()
        {
            string tradesTarget = _target + "api/trades";
            string response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(tradesTarget);
            }

            return response;
        }

        public string getOrderbooks()
        {

            string orderbookTarget = _target + "api/order_books";
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

        public string getFxRate(string pair)
        {
            
            if (!_gettablePair.Contains(pair))
            {
                throw new NotSupportedException(pair + "is not supported.");
            }

            string fxRateTarget = _target + "api/rate/" + pair;

            Console.WriteLine(fxRateTarget);

            string response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(fxRateTarget);
            }

            return response;

        }


    }
}
