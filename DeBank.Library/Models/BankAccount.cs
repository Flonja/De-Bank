using DeBank.Library.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeBank.Library.Models
{
    public class BankAccount
    {
        [key]
        public string Id { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public decimal Money { get; internal set; }
        public List<Transaction> PreviousTransactions = new List<Transaction>();

        [NotMapped]
        public List<Transaction> TransactionQueue = new List<Transaction>();
        public event EventHandler<string> TransactionLog;

        public void Log(object sender, string line)
        {
            TransactionLog?.Invoke(this, line);
        }
    }
}
