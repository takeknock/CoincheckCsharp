using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coincheck
{
    public class UnixTime
    {
        private readonly DateTime UNIXEPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public long _value { get; private set; }

        public UnixTime(DateTime dateTime)
        {
            _value = (long)(dateTime.ToUniversalTime() - UNIXEPOCH).TotalSeconds;
        }
    }
}
