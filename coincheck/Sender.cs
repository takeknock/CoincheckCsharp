using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography;

namespace Coincheck
{
    static class Sender
    {

        static internal async Task<string> SendAsync(HttpClient http, Uri path, string apiKey, string secret, string method, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();

            var content = new FormUrlEncodedContent(parameters);
            string param = await content.ReadAsStringAsync();

            UnixTime unixtime = new UnixTime();
            string nonce = unixtime.ToString();

            Uri uri = new Uri(http.BaseAddress, path);
            string message = makeMessage(nonce, uri.ToString(), param);
            string sign = generateSignature(secret, message);
            setHttpHeaders(ref http, apiKey, nonce, sign);

            HttpResponseMessage res;
            switch (method)
            {
                case "POST":
                    res = await http.PostAsync(path, content);
                    break;
                case "GET":
                    res = await http.GetAsync(path);
                    break;
                case "DELETE":
                    res = await http.DeleteAsync(path);
                    break;
                default:
                    throw new ArgumentException("You should choose POST or GET as method.", method);
            }
            string text = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
                return "";

            return text;
        }

        static private string makeMessage(string nonce, string uri, string param)
        {
            return nonce + uri + param;
        }

        static private string generateSignature(string secret, string message)
        {
            byte[] hash = new HMACSHA256(Encoding.UTF8.GetBytes(secret)).ComputeHash(Encoding.UTF8.GetBytes(message));
            string signature = BitConverter.ToString(hash).ToLower().Replace("-", "");
            return signature;
        }

        static private void setHttpHeaders(ref HttpClient http,
            string apiKey, string nonce, string sign)
        {
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("ACCESS-KEY", apiKey);
            http.DefaultRequestHeaders.Add("ACCESS-NONCE", nonce);
            http.DefaultRequestHeaders.Add("ACCESS-SIGNATURE", sign);

        }

    }
}
