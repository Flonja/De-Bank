using DeBank.Library.DAL;
using DeBank.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeBank.Library.Interfaces
{
    public interface IDataService
    {

        bool AddUser(User user);
        bool AddBankaccounts(BankAccount bank);
        bool AddTransaction(Logic.Transaction transaction);
        bool RemoveUser(User user);
        bool RemoveBankaccounts(BankAccount bank);
        bool RemoveTransaction(Logic.Transaction transaction);
        List<User> ReturnAllUsers();
        List<BankAccount> ReturnAllBankAccounts();
        List<Logic.Transaction> ReturnAllTransactions();
        bool UpdateUser(User user);
        bool UpdateBank(BankAccount bank);
        bool UpdateTransactions(Logic.Transaction transaction);
    }
}
