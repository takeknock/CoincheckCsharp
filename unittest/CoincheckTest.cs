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
            client = new CoincheckClient(apiKey, secretKey, sender);
        }
        
        [Test]
        public void testGetTicker()
        {
            Task<string> ticker = client.getTickerAsync();
            Assert.AreEqual("keysecrethttps://coincheck.com//api/tickerGET", ticker.Result.ToString());
        }

        [Test]
        public void testGetTrade()
        {
            Task<string> trades = client.getTradesAsync();
            Assert.AreEqual("keysecrethttps://coincheck.com//api/tradesGET", trades.Result.ToString());
        }

        [Test]
        public void testGetOrderbooks()
        {
            Task<string> orderBook = client.getOrderbookAsync();
            Console.WriteLine(orderBook.Result.ToString());
            Assert.AreEqual("keysecrethttps://coincheck.com//api/order_booksGET", orderBook.Result.ToString());
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
            Assert.AreEqual(expected, response.Result.ToString());
        }
        [Test]
        public void testGetFxRate()
        {
            string correctPair = "btc_jpy";
            string notSupportedPair = "ttt_ttt";
            Task<string> fxRate = client.getFxRateAsync(correctPair);
            Console.WriteLine(fxRate.Result.ToString());
            Assert.AreEqual("keysecrethttps://coincheck.com//api/rate/btc_jpyGET", fxRate.Result.ToString());

            Task<string> except = client.getFxRateAsync(notSupportedPair);
            Assert.That(() =>
                except.Result.ToString(), Throws.Exception.TypeOf<AggregateException>());

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
            Task<string> actual = client.createOrderAsync(orderType, 10000.0, 0.5, "btc_jpy");
            Console.WriteLine(actual.Result.ToString());
            Assert.AreEqual("keysecrethttps://coincheck.com//api/exchange/ordersPOST", actual.Result.ToString());

        }

        [Test]
        public void testcreateOrderAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetOutstandingOrdersAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetOwnTransactionPaginationAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetOwnTransactionAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetOpenordersAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testcancelOrder()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testcheckLeveragePositions()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testcheckLeverageBalance()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetAccountInfo()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetSendHistory()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetDepositHistory()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetBankAccountInfo()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testapplyBorrowingMoney()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetBorrowInfo()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testsendBitcoin()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testregistBankAccount()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testdeleteBankAccount()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetWithdrawHistory()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testwithdraw()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testcancelWithdraw()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testrepay()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testtransferToLeverage()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testtransferFromLeverage()
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
