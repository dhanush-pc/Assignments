using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Atmsystem1
{
    class Account
    {
        public string username { get; set; }
        public string password { get; set; }
        public long accountno { get; set; }
        public int amount { get; set; }

        public Account(string Username,string Password,long accountno,int Amount)
        {
            username = Username;
            password = Password;
            long account = accountno;   
            amount = Amount;
        }

    }
    class Program
    {
        Dictionary<long, Account> accounts = new Dictionary<long, Account>();
        public void Login()
        {
            Console.WriteLine("******welcome to the login page******");
            Console.WriteLine("enter username");
            string username = Console.ReadLine();
            Console.WriteLine("enter password");
            string password = Console.ReadLine();
            Console.WriteLine("enter your account number");
            long accountno = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("enter the amount");
            int amount = Convert.ToInt32(Console.ReadLine());

            accounts[accountno] = new Account(username, password, accountno, amount);
            Console.WriteLine("logged in succesfully.....");

        }
        public void checkbalance()
        {
            Console.WriteLine("enter your account to check balance");
            long accno = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("enter your password for validation");
            string pass = Console.ReadLine();
            if (accounts.ContainsKey(accno))
            {
                if (accounts[accno].password != pass)
                {
                    Console.WriteLine("not allowed!");
                }
                else
                {
                    Console.WriteLine("validated");
                    Console.WriteLine($"your balance is {accounts[accno].amount}");
                }
            }
        }
        public void deposit()
        {
            Console.WriteLine("enter your account no for deposit");
            long accno = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("enter the amount for deposit");
            int amt = Convert.ToInt32(Console.ReadLine());
            accounts[accno].amount += amt;
            Console.WriteLine($"your total balance is {accounts[accno].amount}");
        }
        public void withdraw()
        {
            Console.WriteLine("enter your account no for withdraw");
            long accno = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("enter the amount for withdrawal");
            int amt = Convert.ToInt32(Console.ReadLine());
            if (accounts[accno].amount < amt)
            {
                Console.WriteLine("low balance");
            }
            else
            {

                accounts[accno].amount -= amt;

                Console.WriteLine($"your total balance is {accounts[accno].amount}");
            }
        }
        public void logout()
        {
            Console.WriteLine("logout successfully");
            Environment.Exit(0);
        }


        static void Main(string[] args)
        {

            Console.WriteLine("******* MEYBANK ATM SECURE MENU *******");
            Console.WriteLine("What do you want to do :");
            int choice = 0;

            Console.WriteLine("1.Login \n 2.CheckBalance \n 3.Deposit \n  4.Withdraw \n 5.Logout \n 6.Clear \n 7.Exit");

            Program p = new Program();
            string stay = "Y";
            while (stay == "Y")
            {
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        p.Login();
                        break;
                    case 2:
                        p.checkbalance();
                        break;
                    case 3:
                        p.deposit();
                        break;
                    case 4:
                        p.withdraw();
                        break;
                    case 5:
                        p.logout();
                        break;
                    case 6:
                        Console.WriteLine("clearing your screen.....");
                        Console.Clear();
                        break;
                    case 7:
                        Console.WriteLine("exiting......");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

                Console.WriteLine("choose some other option or exit? y/n");
                stay = Console.ReadLine().ToUpper();
                Console.WriteLine("ok choose:");
            }
        }
    }
}
