﻿using System;
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
            client = new CoincheckClient(apiKey, secretKey);
        }
        
        [Test]
        public void testGetTicker()
        {
            Task<string> ticker = client.getTickerAsync();
            Assert.AreNotEqual(ticker.Result, "");
            
        }

        [Test]
        public void testGetTrade()
        {
            Task<string> trades = client.getTradesAsync();
            //Console.WriteLine(trades);
            Assert.AreNotEqual(trades, "");
        }

        [Test]
        public void testGetOrderbooks()
        {
            Task<string> orderBook = client.getOrderbookAsync();
            //Console.WriteLine(orderBook);
            Assert.AreNotEqual(orderBook, "");
        }

        [Test]
        public void testGetExchangeRate()
        {
            //string order = "buy";
            //string pair = "btc_jpy";
            //double amount = 10;
            //double price = 85000;
            //string response = client.getExchangeRate(order, pair, amount, price);
            //Console.WriteLine(response);
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
