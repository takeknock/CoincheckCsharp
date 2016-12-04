using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coincheck
{
    public class Ticker
    {

        public double _last { get; set; }
        public double _bid { get; set; }
        public double _ask { get; set; }
        public double _high { get; set; }
        public double _low { get; set; }
        public double _volume { get; set; }
        public int _timestamp { get; set; }

        public Ticker(Ticker rhs)
        {
            _last = rhs._last;
            _bid = rhs._bid;
            _ask = rhs._ask;
            _high = rhs._high;
            _low = rhs._low;
            _volume = rhs._volume;
            _timestamp = rhs._timestamp;
        }
        public Ticker()
        { }
        public void serialize()
        {
            Console.WriteLine("last: " + _last);
            Console.WriteLine("bid: " + _bid);
            Console.WriteLine("ask: " + _ask);
            Console.WriteLine("high: " + _high);
            Console.WriteLine("low: " + _low);
            Console.WriteLine("volume: " + _volume);
            Console.WriteLine("timestamp: " + _timestamp);

        }
    }
}
