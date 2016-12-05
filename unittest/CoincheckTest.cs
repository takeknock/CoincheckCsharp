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
        [Test]
        public void testGetTicker()
        {
            var client = new CoincheckClient();
            String ticker = client.getTicker();
            Assert.AreNotEqual(ticker, null);
        }

    }
}
