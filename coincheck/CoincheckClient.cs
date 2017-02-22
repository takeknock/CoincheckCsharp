﻿using System;
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
            {"createOrders", "/api/exchange/orders" },
            {"fxRates", "/api/rate/" }
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
            //string createOrderTarget = _target + "/api/exchange/orders";
            //Dictionary<string, string> headers = getHeaders(createOrderTarget);

            //HttpRequestMessage request = new HttpRequestMessage();
            //foreach(var i in headers)
            //{
            //    request.Headers.Add(i.Key, i.Value);
            //}
            //request.Method = HttpMethod.Get;
            //var result = getResponse(request);
            Uri path = new Uri(paths["createOrders"], UriKind.Relative);

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
            
            //HttpClient http = new HttpClient();
            //http.BaseAddress = new Uri(_target);

            Uri path = new Uri(paths["assets"], UriKind.Relative);

            string text = await Sender.SendAsync(http, path, _key, _secret, "GET");

            return text;
            //string method = "POST";

            //Task<string> json = Sender.SendAsync(http, path, _key, _secret, method);
            //Console.WriteLine("getOutstandingOrders :");
            //Console.WriteLine(json);
            //return json.ToString();
            //string outstandingsTarget = _target + "/api/exchange/orders/opens";
            //Dictionary<string, string> headers = getHeaders(outstandingsTarget);

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(outstandingsTarget);
            //request.Headers.Add("ACCESS-KEY", headers["ACCESS-KEY"]);
            //request.Headers.Add("ACCESS-NONCE", headers["ACCESS-NONCE"]);
            //request.Headers.Add("ACCESS-SIGNATURE", headers["ACCESS-SIGNATURE"]);
            ////request.Headers.Host = _target;
            ////foreach (var i in headers)
            ////{
            ////    request.Headers.Add(i.Key, i.Value);
            ////}

            ////string content = "{\"ACCESS-KEY\":" + headers["ACCESS-KEY"] + ",\"ACCESS-NONCE\":" + headers["ACCESS-NONCE"] + ",\"ACCESS-SIGNATURE\":" + headers["ACCESS-SIGNATURE"] + "}";
            ////request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            ////var result = getResponse((HttpRequestMessage)request);
            //HttpWebResponse result = (HttpWebResponse)request.GetResponse();
            //return result.StatusCode.ToString();
        }

         public void getOwnTransactionResults()
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(_target);
            Uri path = 
                new Uri("/api/exchange/orders/transactions_pagination", UriKind.Relative);

            string method = "GET";

            Task<string> json = Sender.SendAsync(http, path, _key, _secret, method);
            //Dictionary<string, string> headers = getHeaders(transactionsTarget);
            Console.WriteLine("orders result:");
            Console.WriteLine(json);

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

        #region private functions
        private async Task<string> getResponse(HttpRequestMessage request)
        {
            var client = new HttpClient();
            //var response = new HttpResponseMessage();

            var response = await client.SendAsync(request);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }


        private Dictionary<string, string> getHeaders(string uri, string body="hoge=foo")
        {
            UnixTime unixtime = new UnixTime();
            string nonce = unixtime.ToString();
            string message = nonce + uri + body;

            var keyByte = Encoding.UTF8.GetBytes(_secret);
            string signature = "";
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(message));
                signature = convertByteToString(hmacsha256.Hash);
            }

            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("ACCESS-KEY", _key);
            result.Add("ACCESS-NONCE", nonce);
            result.Add("ACCESS-SIGNATURE", signature);

            return new Dictionary<string, string>(result);
        }

        private string convertByteToString(byte[] buffer)
        {
            string stringBinary = "";
            for (int i = 0; i < buffer.Length; ++i)
            {
                stringBinary += buffer[i].ToString("X2");
            }

            return stringBinary;
        }
        #endregion
    }
}
