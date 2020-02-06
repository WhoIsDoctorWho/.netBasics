using System;
using System.Collections.Generic;


namespace NumAnalysisDemo
{
    static class Menu
    {
        private delegate double Method(double a, double b, double epsilon);
        public static void Run()
        {
            int option = 0;
            while (option < 1 || option > 5)
            {
                option = Tools.ReadInt("1. Secant method\n2. Newton's method.\n" +
                    "3. Simple iterations method\n4. Graeffe method\n5. Exit");
            }
            Console.Clear();
            switch (option)
            {
                case 1:
                    Solve("Secant Method", "x^2 - cos^2(x)+sin(x)-sqrt(13+x)", SecantMethod.Solve);
                    break;
                case 2:
                    Solve("Newton's Method", "x^2 - cos^2(x)+sin(x)-sqrt(13+x)", NewtonsMethod.Solve);
                    break;
                case 3:
                    Solve("Simple Iteration's Method", "x^2 - cos^2(x)+sin(x)-sqrt(13+x)", SimpleIterationsMethod.Solve);
                    break;
                case 4:
                    GraeffeMethod();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Tools.PrintColorMessage("Something went wrong", ConsoleColor.Red);
                    return;
            }
        }
        private static void Solve(string method, string function, Method methodCall)
        {

            Tools.PrintColorMessage(method, ConsoleColor.Cyan);
            Tools.PrintColorMessage(function, ConsoleColor.DarkCyan);
            int accuracy = GetAccuracy();
            var interval = GetInterval();
            double epsilon = Math.Pow(10, -accuracy);
            double root = methodCall(interval.a, interval.b, epsilon);
            Tools.PrintColorMessage($"x = {Math.Round(root, accuracy)}", ConsoleColor.Green);
        }
        private static void GraeffeMethod()
        {
            double[] coefs = { -74, -789, -840, 907, 730, -348, -50, 19 };
            List<double> l = new List<double>(coefs);
            Graeffe g = new Graeffe(l, 1);
            List<double> roots = g.Solve();
            Console.WriteLine();
            for (int i = 0; i < roots.Count; i++)
            {
                Console.WriteLine($"x{i} = {roots[i]}");
            }
            Tools.PrintColorMessage("\nLet's clarify roots with Secant Method!", ConsoleColor.Cyan);
            System.IO.TextWriter acceptOutput = Console.Out;
            System.IO.TextWriter declineOutput = new System.IO.StreamWriter(System.IO.Stream.Null);
            foreach (double root in roots)
            {
                double b = Math.Round(root, 4) + 0.001;
                double a = Math.Round(root, 4) - 0.001;
                Console.SetOut(declineOutput);
                double x_secant = g.Clarify(a, b, 0.0000001);
                Console.SetOut(acceptOutput);
                Console.WriteLine($"\nx_graeffe: {root}\nx_secant:  {x_secant}");
            }
        }
        private static int GetAccuracy()
        {
            return Tools.ReadInt("Accuracy:");
        }
        private static (double a, double b) GetInterval()
        {
            return (Tools.ReadDouble("a is: "), Tools.ReadDouble("b is: "));
        }

    }
}
