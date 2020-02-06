using System;
using System.Collections.Generic;

namespace NumAnalysisDemo
{
    class Graeffe
    {
        private List<double> coefs;
        private List<double> a;
        private List<double> b; 
        private List<double> roots;
        private double epsilon;
        public int p;

        public Graeffe(List<double> coefs, int accuracy)
        {
            this.coefs = coefs;
            roots = new List<double>();
            b = new List<double>();
            b = coefs;
            epsilon = Math.Pow(10, -accuracy);
        }

        public List<double> Solve()
        {
            p = 0;
            do
            {
                Quadrate();
                CalcRoots();
            }
            while (StopCriteria());
            return roots;
        }

        private void CalcRoots()
        {
            roots.Clear();
            for (int i = 0; i < b.Count - 1; i++)
            {
                roots.Add(
                    Math.Pow(b[i + 1] / b[i],
                    Math.Pow(2, -p)
                    )
                );
                if (Math.Abs(Function(roots[i])) > Math.Abs(Function(-roots[i])))
                {
                    roots[i] *= -1;
                }
            }
        }

        private void Quadrate()
        {
            this.a = b;
            List<double> tmp = new List<double>();

            for (int i = 0; i < b.Count; i++)
            {
                double sum = 0;
                for (int j = 0; j < b.Count - i; j++)
                {
                    if (j <= i && i + j < b.Count)
                        sum += Math.Pow(-1, j + 1) * b[i - j] * b[i + j];
                }
                double a = -((b[i] * b[i]) + 2 * sum);
                tmp.Add(a);
            }
            b = tmp;
            Tools.PrintIteration(++p);
            foreach (double num in b)
            {
                Console.Write("{0} ", num);
            }
            Console.WriteLine();
        }

        private bool StopCriteria()
        {
            double n = 0;
            for (int i = 0; i < b.Count; i++)
            {
                n += Math.Pow(1 - b[i] / Math.Pow(a[i], 2), 2);
            }
            n = Math.Pow(n, 0.5);
            return n >= epsilon;
        }

        public double Function(double x)
        {
            double result = 0;
            for (int i = 0; i < coefs.Count; i++)
            {
                result += coefs[i] * Math.Pow(x, coefs.Count - i - 1);
            }
            return result;
        }

        public double getDiffValue(double x, int derivative)
        {
            double result = 0;
            for (int i = 0; i < coefs.Count - derivative; i++)
            {
                int order = coefs.Count - i - 1;
                double coef = coefs[i];
                for (int n = 0; n < derivative; n++)
                {
                    coef *= order - n;
                }
                result += coef * Math.Pow(x, order - derivative);
            }
            return result;
        }
        public double Clarify(double a, double b, double epsilon)
        {
            return SecantMethod.Solve(a, b, epsilon, Function);
        }

    }
}

