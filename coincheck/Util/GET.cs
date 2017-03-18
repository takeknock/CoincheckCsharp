using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace coincheck.Util
{
    class GET : IHttpMethod
    {
        public async Task<string> sendAsync(HttpClient http, Uri path)
        {
            return "fuga";
        }
    }
}
