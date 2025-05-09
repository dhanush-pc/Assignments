using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    public interface operation
    {
        void execute();
    }
    public class switchon : operation
    {
        public void execute()
        {
            Console.WriteLine("switch on");
        }
    }
    public class switchoff : operation
    {
        public void execute()
        {
            Console.WriteLine("switch off");
        }
    }

    public class remotecontrol
    {
       public  void button(operation op)
        {
            op.execute();
        }
    }

    internal class Program
    {
        
        static void Main(string[] args)
             {
            remotecontrol rm= new remotecontrol();
            rm.button(new switchon());
            rm.button(new switchoff());
        }
    }
}
