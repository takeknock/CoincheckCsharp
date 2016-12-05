using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Coincheck;

namespace unittest
{
    [TestFixture]
    class CoincheckTest
    {
        private CoincheckClient client = new CoincheckClient();
        [Test]
        public void testGetTicker()
        {
            String ticker = client.getTicker();
            Assert.AreNotEqual(ticker, "");
            
        }

        [Test]
        public void testGetTrade()
        {
            String trades = client.getTrades();
            Console.WriteLine(trades);
            Assert.AreNotEqual(trades, "");
        }

    }
}
