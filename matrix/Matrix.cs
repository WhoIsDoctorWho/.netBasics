using System;

namespace matrix
{
    public class Matrix
    {
        public int[,] matrix;
        public int _rows { get; }
        public int _columns { get; }
        public delegate int Operation(int a, int b);
        public Matrix(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
                throw new IndexOutOfRangeException("Sizes of matrix must be greater then zero!");
            _rows = rows;
            _columns = columns;
            CreateRandomMatrix();
        }
        public static bool isSameSizes(Matrix m1, Matrix m2)
        {
            return m1?._columns != m2?._columns || m1?._rows != m2?._rows;
        }
        
        //@todo better name
        private static Matrix MatrixSum(Matrix m1, Matrix m2, Operation op)
        {
            if (!isSameSizes(m1, m2) || op == null)
            {
                throw new Exception("Wrong sizes");
            }
            Matrix result = new Matrix(m1._rows, m1._columns);
            for (int i = 0; i < result._rows; i++)
            {
                for (int j = 0; j < result._columns; j++)
                {
                    result.matrix[i, j] = op.Invoke(m1.matrix[i, j], m2.matrix[i, j]);//m1.matrix[i, j] + m2.matrix[i, j];
                }
            }
            return result;
        }
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            return MatrixSum(m1, m2, Sum);
        }
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return MatrixSum(m1, m2, Substract);
        }
        public static Matrix operator *(Matrix m1, int number)
        {
            throw new NotImplementedException();
        }
        public static Matrix operator *(int number, Matrix m1)
        {
            throw new NotImplementedException();
        }
        private void CreateRandomMatrix()
        {
            matrix = new int[_rows, _columns];
            Random rand = new Random();
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    matrix[i, j] = rand.Next(100);
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    Console.Write($"{matrix[i, j]} \t");
                }
                Console.WriteLine();
            }
        }
        private static int Sum(int a, int b)
        {
            return a + b;
        }
        private static int Substract(int a, int b)
        {
            return a - b;
        }
        private static int Mult(int a, int b)
        {
            return a * b;
        }

    }
}