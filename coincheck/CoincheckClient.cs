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


        async public Task<string> getTicker()
        {
            Uri path = new Uri(paths["ticker"], UriKind.Relative);
            string response = await Sender.SendAsync(http, path, _key, _secret, "GET");
            
            return response;
        }       

        async public Task<string> getTrades()
        {
            Uri path = new Uri(paths["trades"], UriKind.Relative);
            string response = await Sender.SendAsync(http, path, _key, _secret, "GET");
            return response;
        }

        async public Task<string> getOrderbook()
        {
            Uri path = new Uri(paths["orderbook"], UriKind.Relative);
            string response = await Sender.SendAsync(http, path, _key, _secret, "GET");

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


        //    return response;
        //}

        async public Task<string> getFxRate(string pair)
        {   
            if (!_gettablePair.Contains(pair))
            {
                throw new NotSupportedException("[" + pair + "] is not supported.");
            }

            string fxRateTarget = paths["fxRates"] + pair;
            Uri path = new Uri(fxRateTarget, UriKind.Relative);

            string response = await Sender.SendAsync(http, path, _key, _secret, "GET");
            //Console.WriteLine(fxRateTarget);

            //string response;
            //using (WebClient client = new WebClient())
            //{
            //    response = client.DownloadString(fxRateTarget);
            //}

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

        public string getOutstandingOrders()
        {
            string outstandingsTarget = _target + "/api/exchange/orders/opens";
            Dictionary<string, string> headers = getHeaders(outstandingsTarget);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(outstandingsTarget);
            request.Headers.Add("ACCESS-KEY", headers["ACCESS-KEY"]);
            request.Headers.Add("ACCESS-NONCE", headers["ACCESS-NONCE"]);
            request.Headers.Add("ACCESS-SIGNATURE", headers["ACCESS-SIGNATURE"]);
            //request.Headers.Host = _target;
            //foreach (var i in headers)
            //{
            //    request.Headers.Add(i.Key, i.Value);
            //}

            //string content = "{\"ACCESS-KEY\":" + headers["ACCESS-KEY"] + ",\"ACCESS-NONCE\":" + headers["ACCESS-NONCE"] + ",\"ACCESS-SIGNATURE\":" + headers["ACCESS-SIGNATURE"] + "}";
            //request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            //var result = getResponse((HttpRequestMessage)request);
            HttpWebResponse result = (HttpWebResponse)request.GetResponse();
            return result.StatusCode.ToString();
        }

        public string getOwnTransactionResults()
        {
            string transactionsTarget = _target + "/api/exchange/orders/transactions";

            Dictionary<string, string> headers = getHeaders(transactionsTarget);

            return "";

        }

        internal async Task<string> Send(HttpClient http, Uri path, string apikey,
            string secret, string method, Dictionary<string, string> query = null, 
            object body = null)
        {
            if (query != null && query.Any())
            {
                var content = new FormUrlEncodedContent(query);
                string q = await content.ReadAsStringAsync();

                path = new Uri(path.ToString() + "?" + q, UriKind.Relative);
            }

            string timestamp = DateTimeOffset.Now.ToUniversalTime().ToString();

            string jsonBody = body == null ? "" : JsonConvert.SerializeObject(body);

            // POSTするメッセージを作成
            string message = timestamp + method + path.ToString() + jsonBody;

            // メッセージをHMACSHA256で署名
            byte[] hash = new HMACSHA256(Encoding.UTF8.GetBytes(secret)).ComputeHash(Encoding.UTF8.GetBytes(message));
            string sign = BitConverter.ToString(hash).ToLower().Replace("-", "");//バイト配列をを16進文字列へ

            // HTTPヘッダをセット
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("ACCESS-KEY", apikey);
            http.DefaultRequestHeaders.Add("ACCESS-TIMESTAMP", timestamp);
            http.DefaultRequestHeaders.Add("ACCESS-SIGN", sign);

            // 送信
            HttpResponseMessage res;
            if (method == "POST")
            {
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                res = await http.PostAsync(path, content);
            }
            else if (method == "GET")
            {
                res = await http.GetAsync(path);
            }
            else
            {
                throw new ArgumentException("method は POST か GET を指定してください。", method);
            }

            //返答内容を取得
            string text = await res.Content.ReadAsStringAsync();

            //通信上の失敗
            if (!res.IsSuccessStatusCode)
                return "";

            return text;
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
