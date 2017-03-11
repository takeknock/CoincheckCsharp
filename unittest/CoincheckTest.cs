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
            string expected = "keysecrethttps://coincheck.com//api/tickerGET";
            Assert.AreEqual(expected, ticker.Result.ToString());
        }

        [Test]
        public void testGetTrade()
        {
            Task<string> trades = client.getTradesAsync();
            string expected = "keysecrethttps://coincheck.com//api/tradesGET";
            Assert.AreEqual(expected, trades.Result.ToString());
        }

        [Test]
        public void testGetOrderbooks()
        {
            Task<string> orderBook = client.getOrderbookAsync();
            string expected = "keysecrethttps://coincheck.com//api/order_booksGET";
            Assert.AreEqual(expected, orderBook.Result.ToString());
        }

        [Test]
        public void testGetExchangeRate()
        {
            string order = "buy";
            string pair = "btc_jpy";
            double amount = 10;
            double price = 85000;
            Task<string> response = client.getExchangeRateAsync(order, pair, amount, price);
            string expected = "keysecrethttps://coincheck.com//api/exchange/orders/rateGET";
            Assert.AreEqual(expected, response.Result.ToString());
        }
        [Test]
        public void testGetFxRate()
        {
            string correctPair = "btc_jpy";
            string notSupportedPair = "ttt_ttt";
            Task<string> fxRate = client.getFxRateAsync(correctPair);
            string expected = "keysecrethttps://coincheck.com//api/rate/btc_jpyGET";
            Assert.AreEqual(expected, fxRate.Result.ToString());

            Task<string> except = client.getFxRateAsync(notSupportedPair);
            Assert.That(() =>
                except.Result.ToString(), Throws.Exception.TypeOf<AggregateException>());

        }

        [Test]
        public void testCreateOrder()
        {
            string orderType = "buy";
            Task<string> actual = client.createOrderAsync(orderType, 10000.0, 0.5, "btc_jpy");
            string expected = "keysecrethttps://coincheck.com//api/exchange/ordersPOST";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testcreateOrderAsync()
        {
            string orderType = "buy";
            Task<string> actual = client.createOrderAsync(orderType, 110000.0, 0.5, "btc_jpy");
            string expected = "keysecrethttps://coincheck.com//api/exchange/ordersPOST";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testgetOutstandingOrdersAsync()
        {
            Task<string> actual = client.getOutstandingOrdersAsync();
            string expected = "keysecrethttps://coincheck.com//api/accounts/balanceGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testgetOwnTransactionPaginationAsync()
        {
            Task<string> actual = client.getOwnTransactionPaginationAsync();
            string expected = "keysecrethttps://coincheck.com//api/exchange/orders/transactions_paginationGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testgetOwnTransactionAsync()
        {
            Task<string> actual = client.getOwnTransactionAsync();
            string expected = "keysecrethttps://coincheck.com//api/exchange/orders/transactionsGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testgetOpenordersAsync()
        {
            Task<string> actual = client.getOpenordersAsync();
            string expected = "keysecrethttps://coincheck.com//api/exchange/orders/opensGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testcancelOrderAsync()
        {
            Task<string> actual = client.cancelOrderAsync("14");
            string expected = "keysecrethttps://coincheck.com//api/exchange/orders/14DELETE";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testcheckLeveragePositionsAsync()
        {
            Task<string> actual = client.checkLeveragePositionsAsync();
            string expected = "keysecrethttps://coincheck.com//api/exchange/leverage/positionsGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testcheckLeverageBalanceAsync()
        {
            Task<string> actual = client.checkLeverageBalanceAsync();
            string expected = "keysecrethttps://coincheck.com//api/accounts/leverage_balanceGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testgetAccountInfoAsync()
        {
            Task<string> actual = client.getAccountInfoAsync();
            string expected = "keysecrethttps://coincheck.com//api/accountsGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testgetSendHistoryAsync()
        {
            Task<string> actual = client.getSendHistoryAsync();
            string expected = "keysecrethttps://coincheck.com//api/send_money?currency=BTCGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testgetDepositHistoryAsync()
        {
            Task<string> actual = client.getDepositHistoryAsync();
            string expected = "keysecrethttps://coincheck.com//api/deposit_money?currency=btc_jpyGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testgetBankAccountInfoAsync()
        {
            Task<string> actual = client.getBankAccountInfoAsync();
            string expected = "keysecrethttps://coincheck.com//api/bank_accountsGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testapplyBorrowingMoneyAsync()
        {
            double amount = 0.5;
            string ccy = "BTC";
            Task<string> actual = client.applyBorrowingMoneyAsync(amount, ccy);
            string expected = "keysecrethttps://coincheck.com//api/lending/borrowsPOST";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testgetBorrowInfoAsync()
        {
            Task<string> actual = client.getBorrowInfoAsync();
            string expected = "keysecrethttps://coincheck.com//api/lending/borrows/matchesGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testsendBitcoinAsync()
        {
            string address = "va8uoan3a:pvoa";
            double amount = 1.0;
            Task<string> actual = client.sendBitcoinAsync(address, amount);
            string expected = "keysecrethttps://coincheck.com//api/send_moneyPOST";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testregistBankAccountAsync()
        {
            string bankName = "TestBank";
            string bankBranch = "hoge";
            string accountType = "futsu";
            string accountNumber = "1234567";
            string registedName = "Matthew";
            Task<string> actual = client.registBankAccountAsync(bankName, bankBranch, accountType, accountNumber, registedName);
            string expected = "keysecrethttps://coincheck.com//api/bank_accountsPOST";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testdeleteBankAccountAsync()
        {
            string id = "12";
            Task<string> actual = client.deleteBankAccountAsync(id);
            string expected = "keysecrethttps://coincheck.com//api/bank_accounts/12DELETE";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testgetWithdrawHistoryAsync()
        {
            Task<string> actual = client.getWithdrawHistoryAsync();
            string expected = "keysecrethttps://coincheck.com//api/withdrawsGET";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testwithdrawAsync()
        {
            string accountId = "ba";
            double amount = 30000.0;
            Task<string> actual = client.withdrawAsync(accountId, amount);
            string expected = "keysecrethttps://coincheck.com//api/withdrawsPOST";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testcancelWithdrawAsync()
        {
            string withdrawId = "1431";
            Task<string> actual = client.cancelWithdrawAsync(withdrawId);
            string expected = "keysecrethttps://coincheck.com//api/withdraws/1431DELETE";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testrepayAsync()
        {
            string borrowingId = "12345";
            Task<string> actual = client.repay(borrowingId);
            string expected = "keysecrethttps://coincheck.com//api/lending/borrows/12345/repayPOST";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testtransferToLeverageAsync()
        {
            Task<string> actual = client.transferToLeverageAsync(10000);
            string expected = "keysecrethttps://coincheck.com//api/exchange/transfers/to_leveragePOST";
            Assert.AreEqual(expected, actual.Result.ToString());
        }

        [Test]
        public void testtransferFromLeverageAsync()
        {
            Task<string> actual = client.transferFromLeverageAsync(10000);
            string expected = "keysecrethttps://coincheck.com//api/exchange/transfers/from_leveragePOST";
            Assert.AreEqual(expected, actual.Result.ToString());
        }


        [TearDown]
        public void Dispose()
        {
        }
        

    }
}
