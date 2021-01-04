using DeBank.Library;
using DeBank.Library.Logic;
using DeBank.Library.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeBank.FrontEnd
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var db = new BankDbContext())
            {
                Console.WriteLine("Users:");
                foreach (User user in db.Users)
                {
                    Console.WriteLine("  " + user.Name + ":");
                    foreach (BankAccount account in user.Accounts)
                    {
                        account.TransactionLog += Account_TransactionLog;

                        Console.WriteLine("  - " + account.Name + ":");
                        Console.WriteLine("     Money: " + account.Money);
                    }
                }

                var user1 = db.Users.Where(u => u.Name == "Vanja van Essen").FirstOrDefault();
                var user2 = db.Users.Where(u => u.Name == "Luna Herder").FirstOrDefault();

                var account1 = user1.Accounts.FirstOrDefault();
                var account2 = user2.Accounts.FirstOrDefault();

                await BankLogic.TransferMoney(account1, account2, 10, "Testen");

                Console.WriteLine("Users (AFTER):");
                foreach (User user in db.Users)
                {
                    Console.WriteLine("  " + user.Name + ":");
                    foreach (BankAccount account in user.Accounts)
                    {
                        Console.WriteLine("  - " + account.Name + ":");
                        Console.WriteLine("     Money: " + account.Money);
                    }
                }
            }
        }

        private static void Account_TransactionLog(object sender, string e)
        {
            Console.WriteLine("LOG (" + sender + "): " + e);
        }
    }
}
