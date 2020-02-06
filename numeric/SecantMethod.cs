using System;


namespace NumAnalysisDemo
{
    public static class SecantMethod
    {
        public delegate double func(double x);
        public static double Solve(double left, double right, double epsilon)
        {
            return Solve(left, right, epsilon, Function);
        }
        public static double Solve(double left, double right, double epsilon, func func)
        {
            int iteration = 0; 
            double c = 0;
            while (true)
            {
                double cPrev = c;
                Tools.PrintIteration(++iteration);
                Console.WriteLine($"a: {left}");
                Console.WriteLine($"b: {right}");
                c = left - (func(left) * (right - left)) / (func(right) - func(left));
                Console.WriteLine($"c: {c}");
                Console.WriteLine("Ck-1: {0}", cPrev);
                if (func(right) * func(c) < 0)
                    left = c;
                else
                    right = c;
                if (Tools.SimpleStopCriteria(c, cPrev, epsilon))
                    return c;
            }
        }

        static double Function(double x)
        {
            return x * x - Math.Pow(Math.Cos(x), 2) + Math.Sin(x) - Math.Sqrt(13 + x);
        }


    }
}
