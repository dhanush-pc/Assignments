using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
   
    internal class Program
    {
        public interface bank
        {
            void balance(int amount);
        }
        public class realbank : bank
        {
            public void balance(int amount)
            {
                Console.WriteLine("Your balance is: " + amount);
            }
        }
        public class atmproxy : bank
        {
            public realbank rb;
            public void balance(int amount)
            {
                if (rb == null)
                {
                    rb = new realbank();
                }
                Console.WriteLine("balance: " + amount);
            }

        }

            static void Main(string[] args)
             {
                bank b=new atmproxy();
                b.balance(1000);
            }
    }
}
