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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using Newtonsoft.Json;


namespace Coincheck
{ 
    public class CoincheckClient
    {
        private string _target = "https://coincheck.com";
        private HttpClient http = new HttpClient();
        private List<string> _gettablePair = new List<string>() { "btc_jpy", "eth_jpy", "etc_jpy", "dao_jpy", "lsk_jpy", "fct_jpy", "xmr_jpy", "rep_jpy", "xrp_jpy", "zec_jpy", "eth_btc", "etc_btc", "lsk_btc", "fct_btc", "xmr_btc", "rep_btc", "xrp_btc", "zec_btc" };

        private Dictionary<string, string> paths = new Dictionary<string, string>
        {
            {"ticker", "/api/ticker" },
            {"trades", "/api/trades" },
            {"orderbook", "/api/order_books" },
            {"orderrate", "/api/exchange/orders/rate" },
            {"assets", "/api/accounts/balance" },
            {"orders", "/api/exchange/orders" },
            {"fxRates", "/api/rate/" },
            {"openorders", "/api/exchange/orders/opens" }
        };

        private string _key;
        private string _secret;

        public CoincheckClient()
        {
            http.BaseAddress = new Uri("https://coincheck.com");
        }

        public CoincheckClient(string key, string secret)
        {
            _key = key;
            _secret = secret;
            http.BaseAddress = new Uri("https://coincheck.com");
        }


        async public Task<string> getTickerAsync()
        {
            Uri path = new Uri(paths["ticker"], UriKind.Relative);
            string response = await Sender.SendAsync(http, path, _key, _secret, "GET");
            
            return response;
        }       

        async public Task<string> getTradesAsync()
        {
            Uri path = new Uri(paths["trades"], UriKind.Relative);
            string response = await Sender.SendAsync(http, path, _key, _secret, "GET");
            return response;
        }

        async public Task<string> getOrderbookAsync()
        {
            Uri path = new Uri(paths["orderbook"], UriKind.Relative);
            string response = await Sender.SendAsync(http, path, _key, _secret, "GET");

            return response;
        }

        //error
        async public Task<string> getExchangeRateAsync(string order, string pair, double amount, int price)
        {
            Uri path = new Uri(paths["orderrate"], UriKind.Relative);
            //Dictionary<string, string> parameters =
            //    new Dictionary<string, string>
            //    {
            //        {"order_type", order },
            //        {"pair", pair },
            //        {"amount", amount.ToString() },
            //        {"price", price.ToString() }
            //    };
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    {"order_type", order },
                    {"pair", pair }
                };
            string response = await Sender.SendAsync(http, path, _key, _secret, "GET", parameters);

            return response;
        }

        async public Task<string> getFxRateAsync(string pair)
        {   
            if (!_gettablePair.Contains(pair))
            {
                throw new NotSupportedException("[" + pair + "] is not supported.");
            }

            string fxRateTarget = paths["fxRates"] + pair;
            Uri path = new Uri(fxRateTarget, UriKind.Relative);

            string response = await Sender.SendAsync(http, path, _key, _secret, "GET");

            return response;
        }

        // error
        async public Task<string> createOrderAsync(string orderType, double rate, double amount, string pair)
        {

            // not tested
            Uri path = new Uri(paths["orders"], UriKind.Relative);

            var param = new Dictionary<string, string>
            {
                {"pair", pair },
                {"order_type", orderType },
                {"rate", rate.ToString() },
                {"amount", amount.ToString() }
            };
            string result = await Sender.SendAsync(http, path,_key, _secret, "POST", param);
            return result;
        }

        async public Task<string> getOutstandingOrders()
        {   
            Uri path = new Uri(paths["assets"], UriKind.Relative);

            string text = await Sender.SendAsync(http, path, _key, _secret, "GET");

            return text;
        }

        async public Task<string> getOwnTransactionResultsAsync()
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(_target);
            Uri path = 
                new Uri("/api/exchange/orders/transactions_pagination", UriKind.Relative);

            string method = "GET";

            string json = await Sender.SendAsync(http, path, _key, _secret, method);
            //Dictionary<string, string> headers = getHeaders(transactionsTarget);
            return json;

        }

        async public void getOrderBooks()
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(_target);
            Uri path = new Uri("/api/order_books", UriKind.Relative);

            string json = await Sender.SendAsync(http, path, _key, _secret, "GET");

            Console.WriteLine("Order books: ");
            Console.WriteLine(json);
            //return res.ToString();
        }

        async public Task<string> getOpenordersAsync()
        {
            Uri path = new Uri(paths["openorders"], UriKind.Relative);
            string orders = await Sender.SendAsync(http, path, _key, _secret, "GET");

            return orders;
        }

    }
}
