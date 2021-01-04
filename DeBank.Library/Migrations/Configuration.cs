namespace DeBank.Library.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DeBank.Library.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<DeBank.Library.BankDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DeBank.Library.BankDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(new BankAccount { Name = "Persoonlijke Kaart", Money = 100 });

            context.Users.AddOrUpdate(new User { Name = "Vanja van Essen", Accounts = accounts });
            context.SaveChanges();
        }
    }
}
