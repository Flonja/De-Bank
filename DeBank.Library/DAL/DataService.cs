using DeBank.Library.Interfaces;
using DeBank.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Linq;

namespace DeBank.Library.DAL
{
    public class DataService :IDataService
    {
        private BankDbContext _dbContext = BankDbContext.GetDbContext();

        private static DataService _dataService;

        private DataService()
        { 
        
        }

        public static DataService GetDataService()
        {
            if (_dataService == null)
            {
                _dataService = new DataService();
            }
            return _dataService;
        }

        public bool AddUser(User user)
        {
            _dbContext.Users.Add(user);
            return true;
        }

        public bool AddBankaccounts(BankAccount bank)
        {
            _dbContext.BankAccounts.Add(bank);
            return true;
        }

        public bool AddTransaction(Logic.Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            return true;
        }

        public bool RemoveUser(User user)
        {
            _dbContext.Users.Remove(user);
            return true;
        }

        public bool RemoveBankaccounts(BankAccount bank)
        {
            _dbContext.BankAccounts.Remove(bank);
            return true;
        }

        public bool RemoveTransaction(Logic.Transaction transaction)
        {
            _dbContext.Transactions.Remove(transaction);
            return true;
        }

        public List<User> ReturnAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public List<BankAccount> ReturnAllBankAccounts()
        {
            return _dbContext.BankAccounts.ToList();
        }
        public List<Logic.Transaction> ReturnAllTransactions()
        {
            return _dbContext.Transactions.ToList();
        }

        public bool UpdateUser(User user)
        {
            var item = _dbContext.Users.Where(a => a.Id == user.Id).FirstOrDefault();
            item = user;
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateBank(BankAccount bank)
        {
            var item = _dbContext.BankAccounts.Where(a => a.Id == bank.Id).FirstOrDefault();
            item = bank;
            _dbContext.SaveChanges();
            return true;
        }
        public bool UpdateTransactions(Logic.Transaction transaction)
        {
            var item = _dbContext.Transactions.Where(a => a.id == transaction.id).FirstOrDefault();
            item = transaction;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
