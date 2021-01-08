using DeBank.Library.Models;
using System;
using System.Threading.Tasks;

namespace DeBank.Library.Logic
{
    public class Transaction
    {
        [key]
        public string id { get; set; }
        public BankAccount Account { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public bool dummytransaction { get; set; }
        public bool MayExecuteMore { get; set; }
        public bool AlreadyExecuted { get; private set; }
        public DateTime date { get; set; }

        public event EventHandler<string> TransactionLog;

        //public BankAccount RecievingAccount { get; set; }


        public async Task<bool> Queue()
        {
            if(AlreadyExecuted && !MayExecuteMore)
            {
                TransactionLog?.Invoke(this, "U kan deze actie niet nog een keer uitvoeren");
                return false;
            }

            foreach (Transaction transaction in Account.TransactionQueue)
            {
                if (Account.Money + transaction.Amount < 0)
                {
                    TransactionLog?.Invoke(this, "U heeft geen geld meer deze actie uit te voeren");
                    return false;
                }
            }

            TransactionLog?.Invoke(this, "Uw actie staat nu in de wachtrij");

            await Task.Delay(5 * 1000);

            Account.Money += Amount;
            AlreadyExecuted = true;

            TransactionLog?.Invoke(this, "Transactie voltooid" + (Reason != "" ? ": " + Reason : ""));
            return true;
        }
    }
}
