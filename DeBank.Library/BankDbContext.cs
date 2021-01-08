using DeBank.Library.Models;
using System.Data.Entity;
using System.Transactions;

namespace DeBank.Library
{
    public class BankDbContext : DbContext
    {
        private static BankDbContext _dbContext;

        public static BankDbContext GetDbContext()
        {
            if (_dbContext != null)
            {
                _dbContext = new BankDbContext();
            }
            return _dbContext;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Logic.Transaction> Transactions {get;set;}
    }
}
