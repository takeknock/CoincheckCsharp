using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace coincheck.Util
{
    interface IHttpMethod
    {
        Task<string> sendAsync(HttpClient http, Uri uri);
    }
}
