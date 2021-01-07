using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DeBank.Library.Models;
using DeBank.Library.Logic;

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
    }
}
