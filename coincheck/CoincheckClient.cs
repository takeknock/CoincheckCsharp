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
using Coincheck.Util;

namespace Coincheck
{ 
    public class CoincheckClient
    {
        private ISender sender = new Sender();
        //private HttpClient http = new HttpClient();
        private List<string> _gettablePair = new List<string>() { "btc_jpy", "eth_jpy", "etc_jpy", "dao_jpy", "lsk_jpy", "fct_jpy", "xmr_jpy", "rep_jpy", "xrp_jpy", "zec_jpy", "eth_btc", "etc_btc", "lsk_btc", "fct_btc", "xmr_btc", "rep_btc", "xrp_btc", "zec_btc" };

        private string baseAddress = "https://coincheck.com";
        private HttpMethodBuilder httpMethodBuilder = new HttpMethodBuilder();

        private Dictionary<string, string> paths = new Dictionary<string, string>
        {
            {"ticker", "/api/ticker" },
            {"trades", "/api/trades" },
            {"orderbook", "/api/order_books" },
            {"orderrate", "/api/exchange/orders/rate" },
            {"assets", "/api/accounts/balance" },
            {"orders", "/api/exchange/orders" },
            {"fxRates", "/api/rate/" },
            {"openorders", "/api/exchange/orders/opens" },
            {"transactions", "/api/exchange/orders/transactions" },
            {"pagination", "/api/exchange/orders/transactions_pagination" },
            {"leveragePositions", "/api/exchange/leverage/positions" },
            {"leverageBalance", "/api/accounts/leverage_balance" },
            {"account", "/api/accounts" },
            {"send", "/api/send_money" },
            {"deposit", "/api/deposit_money" },
            {"bankAccount", "/api/bank_accounts" },
            {"borrows","/api/lending/borrows" },
            {"borrowInfo", "/api/lending/borrows/matches" },
            {"withdraw", "/api/withdraws" },
            {"toLeverage", "/api/exchange/transfers/to_leverage" },
            {"fromLeverage", "/api/exchange/transfers/from_leverage" }
        };

        private string _key;
        private string _secret;

        public CoincheckClient()
        {
            //http.BaseAddress = new Uri("https://coincheck.com");
        }

        public CoincheckClient(ISender argSender)
        {
            //http.BaseAddress = new Uri("https://coincheck.com");
            sender = argSender;
        }

        public CoincheckClient(string key, string secret)
        {
            _key = key;
            _secret = secret;
            //http.BaseAddress = new Uri("https://coincheck.com");
        }

        public CoincheckClient(string key, string secret, ISender argSender)
        {
            _key = key;
            _secret = secret;
            sender = argSender;
            //http.BaseAddress = new Uri("https://coincheck.com");
        }


        async public Task<string> getTickerAsync()
        {
            Uri path = new Uri(paths["ticker"], UriKind.Relative);
            string response = await sender.SendAsync(http, path, _key, _secret, "GET");
            
            return response;
        }       

        async public Task<string> getTradesAsync()
        {
            Uri path = new Uri(paths["trades"], UriKind.Relative);
            string response = await sender.SendAsync(http, path, _key, _secret, "GET");
            return response;
        }

        async public Task<string> getOrderbookAsync()
        {
            Uri path = new Uri(paths["orderbook"], UriKind.Relative);
            string response = await sender.SendAsync(http, path, _key, _secret, "GET");

            return response;
        }
        
        async public Task<string> getExchangeRateAsync(string order, string pair, double amount, double price)
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
            string response = await sender.SendAsync(http, path, _key, _secret, "GET", parameters);

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

            string response = await sender.SendAsync(http, path, _key, _secret, "GET");

            return response;
        }
        
        async public Task<string> createOrderAsync(string orderType, double rate, double amount, string pair)
        {
            if (pair != "btc_jpy")
                throw new NotSupportedException(pair + " is not supported.");

            Uri path = new Uri(paths["orders"], UriKind.Relative);

            var param = new Dictionary<string, string>
            {
                {"pair", pair },
                {"order_type", orderType },
                {"rate", rate.ToString() },
                {"amount", amount.ToString() }
            };
            string result = await sender.SendAsync(http, path,_key, _secret, "POST", param);
            return result;
        }

        async public Task<string> getOutstandingOrdersAsync()
        {   
            Uri path = new Uri(paths["assets"], UriKind.Relative);

            string text = await sender.SendAsync(http, path, _key, _secret, "GET");

            return text;
        }

        async public Task<string> getOwnTransactionPaginationAsync()
        {
            Uri path = new Uri(paths["pagination"], UriKind.Relative);

            string method = "GET";

            string json = await sender.SendAsync(http, path, _key, _secret, method);
            return json;

        }

        async public Task<string> getOwnTransactionAsync()
        {
            Uri path = new Uri(paths["transactions"], UriKind.Relative);
            string method = "GET";

            string json = await sender.SendAsync(http, path, _key, _secret, method);
            return json;
        }

        async public Task<string> getOpenordersAsync()
        {
            Uri path = new Uri(paths["openorders"], UriKind.Relative);
            string orders = await sender.SendAsync(http, path, _key, _secret, "GET");

            return orders;
        }

        async public Task<string> cancelOrderAsync(string orderId)
        {
            string pId = paths["orders"] + "/" + orderId;
            Uri pathId = new Uri(pId, UriKind.Relative);

            string cancelOrder = await sender.SendAsync(http, pathId, _key, _secret, "DELETE");

            return cancelOrder;
        }

        async public Task<string> checkLeveragePositionsAsync()
        {
            Uri path = new Uri(paths["leveragePositions"], UriKind.Relative);

            string checkResult = await sender.SendAsync(http, path, _key, _secret, "GET");

            return checkResult;
        }

        async public Task<string> checkLeverageBalanceAsync()
        {
            Uri path = new Uri(paths["leverageBalance"], UriKind.Relative);
            string checkResult = await sender.SendAsync(http, path, _key, _secret, "GET");

            return checkResult;

        }

        async public Task<string> getAccountInfoAsync()
        {
            Uri path = new Uri(paths["account"], UriKind.Relative);
            string info = await sender.SendAsync(http, path, _key, _secret, "GET");
            return info;
        }


        //async public Task<string> sendMoney(
        //    string targetAddress, double amount, double fee = 0.0)
        //{
        //    Uri path = new Uri(paths["send"], UriKind.Relative);
        //    Dictionary<string, string> param = new Dictionary<string, string>()
        //    {
        //        {"address", targetAddress },
        //        {"amount", amount.ToString() },
        //        {"fee", fee.ToString() }
        //    };
        //    string sendResult = 
        //        await sender.SendAsync(http, path, _key, _secret, "POST", param);

        //    return sendResult;

        //}

        async public Task<string> getSendHistoryAsync(string currency = "BTC")
        {
            string param = "?currency=" + currency;
            Uri path = new Uri(paths["send"] + param, UriKind.Relative);

            string sendHistory =
                await sender.SendAsync(http, path, _key, _secret, "GET");
            return sendHistory;
        }

        async public Task<string> getDepositHistoryAsync(string currency = "btc_jpy")
        {
            string param = "?currency=" + currency;
            Uri path = new Uri(paths["deposit"] + param, UriKind.Relative);

            string history = await sender.SendAsync(http, path, _key, _secret, "GET");
            return history;
        }

        async public Task<string> getBankAccountInfoAsync()
        {
            Uri path = new Uri(paths["bankAccount"], UriKind.Relative);
            string info = await sender.SendAsync(http, path, _key, _secret, "GET");
            return info;
        }
        

        async public Task<string> applyBorrowingMoneyAsync(double amount, string currency)
        {
            Uri path = new Uri(paths["borrows"], UriKind.Relative);
            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                {"amount", amount.ToString() },
                {"currency", currency }
            };
            string result = await sender.SendAsync(http, path, _key, _secret, "POST", param);
            return result;
        }

        async public Task<string> getBorrowInfoAsync()
        {
            Uri path = new Uri(paths["borrowInfo"], UriKind.Relative);
            string info = await sender.SendAsync(http, path, _key, _secret, "GET");
            return info;
        }

        // not tested
        async public Task<string> sendBitcoinAsync(string address, double amount)
        {
            Uri path = new Uri(paths["send"], UriKind.Relative);
            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                {"address", address },
                {"amount", amount.ToString() }
            };
            string sendStatus = await sender.SendAsync(http, path, _key, _secret, "POST", param);
            return sendStatus;
        }

        async public Task<string> registBankAccountAsync(
            string bankName, string branchName, string accountType, string number, string resitedName)
        {
            Uri path = new Uri(paths["bankAccount"], UriKind.Relative);
            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                {"bank_name", bankName },
                {"branch_name", branchName },
                {"bank_account_type", accountType },
                {"number", number },
                {"name", resitedName }
            };
            string result = await sender.SendAsync(http, path, _key, _secret, "POST", param);
            return result;
        }

        async public Task<string> deleteBankAccountAsync(string id)
        {
            Uri path = new Uri(paths["bankAccount"] + "/" + id, UriKind.Relative);
            string status = await sender.SendAsync(http, path, _key, _secret, "DELETE");
            return status;
        }

        async public Task<string> getWithdrawHistoryAsync()
        {
            Uri path = new Uri(paths["withdraw"], UriKind.Relative);
            string history = await sender.SendAsync(http, path, _key, _secret, "GET");
            return history;
        }

        // error
        async public Task<string> withdrawAsync(string bankAccountId, double amount, string currency = "JPY", bool isFast = false)
        {
            Uri path = new Uri(paths["withdraw"], UriKind.Relative);
            //Dictionary<string, string> param = new Dictionary<string, string>()
            //{
            //    {"bank_account_id", bankAccountId },
            //    {"amount", amount.ToString() },
            //    {"currency", currency },
            //    {"is_fast", isFast.ToString() }
            //};
            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                {"bank_account_id", bankAccountId },
                {"amount", amount.ToString() },
                {"currency", currency }
            };

            string status = await sender.SendAsync(http, path, _key, _secret, "POST", param);
            return status;

        }

        async public Task<string> cancelWithdrawAsync(string withdrawId)
        {
            Uri path = new Uri(paths["withdraw"] + "/" + withdrawId, UriKind.Relative);
            string status = await sender.SendAsync(http, path, _key, _secret, "DELETE");
            return status;
        }

        async public Task<string> repay(string borrowingId)
        {
            Uri path = new Uri(paths["borrows"] + "/" + borrowingId + "/repay", UriKind.Relative);
            //Dictionary<string, string> param = new Dictionary<string, string>()
            //{
            //    {"id", borrowingId }
            //};
            string result = await sender.SendAsync(http, path, _key, _secret, "POST");
            return result;
        }

        // error
        async public Task<string> transferToLeverageAsync(double amount, string currency = "JPY")
        {
            Uri path = new Uri(paths["toLeverage"], UriKind.Relative);
            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                {"currency", currency },
                {"amount", amount.ToString()}
            };
            string status = await sender.SendAsync(http, path, _key, _secret, "POST", param);
            return status;
        }

        // error Bad Request
        async public Task<string> transferFromLeverageAsync(double amount, string currency = "JPY")
        {
            Uri path = new Uri(paths["fromLeverage"], UriKind.Relative);
            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                {"currency", currency },
                {"amount", amount.ToString()}
            };
            string status = await sender.SendAsync(http, path, _key, _secret, "POST", param);
            return status;

        }

        private async Task<HttpClient> createHttp(Uri path, Dictionary<string, string> parameters)
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(baseAddress);

            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }
            var content = new FormUrlEncodedContent(parameters);
            string param = await content.ReadAsStringAsync();

            UnixTime unixtime = new UnixTime();
            string nonce = unixtime.ToString();

            Uri uri = new Uri(http.BaseAddress, path);
            string message = makeMessage(nonce, uri.ToString(), param);
            string sign = generateSignature(_secret, message);
            setHttpHeaders(ref http, _key, nonce, sign);
            return http;
        }

        private string makeMessage(string nonce, string uri, string param)
        {
            return nonce + uri + param;
        }

        private string generateSignature(string secret, string message)
        {
            byte[] hash = new HMACSHA256(Encoding.UTF8.GetBytes(secret)).ComputeHash(Encoding.UTF8.GetBytes(message));
            string signature = BitConverter.ToString(hash).ToLower().Replace("-", "");
            return signature;
        }

        private void setHttpHeaders(ref HttpClient http,
            string apiKey, string nonce, string sign)
        {
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("ACCESS-KEY", apiKey);
            http.DefaultRequestHeaders.Add("ACCESS-NONCE", nonce);
            http.DefaultRequestHeaders.Add("ACCESS-SIGNATURE", sign);
        }


    }
}
