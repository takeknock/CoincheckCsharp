using System;
using System.Collections.Generic;
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
            clear();
            this._key = apiKey;
            this._secret = secret;
            this._http = http;
            this._method = method;
            this._param = parameters;
            //string paramStr = _param.Select(e => e.Key.Concat(e.Value));
            return _key + _secret + _http.BaseAddress.ToString() + path.ToString() + method;
        }

        private void clear()
        {
            _key = null;
            _secret = null;
            _http = null;
            _method = null;
            _param = null;
        }

    }
}
