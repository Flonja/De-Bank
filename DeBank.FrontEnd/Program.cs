using DeBank.Library;
using DeBank.Library.Models;
using System;

namespace DeBank.FrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BankDbContext())
            {
                Console.WriteLine("Users:");
                foreach (User user in db.Users)
                {
                    Console.WriteLine("  " + user.Name + ":");
                    foreach (BankAccount account in user.Accounts)
                    {
                        Console.WriteLine("  - " + account.Name + ":");
                        Console.WriteLine("     Money: " + account.Money);
                    }
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
