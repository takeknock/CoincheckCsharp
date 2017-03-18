﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coincheck.Util
{
    class HttpMethodBuilder
    {
        internal POST createPOST(Dictionary<string, string> parameters)
        {
            return new POST(parameters);
        }

        internal GET createGET()
        {
            return new GET();
        }

        internal DELETE createDELETE()
        {
            return new DELETE();
        }
    }
}
