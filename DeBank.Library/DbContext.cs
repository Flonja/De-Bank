using System.Data.Entity;

namespace DeBank.Library
{
    public class DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
