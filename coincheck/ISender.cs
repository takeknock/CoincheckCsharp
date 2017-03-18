using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Coincheck
{
    public interface ISender
    {
        Task<string> SendAsync(HttpClient http, Uri path, string apiKey, string secret, Util.IHttpMethod method);
    }
}
