using System;

namespace NumAnalysisDemo
{
    static class Tools
    {
        internal static void PrintColorMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        internal static void PrintColorPart(string colorMessage, string usualMessage, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"{colorMessage} ");
            Console.ResetColor();
            Console.WriteLine(usualMessage);
        }
        internal static int ReadInt(string message)
        {
            PrintColorMessage(message, ConsoleColor.Cyan);
            while (true)
            {
                int result;
                if (int.TryParse(Console.ReadLine(), out result))
                    return result;
            }
        }
        internal static double ReadDouble(string message)
        {
            PrintColorMessage(message, ConsoleColor.Cyan);
            while (true)
            {
                double result;
                if (double.TryParse(Console.ReadLine(), out result))
                    return result;
            }
        }
        internal static void PrintIteration(int iteration)
        {
            PrintColorMessage($"Iteration: {iteration}", ConsoleColor.Cyan);
        }
        internal static bool SimpleStopCriteria(double c, double cPrev, double epsilon)
        {
            return Math.Abs(c - cPrev) < epsilon;
        }
    }
}
