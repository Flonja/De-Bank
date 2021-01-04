using System;
using System.Collections.Generic;

namespace DeBank.Library
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<BankAccount> Accounts { get; set; }
    }
}
