using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Coincheck.Util
{
    class DELETE : IHttpMethod
    {
        public async Task<string> sendAsync(HttpClient http, Uri path)
        {
            HttpResponseMessage response = await http.DeleteAsync(path);
            response.EnsureSuccessStatusCode();
            string returnedText = await response.Content.ReadAsStringAsync();
            return returnedText;
        }
    }
}
