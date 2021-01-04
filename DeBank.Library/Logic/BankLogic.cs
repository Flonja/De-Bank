using DeBank.Library.Models;
using System.Threading.Tasks;

namespace DeBank.Library.Logic
{
    public class BankLogic
    {
        public static async Task<bool> AddMoney(BankAccount account, decimal money, string reason = "")
        {
            if(money < 0)
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
