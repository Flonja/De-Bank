using DeBank.Library.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DeBank.Library.Logic
{
    public class BankLogic
    {
        public static async Task<bool> AddMoney(BankAccount account, decimal money, string reason = "")
        {
            Interfaces.IDataService _dataService = DAL.DataService.GetDataService();
            if (money < 0)
            {
                return false;
            }

            Transaction transaction = new Transaction { Account = account, Amount = money, Reason = reason };
            transaction.TransactionLog += account.Log;
            account.TransactionQueue.Add(transaction);
            bool result = await transaction.Queue();
            account.TransactionQueue.Remove(transaction);

            account.PreviousTransactions.Add(transaction);



            return result;
        }

        //<summary>
        //added code
        //<summary>
        public static async void AddUser(string Name)
        {
            var lockingobject = new object();
            Monitor.Enter(lockingobject);
            try
            {
                await Task.Run(() =>
                {
                    Interfaces.IDataService _dataService = DAL.DataService.GetDataService();
                    User user = new User()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = Name,
                        Accounts = new System.Collections.Generic.List<BankAccount>()
                        { }
                    };
                    _dataService.AddUser(user);
                });
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
            }
            finally
            {
                Monitor.Exit(lockingobject);
            }
        }

        public static async void AddBankAccount(User owner,string name, decimal money)
        {
            var lockingobject = new object();
            Monitor.Enter(lockingobject);
            try
            {
                await Task.Run(() =>
                {
                    Interfaces.IDataService _dataService = DAL.DataService.GetDataService();
                    BankAccount user = new BankAccount()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Money = money,
                        Name = name, 
                        Owner = owner
                    };
                    _dataService.AddBankaccounts(user);
                });
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
            }
            finally
            {
                Monitor.Exit(lockingobject);
            }
        }

        //<summary>
        //added code
        //<summary>
        public static async Task<bool> AddMoney(BankAccount account, decimal money, BankAccount opossiteAccount, string reason = "")
        {
            Interfaces.IDataService _dataService = DAL.DataService.GetDataService();
            if (money < 0)
            {
                return false;
            }

            Transaction transaction = new Transaction { Account = account, Amount = money, Reason = reason };
            transaction.TransactionLog += account.Log;
            account.TransactionQueue.Add(transaction);
            bool result = await transaction.Queue();
            account.TransactionQueue.Remove(transaction);

            account.PreviousTransactions.Add(transaction);

            //<summary>
            //added code
            //<summary>

            Transaction oppossitetransaction = new Transaction { Account = opossiteAccount, Amount = -money, Reason = reason };
            transaction.TransactionLog += opossiteAccount.Log;
            opossiteAccount.TransactionQueue.Add(oppossitetransaction);
            result = await oppossitetransaction.Queue();
            opossiteAccount.TransactionQueue.Remove(oppossitetransaction);
            opossiteAccount.PreviousTransactions.Add(oppossitetransaction);

            _dataService.AddTransaction(transaction);
            _dataService.UpdateBank(opossiteAccount);
            _dataService.UpdateBank(account);
            var UserSuperAccount = _dataService.ReturnAllUsers().Where(a => a.Accounts.Where(a => a.Id == account.Id).Any()).FirstOrDefault();
            var OppositeUserSuperAccount = _dataService.ReturnAllUsers().Where(a => a.Accounts.Where(a => a.Id == opossiteAccount.Id).Any()).FirstOrDefault();
            _dataService.UpdateUser(UserSuperAccount);
            _dataService.UpdateUser(OppositeUserSuperAccount);

            //<summary>
            //added code
            //<summary>

            return result;
        }

        public static async Task<bool> SpendMoney(BankAccount account, decimal money, bool subscription = false, string reason = "")
        {
            if (money < 0)
            {
                return false;
            }

            Transaction transaction = new Transaction { Account = account, Amount = -money, Reason = reason, MayExecuteMore = subscription };
            transaction.TransactionLog += account.Log;

            account.TransactionQueue.Add(transaction);
            bool result = await transaction.Queue();
            account.TransactionQueue.Remove(transaction);

            account.PreviousTransactions.Add(transaction);

            return result;
        }

        public static async Task<bool> TransferMoney(BankAccount account1, BankAccount account2, decimal money, string reason = "")
        {
            if (money < 0)
            {
                return false;
            }

            bool result = await SpendMoney(account1, money, false, "Geld overmaken naar " + account2.Owner.Name + (reason != "" ? ": " + reason : ""));
            if (result)
            {
                await AddMoney(account2, money, "Geld overmaken van " + account1.Owner.Name + (reason != "" ? ": " + reason : ""));
            }

            return result;
        }


    }
}
