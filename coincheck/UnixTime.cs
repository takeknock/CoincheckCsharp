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

        public UnixTime()
        {
            _value = (long)((DateTimeOffset.UtcNow - UNIXEPOCH).TotalSeconds * 1000.0);
        }

        public override string ToString()
        {
            return _value.ToString();
        }


    }
}
