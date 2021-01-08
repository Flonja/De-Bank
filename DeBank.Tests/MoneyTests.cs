using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DeBank.Library.Models;
using DeBank.Library.Logic;
using System;

namespace DeBank.Tests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void AddMoneyTest()
        {
            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(new BankAccount { Name = "Persoonlijke Kaart" });

            User user1 = new User { Name = "Vanja van Essen", Accounts = accounts };

            BankLogic.AddMoney(user1.Accounts[0], 10, "Initial money").Wait();

            Assert.AreEqual(10, user1.Accounts[0].Money, "Account didn't get the money correctly");
        }

        [TestMethod]
        public void SpendMoneyOnceTest()
        {
            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(new BankAccount { Name = "Persoonlijke Kaart" });

            User user1 = new User { Name = "Vanja van Essen", Accounts = accounts };

            BankLogic.AddMoney(user1.Accounts[0], 500, "Initial money").Wait();

            BankLogic.SpendMoney(user1.Accounts[0], 10).Wait();

            Assert.AreEqual(490, user1.Accounts[0].Money, "Account didn't remove the money correctly");
        }

        [TestMethod]
        public void SpendMoneyTwiceTest()
        {
            List<BankAccount> accounts1 = new List<BankAccount>();
            accounts1.Add(new BankAccount { Name = "Persoonlijke Kaart" });

            User user1 = new User { Name = "Vanja van Essen", Accounts = accounts1 };

            BankLogic.AddMoney(user1.Accounts[0], 250, "Initial money").Wait();

            BankLogic.SpendMoney(user1.Accounts[0], 10).Wait();

            BankLogic.SpendMoney(user1.Accounts[0], 10, true).Wait();
            user1.Accounts[0].PreviousTransactions.FindLast(transaction => transaction.MayExecuteMore == true).Queue().Wait();

            Assert.AreEqual(220, user1.Accounts[0].Money, "Account didn't remove the money correctly");
        }

        [TestMethod]
        [TestCategory("HRTesting")]
        public void TestAutomatedReccuringPayments()
        {
            NUnit.Framework.Assert.DoesNotThrow(() => BankLogic.AutomatedRecurringPayments(100, 4));
        }

        [TestMethod]
        [TestCategory("HRTesting")]
        public void TestAddUser()
        {
            NUnit.Framework.Assert.DoesNotThrow(() => BankLogic.AddUser("test", false));
        }

        [TestMethod]
        [TestCategory("HRTesting")]
        public void TestAddBankAccount()
        {
            User user = new User()
            {
             Id = Guid.NewGuid().ToString(),
             dateofcreation = DateTime.Now,
             dummyaccount = true,
             Name = "Test"
            };
            NUnit.Framework.Assert.DoesNotThrow(() => BankLogic.AddBankAccount(user, "test", 1000000, true));
        }

        [TestMethod]
        [TestCategory("HRTesting")]
        public void TestReturnTransactionsWithinTimeFrame()
        {
            BankAccount account = new BankAccount()
            {
                Id = Guid.NewGuid().ToString(),
                dateofcreation = DateTime.Now,
                dummyaccount = true,
                PreviousTransactions = new List<Transaction>()
                {
                  new Transaction()
                  { 
                   id = Guid.NewGuid().ToString(),
                   Amount = 100,
                   date = DateTime.Now,
                   dummytransaction = true,
                  }
                }
            };
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            NUnit.Framework.Assert.DoesNotThrow(() =>  BankLogic.ReturnTransactionsWithinTimeFrame(account, 100, Library.Enums.PositiveNegativeOrAllTransactions.none));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        }

        [TestMethod]
        [TestCategory("HRTesting")]
        public void TestReturnAllUsersBeneathOraboveGivenValue()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            NUnit.Framework.Assert.DoesNotThrow(() => BankLogic.ReturnAllusersBeneathOrAboveGivenValue(100, true));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}
