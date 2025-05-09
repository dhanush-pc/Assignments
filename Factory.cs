using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practricee
{
    //public class singleton
    //{
    //    private static singleton instance;
    //    private singleton()
    //    {

    //    }
    //    public static singleton Instance
    //    {
    //        get
    //        {
    //            if(instance == null)
    //            {
    //                instance = new singleton();
    //            }
    //            return instance;
    //        }
    //    }
    //    public void DisplayMessage()
    //    {
    //        Console.WriteLine("Singleton instance called");
    //    }
    //}

    public interface shapes
    {
        void draw();
    }
    public class circle : shapes
    {
        public void draw()
        {
            Console.WriteLine("Drawing circle");
        }
    }
    public class square : shapes
    {
        public void draw()
        {
            Console.WriteLine("Drawing square");
        }
    }
    public class shapefactory
    {
        public shapes getshape(string shape)
        {
            if (shape.Equals("circle"))
            {
                return new circle();
            }
            else if (shape.Equals("square"))
            {
                return new square();
            }
            else
            {
                Console.WriteLine($"Unknown shape: {shape}");
                return null; // Or throw new ArgumentException($"Unknown shape: {shape}");
            }
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {

            //singleton s1 = singleton.Instance;
            //singleton s2 = singleton.Instance;
            //s1.DisplayMessage();
            //s2.DisplayMessage();

            shapefactory factory = new shapefactory();
            shapes s=factory.getshape("circle");
            s.draw();
            shapes s1 = factory.getshape("square");
            s1.draw();
        }
    }
}
