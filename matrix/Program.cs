using System;

namespace matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Matrix m1 = new Matrix(3, 3, true);
                Matrix m2 = new Matrix(3, 1, true);
                Matrix m3 = m1*m2;
                m1.Print();
                Console.WriteLine("---------------------------");
                m2.Print();
                Console.WriteLine("---------------------------");
                m3.Print();
            }
            catch (IndexOutOfRangeException ex) {
                Console.WriteLine(ex.Message);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Unknown error");                
            }            
        }
    }
}
