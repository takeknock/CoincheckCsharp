using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Coincheck
{
    public class TickerFactory
    {
        public TickerFactory()
        { }

        public Ticker createTicker(Ticker ticker)
        {

            return new Ticker(ticker);
        }

        public Ticker createTicker(Dictionary<String, double> tickerDictionary)
        {
            Ticker ticker = new Ticker();
            ticker._ask = tickerDictionary["ask"];
            ticker._bid = tickerDictionary["bid"];
            ticker._high = tickerDictionary["high"];
            ticker._low = tickerDictionary["low"];
            ticker._volume = tickerDictionary["volume"];
            ticker._timestamp = Convert.ToInt32(tickerDictionary["timestamp"]);

            return new Ticker(ticker);
        }
    }
}
