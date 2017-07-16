using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography;

namespace Coincheck
{
    public class Sender : ISender
    {

        public async Task<string> SendAsync(HttpClient http, Uri path, string apiKey, string secret, Util.IHttpMethod method)
        {
            string response = await method.sendAsync(http, path);
            return response;
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
