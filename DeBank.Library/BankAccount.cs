using System;
using System.Threading;

namespace DeBank.Library
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Money { get; private set; }

        public event EventHandler<string> LogAppended;

        public bool AddMoney(decimal money, string reason = "")
        {
            Thread.Sleep(5 * 1000);

            Money += money;

            if (reason != "") LogAppended?.Invoke(this, reason);
            return true;
        }

        public bool TransferMoney(BankAccount bankAccount, decimal money, string reason = "")
        {
            if(Money - money < 0)
            {
                return false;
            }

            Thread.Sleep(5 * 1000);

            Money -= money;
            bankAccount.Money += money;

            if (reason != "") LogAppended?.Invoke(this, reason);
            return true;
        }

        public bool SpendMoney(decimal money, string reason = "")
        {
            if (Money - money < 0)
            {
                return false;
            }

            Thread.Sleep(5 * 1000);

            Money -= money;

            if (reason != "") LogAppended?.Invoke(this, reason);
            return true;
        }
    }
}
