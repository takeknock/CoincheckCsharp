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
        private CoincheckClient client;

        [SetUp]
        public void Init()
        {
            string apiKey = "key";
            string secretKey = "secret";
            ISender sender = new MockSender();
            client = new CoincheckClient(apiKey, secretKey, sender);
        }
        
        [Test]
        public void testGetTicker()
        {
            Task<string> ticker = client.getTickerAsync();
            Assert.AreEqual(ticker.Result.ToString(), "keysecrethttps://coincheck.com//api/tickerGET");
        }

        [Test]
        public void testGetTrade()
        {
            Task<string> trades = client.getTradesAsync();
            Assert.AreEqual(trades.Result.ToString(), "keysecrethttps://coincheck.com//api/tradesGET");
        }

        [Test]
        public void testGetOrderbooks()
        {
            Task<string> orderBook = client.getOrderbookAsync();
            Console.WriteLine(orderBook.Result.ToString());
            Assert.AreEqual(orderBook.Result.ToString(), "keysecrethttps://coincheck.com//api/order_booksGET");
        }

        [Test]
        public void testGetExchangeRate()
        {
            string order = "buy";
            string pair = "btc_jpy";
            double amount = 10;
            double price = 85000;
            Task<string> response = client.getExchangeRateAsync(order, pair, amount, price);
            Console.WriteLine(response.Result.ToString());
            string expected = "keysecrethttps://coincheck.com//api/exchange/orders/rateGET";
            Assert.AreEqual(response.Result.ToString(), expected);
        }
        [Test]
        public void testGetFxRate()
        {
            string correctPair = "btc_jpy";
            string notSupportedPair = "ttt_ttt";
            Task<string> fxRate = client.getFxRateAsync(correctPair);
            Assert.AreNotEqual(fxRate, "");

            Assert.That(() =>
                client.getFxRateAsync(notSupportedPair), Throws.Exception.TypeOf<NotSupportedException>());
           
        }

        [Test]
        public void testConstructorWithArg()
        {
            // want to test constructor with args
            // WebClientを外だしして外から与えるようにして、依存性を外す
            // 外だしすることで、モックオブジェクトを使用してテストが可能になる

        }

        [Test]
        public void testCreateOrder()
        {
            
            string orderType = "buy";


        }

        [Test]
        public void testGetOutstandingOrder()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }

        [TearDown]
        public void Dispose()
        {

        }
        

    }
}
