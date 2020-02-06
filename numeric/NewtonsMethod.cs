using System;

namespace NumAnalysisDemo
{
    static class NewtonsMethod
    {
        public static bool FourierCondition(double x)
        {
            return Function(x) * SecondDerivative(x) > 0;
        }
        public static bool CheckLocalizationInterval(double a, double b)
        {
            return FirstDerivative(a) * FirstDerivative(b) > 0 
                && SecondDerivative(a) * SecondDerivative(b) > 0 
                && Function(a) * Function(b) < 0; 
        }
        public static double Solve(double a, double b, double epsilon)
        {
            if (!CheckLocalizationInterval(a, b))
            {
                throw new Exception($"Wrong localozation interval ({a};{b})");
            }
            double x, prev = FourierCondition(a) ? a : b; // Find 'dynamic' point            
            int iteration = 0;
            while (true)
            {
                x = prev - (Function(prev) / FirstDerivative(prev));
                Tools.PrintIteration(++iteration);
                Console.WriteLine($"Xk:   {prev}");
                Console.WriteLine($"Xk+1: {x}");
                if (Tools.SimpleStopCriteria(x, prev, epsilon))
                    return x;
                prev = x;
            }
        }
        static double Function(double x)
        {
            return x * x + x * x * x + 35 - x * x * x * x * x * x;
        }
        static double FirstDerivative(double x)
        {
            return 2 * x + 3 * x * x - 6 * x * x * x * x * x;
        }
        static double SecondDerivative(double x)
        {
            return 2 + 6 * x - 30 * x * x * x * x;
        }
    }

}

