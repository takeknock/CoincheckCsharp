﻿using System;
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

        public async Task<string> SendAsync(HttpClient http, Uri path, string apiKey, string secret, string method, Dictionary<string, string> parameters = null)
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

            HttpResponseMessage responce;

            switch (method)
            {
                case "POST":
                    responce = await http.PostAsync(path, content);
                    break;
                case "GET":
                    responce = await http.GetAsync(path);
                    break;
                case "DELETE":
                    responce = await http.DeleteAsync(path);
                    break;
                default:
                    throw new ArgumentException("You should choose POST, GET or DELETE as a method.", method);
            }
            responce.EnsureSuccessStatusCode();

            string text = await responce.Content.ReadAsStringAsync();

            return text;
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
