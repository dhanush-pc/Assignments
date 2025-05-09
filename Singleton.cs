using System;

using System.Collections.Generic;

using System.Data.SqlClient;

using System.Linq;

using System.Text;

using System.Threading.Tasks;
 
namespace practricee

{

    public class singleton

    {

        private static singleton instance;

        private singleton()

        {
 
        }

        public static singleton Instance

        {

            get

            {

                if(instance == null)

                {

                    instance = new singleton();

                }

                return instance;

            }

        }

        public void DisplayMessage()

        {

            Console.WriteLine("Singleton instance called");

        }

    }

    internal class Program

    {

        static void Main(string[] args)

        {
 
            singleton s1 = singleton.Instance;

            singleton s2 = singleton.Instance;

            s1.DisplayMessage();

            s2.DisplayMessage();
 
 
        }

    }

}

 
