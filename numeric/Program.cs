using System;

namespace NumAnalysisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Menu.Run();
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Tools.PrintColorMessage(e.Message, ConsoleColor.Red);
            }

        }
    }
}
