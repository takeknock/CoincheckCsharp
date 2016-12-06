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
            string ticker = client.getTicker();
            Assert.AreNotEqual(ticker, "");
            
        }

        [Test]
        public void testGetTrade()
        {
            string trades = client.getTrades();
            //Console.WriteLine(trades);
            Assert.AreNotEqual(trades, "");
        }

        [Test]
        public void testGetOrderbooks()
        {
            string orderBook = client.getOrderbooks();
            Console.WriteLine(orderBook);
            Assert.AreNotEqual(orderBook, "");
        }

    }
}
