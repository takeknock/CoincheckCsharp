using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace coincheck.Util
{
    class POST : IHttpMethod
    {
        FormUrlEncodedContent content;
        public POST(Dictionary<string, string> parameters)
        {
            content = new FormUrlEncodedContent(parameters);
        }
        public async Task<string> sendAsync(HttpClient http, Uri uri)
        {
            HttpResponseMessage response = await http.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string returnedText = await response.Content.ReadAsStringAsync();
            return returnedText;
        }
    }
}
