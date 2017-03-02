using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace unittest
{
    class MockSender : Coincheck.ISender
    {
        public string _key { get; private set; }
        public string _secret { get; private set; }
        public HttpClient _http;
        public string _method;
        public Dictionary<string, string> _param = new Dictionary<string, string>();

        public async Task<string> SendAsync(HttpClient http, Uri path, string apiKey, string secret, string method, Dictionary<string, string> parameters = null)
        {
            this._key = apiKey;
            this._secret = secret;
            this._http = http;
            this._method = method;
            this._param = parameters;

            return "success";
        }

    }
}
