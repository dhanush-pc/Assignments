using System;
using System.Linq;
using Microsoft.EntityFrameworkCore; // Ensure this namespace is included
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
namespace ATM
{

    
    public class BankContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=BankSystemDB;User=root;Password=root;",
                new MySqlServerVersion(new Version(8, 0, 33))); // Adjust MySQL version as needed
        }
    }

    public class Account
    {
        [Key]
        public long AccountNumber { get; set; }
        public string Name { get; set; }
        public long PhoneNo { get; set; }
        public string Address { get; set; }
        public long Amount { get; set; }
    }

    public class Program
    {
        public void CreateAccount()
        {
            using (var context = new BankContext())
            {
                var newAccount = new Account();
                Console.WriteLine("Creating account ...");
                Console.WriteLine("Enter account number:");
                newAccount.AccountNumber = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("Enter customer name:");
                newAccount.Name = Console.ReadLine();
                Console.WriteLine("Enter your phone number:");
                newAccount.PhoneNo = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("Enter your address:");
                newAccount.Address = Console.ReadLine();
                Console.WriteLine("Enter amount:");
                newAccount.Amount = Convert.ToInt64(Console.ReadLine());

                context.Accounts.Add(newAccount);

                context.SaveChanges();

                Console.WriteLine("Your account has been created successfully.");
            }
        }

        public void ShowAccount()
        {
            using (var context = new BankContext())
            {
                Console.WriteLine("Enter your account number to view details:");
                long accNum = Convert.ToInt64(Console.ReadLine());

                var account = context.Accounts.FirstOrDefault(a => a.AccountNumber == accNum);

                if (account != null)
                {
                    Console.WriteLine("********* Your account details are **********");
                    Console.WriteLine($"Account number: {account.AccountNumber}\n" +
                                      $"Name: {account.Name}\n" +
                                      $"Phone number: {account.PhoneNo}\n" +
                                      $"Address: {account.Address}\n" +
                                      $"Amount: {account.Amount}");
                    Console.WriteLine("*********************************************");
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
        }

        public void CreditAmount()
        {
            using (var context = new BankContext())
            {
                Console.WriteLine("Enter your account number to credit amount:");
                long accNum = Convert.ToInt64(Console.ReadLine());

                var account = context.Accounts.FirstOrDefault(a => a.AccountNumber == accNum);

                if (account != null)
                {
                    Console.WriteLine("Enter amount to credit:");
                    int amt = Convert.ToInt32(Console.ReadLine());
                    account.Amount += amt;

                    context.SaveChanges();

                    Console.WriteLine($"The updated total amount is: {account.Amount}");
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
        }

        public void WithdrawAmount()
        {
            using (var context = new BankContext())
            {
                Console.WriteLine("Enter your account number to withdraw amount:");
                long accNum = Convert.ToInt64(Console.ReadLine());

                var account = context.Accounts.FirstOrDefault(a => a.AccountNumber == accNum);

                if (account != null)
                {
                    Console.WriteLine("Enter amount to withdraw:");
                    int amt = Convert.ToInt32(Console.ReadLine());

                    if (amt > account.Amount)
                    {
                        Console.WriteLine("Insufficient balance.");
                    }
                    else
                    {
                        account.Amount -= amt;
                        context.SaveChanges();

                        Console.WriteLine($"You have successfully withdrawn amount: {amt}");
                        Console.WriteLine($"The updated total amount is: {account.Amount}");
                    }
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
        }

        public void ListAccounts()
        {
            using (var context = new BankContext())
            {
                var accounts = context.Accounts.ToList();

                Console.WriteLine($"The total accounts are: {accounts.Count}");
                Console.WriteLine("This list of accounts are:");
                foreach (var account in accounts)
                {
                    Console.WriteLine($"{account.AccountNumber}");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("********* WELCOME TO BANK MANAGEMENT SYSTEM ********");
            Console.WriteLine("What do you want to do:");
            Console.WriteLine("1. Create account\n" +
                              "2. Show account information\n" +
                              "3. Credit amount\n" +
                              "4. Withdraw from account\n" +
                              "5. Show all accounts\n" +
                              "6. Clear screen\n" +
                              "7. Exit");

            var program = new Program();
            string stay = "Y";

            while (stay == "Y")
            {
                Console.WriteLine("Enter your choice:");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        program.CreateAccount();
                        break;
                    case 2:
                        program.ShowAccount();
                        break;
                    case 3:
                        program.CreditAmount();
                        break;
                    case 4:
                        program.WithdrawAmount();
                        break;
                    case 5:
                        program.ListAccounts();
                        break;
                    case 6:
                        Console.Clear();
                        break;
                    case 7:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

                Console.WriteLine("Choose another option or exit? (Y/N)");
                stay = Console.ReadLine().ToUpper();
            }
        }
    }
}