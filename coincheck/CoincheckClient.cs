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


namespace Coincheck
{ 
    public class CoincheckClient
    {
        private readonly string _target = "https://coincheck.com";

        private List<string> _gettablePair = new List<string>() { "btc_jpy", "eth_jpy", "etc_jpy", "dao_jpy", "lsk_jpy", "fct_jpy", "xmr_jpy", "rep_jpy", "xrp_jpy", "zec_jpy", "eth_btc", "etc_btc", "lsk_btc", "fct_btc", "xmr_btc", "rep_btc", "xrp_btc", "zec_btc" };

        private string _key;
        private string _secret;

        public CoincheckClient()
        {
        }

        public CoincheckClient(string key, string secret)
        {
            _key = key;
            _secret = secret;
        }

        public string getTicker()
        {
            string tickerTarget = _target + "/api/ticker";
            string response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(tickerTarget);
            }

            return response;
        }       

        public string getTrades()
        {
            string tradesTarget = _target + "/api/trades";
            string response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(tradesTarget);
            }

            return response;
        }

        public string getOrderbooks()
        {

            string orderbookTarget = _target + "/api/order_books";
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

            string fxRateTarget = _target + "/api/rate/" + pair;

            Console.WriteLine(fxRateTarget);

            string response;
            using (WebClient client = new WebClient())
            {
                response = client.DownloadString(fxRateTarget);
            }

            return response;
        }


        public string createOrder(string orderType, double rate, double amount, string marketBuyAmount, string positionId, string pair)
        {
            // not tested
            string createOrderTarget = _target + "/api/exchange/orders";
            Dictionary<string, string> headers = getHeaders(createOrderTarget);
            
            HttpRequestMessage request = new HttpRequestMessage();
            foreach(var i in headers)
            {
                request.Headers.Add(i.Key, i.Value);
            }
            request.Method = HttpMethod.Get;
            var result = getResponse(request);
            return result.Result;
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


        private Dictionary<string, string> getHeaders(string uri, string body="")
        {
            UnixTime now = new UnixTime(DateTime.Now);
            string nonce = now._value.ToString();
            string message = nonce + uri + body;

            var keyByte = Encoding.UTF8.GetBytes(_secret);
            string signature = "";
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(message));
                signature = convertByteToString(hmacsha256.Hash);
            }

            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Content-Type", "application/json");
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
