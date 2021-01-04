using DeBank.Library.Models;
using System.Data.Entity;

namespace DeBank.Library
{
    public class BankDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
