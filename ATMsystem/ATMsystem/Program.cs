using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{

    class account
    {
        public long accountnumber { get; set; }
        public string name { get; set; }
        public long phoneno { get; set; }

        public string address { get; set; }

        public long amount { get; set; }

    }
    class Program
    {
        List<account> accountslist = new List<account>();
        public void createaccount()
        {
            account newaccount = new account();
            Console.WriteLine("Creating account ...");
            Console.WriteLine("Enter account number");
            newaccount.accountnumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter customer name");
            newaccount.name = Console.ReadLine();
            Console.WriteLine("Enter your Phone number ");
            newaccount.phoneno = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter your address");
            newaccount.address = Console.ReadLine();
            Console.WriteLine("Enter Amount ");
            newaccount.amount = Convert.ToInt32(Console.ReadLine());
            accountslist.Add(newaccount);
            Console.WriteLine("your account has been created succesfully");

        }
        public void showaccount()
        {
            Console.WriteLine("Enter your account number to view details:");
            long accNum = Convert.ToInt64(Console.ReadLine());

            account f = accountslist.FirstOrDefault(a => a.accountnumber == accNum);

            Console.WriteLine("********* Your account details are **********");
            Console.WriteLine($"Account number:{f.accountnumber}\n" +
               $"Name:{f.name}\n" +
               $"Phoneno:{f.phoneno}\n" +
               $"Address:{f.address}\n" +
               $"Amount:{f.amount}");
            Console.WriteLine("*********************************************");
        }
        public void creditamount()
        {
            Console.WriteLine("Enter your account number to credit amount:");
            long accNum = Convert.ToInt64(Console.ReadLine());
            account f = accountslist.FirstOrDefault(a => a.accountnumber == accNum);
            if (f != null)
            {
                Console.WriteLine("Enter amount to credit:");
                int amt = Convert.ToInt32(Console.ReadLine());
                f.amount += amt;
                Console.WriteLine($"the updated total amount is:{f.amount}");
            }
            else
            {
                Console.WriteLine("Account is not found");
            }
        }
        public void withdrawamount()
        {
            Console.WriteLine("Enter your account number to withdraw amount:");
            long accNum = Convert.ToInt64(Console.ReadLine());
            account f = accountslist.FirstOrDefault(a => a.accountnumber == accNum);
            if (f != null)
            {
                Console.WriteLine("Enter amount to withdraw:");
                int amt = Convert.ToInt32(Console.ReadLine());
                f.amount -= amt;
                Console.WriteLine($"you have succesfully withdrawn amount:{amt}");
                Console.WriteLine($"the updated total amount is:{f.amount}");
            }
            else
            {
                Console.WriteLine("Account is not found");
            }
        }

        public void listaccounts()
        {
            Console.WriteLine($"The total accounts are :{accountslist.Count}");
            Console.WriteLine($"this list of accounts are : ");
            foreach (var a in accountslist){
                Console.WriteLine($"{ a.accountnumber}");
            }
        }
        static void Main(string[] args)
        {
            
            Console.WriteLine("********* WELCOME TO BANK MANAGEMENT SYSTEM ********");
            Console.WriteLine("What do you want to do :");
            int choice = 0;
            Console.WriteLine("1.Create account \n" +
                "2.Show account information \n" +
                "3.credit amount \n" +
                "4.withdraw from account \n" +
                "5.show all accounts \n" +
                "6.clear screen \n" +
                "7.Exit");
            Program p = new Program();
            string stay = "Y";
            while(stay=="Y")
            { 
            choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        p.createaccount();
                        break;
                    case 2:
                        p.showaccount();
                        break;
                    case 3:
                        p.creditamount();
                        break;
                    case 4:
                        p.withdrawamount();
                        break;
                    case 5:
                        p.listaccounts();
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