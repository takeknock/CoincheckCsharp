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
            string orderType = "buy";
            Task<string> actual = client.createOrderAsync(orderType, 110000.0, 0.5, "btc_jpy");
            Console.WriteLine(actual.Result.ToString());
            Assert.AreEqual("keysecrethttps://coincheck.com//api/exchange/ordersPOST", actual.Result.ToString());
        }
        [Test]
        public void testgetOutstandingOrdersAsync()
        {
            Task<string> actual = client.getOutstandingOrdersAsync();
            Console.WriteLine(actual.Result.ToString());
            string expected = "keysecrethttps://coincheck.com//api/accounts/balanceGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }
        [Test]
        public void testgetOwnTransactionPaginationAsync()
        {
            Task<string> actual = client.getOwnTransactionPaginationAsync();
            Console.WriteLine(actual.Result.ToString());
            string expected = "keysecrethttps://coincheck.com//api/exchange/orders/transactions_paginationGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }
        [Test]
        public void testgetOwnTransactionAsync()
        {
            Task<string> actual = client.getOwnTransactionAsync();
            Console.WriteLine(actual.Result.ToString());
            string expected = "keysecrethttps://coincheck.com//api/exchange/orders/transactionsGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }
        [Test]
        public void testgetOpenordersAsync()
        {
            Task<string> actual = client.getOpenordersAsync();
            Console.WriteLine(actual.Result.ToString());
            string expected = "keysecrethttps://coincheck.com//api/exchange/orders/opensGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }
        [Test]
        public void testcancelOrderAsync()
        {
            Task<string> actual = client.cancelOrderAsync("14");
            Console.WriteLine(actual.Result.ToString());
            string expected = "keysecrethttps://coincheck.com//api/exchange/orders/14DELETE";
            Assert.AreEqual(expected, actual.Result.ToString());
        }
        [Test]
        public void testcheckLeveragePositionsAsync()
        {
            Task<string> actual = client.checkLeveragePositionsAsync();
            Console.WriteLine(actual.Result.ToString());
            string expected = "keysecrethttps://coincheck.com//api/exchange/leverage/positionsGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }
        [Test]
        public void testcheckLeverageBalanceAsync()
        {
            Task<string> actual = client.checkLeverageBalanceAsync();
            Console.WriteLine(actual.Result.ToString());
            string expected = "keysecrethttps://coincheck.com//api/accounts/leverage_balanceGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }
        [Test]
        public void testgetAccountInfoAsync()
        {

        }
        [Test]
        public void testgetSendHistoryAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetDepositHistoryAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetBankAccountInfoAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testapplyBorrowingMoneyAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetBorrowInfoAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testsendBitcoinAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testregistBankAccountAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testdeleteBankAccountAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testgetWithdrawHistoryAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testwithdrawAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testcancelWithdrawAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testrepayAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testtransferToLeverageAsync()
        {
            //var actual = client.getOutstandingOrders();
            //Console.WriteLine(actual);
        }
        [Test]
        public void testtransferFromLeverageAsync()
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
