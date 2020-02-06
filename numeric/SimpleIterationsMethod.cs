using System;

namespace NumAnalysisDemo
{
    static class SimpleIterationsMethod
    {
        private static readonly int MaxIterations = 50;
        private static double _a;
        private static double _b;

        private static double Alpha 
        {
            get => Math.Min(FirstDerivative(_a), FirstDerivative(_b));
        }
        private static double Gamma
        {
            get => Math.Max(FirstDerivative(_a), FirstDerivative(_b));
        }
        private static double Lambda
        {
            get => 2 / (Alpha + Gamma);
        }
        private static double LipschitzСonstant
        {
            get => (Gamma - Alpha) / (Alpha + Gamma);
        }
        public static double Solve(double a, double b, double epsilon)
        {
            _a = a;
            _b = b;

            if (!CheckLocalizationInterval())
                throw new Exception($"Wrong localization interval ({_a};{_b})");
            double x = GetValueFromInterval();
            double lambda = Lambda;
            double q = LipschitzСonstant;
            if (!CheckValue(x, lambda))
            {
                Console.WriteLine(FirstDerivative(x));
                Console.WriteLine();
                throw new Exception("Can't start from that point");
            }
            double prev = x;
            int cnt = 0;
            Tools.PrintColorPart("Lambda:", lambda.ToString(), ConsoleColor.Cyan);
            Tools.PrintColorPart("q:", q.ToString(), ConsoleColor.Cyan);
            double preEps = (1 - q) / q;
            Tools.PrintColorPart("(1-q)/q:", preEps.ToString(), ConsoleColor.Cyan);
            Tools.PrintColorPart("Starting point:", x.ToString(), ConsoleColor.Cyan);
            while (true)
            {
                Tools.PrintIteration(++cnt);
                Console.WriteLine("Xk:   {0}", prev);
                double stop = Math.Abs((1 - q) / q) > 1 ? epsilon : (1 - q) / q * epsilon;
                x = prev - lambda * (Function(prev));
                Console.WriteLine("Xk+1: {0}", x);
                if (Math.Abs(x - prev) < stop || cnt > MaxIterations)
                    return x;
                prev = x;
            }
        }
        public static bool CheckValue(double x, double lambda)
        {
            return Math.Abs(1 - lambda * FirstDerivative(x)) < 1;
        }
        public static bool CheckLocalizationInterval()
        {
            return FirstDerivative(_a) * FirstDerivative(_b) > 0
                && SecondDerivative(_a) * SecondDerivative(_b) > 0
                && Function(_a) * Function(_b) < 0;
        }
        public static double GetValueFromInterval()
        {
            if (_a > _b)
                InvertInterval();
            return RandomDoubleOnInterval();
        }
        public static void InvertInterval()
        {
            (_a, _b) = (_b, _a);
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
            return Math.Round(2 + 6 * x - 30 * x * x * x * x, 9);
        }
        private static double RandomDoubleOnInterval()
        {
            return new Random().NextDouble() * (_b - _a) + _a;
        }
    }
}
