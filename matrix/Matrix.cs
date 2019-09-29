using System;

namespace matrix
{
    public class Matrix
    {
        public int[,] matrix;
        public int _rows { get; }
        public int _columns { get; }
        public delegate int Operation(int a, int b);
        public Matrix(int rows, int columns, bool isRandom)
        {
            if (rows <= 0 || columns <= 0)
                throw new IndexOutOfRangeException("Sizes of matrix must be greater then zero!");
            _rows = rows;
            _columns = columns;
            if(isRandom)
                CreateRandomMatrix();
            else
                matrix = new int[_rows, _columns];
        }     
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            return MatrixSum(m1, m2, Add);
        }
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return MatrixSum(m1, m2, Substract);
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if(m1._columns != m2._rows) {
                throw new Exception("Can't mult this matrix");
            }
            Matrix result = new Matrix(m2._rows, m2._columns, false);
            for (int i = 0; i < result._rows; i++) // цикл по рядам
            {
                for (int k = 0; k < result._columns; k++) // цикл по колонкам для умножения
                {
                    for(int j = 0; j < m1._columns; j++) { // цикл по числам ряда
                        result.matrix[i, k] += m1.matrix[i, j] * m2.matrix[j, k];
                    }
                    
                }   
            }
            return result;            
        }
        public static Matrix operator *(Matrix m1, int number)
        {
            Matrix result = new Matrix(m1._rows, m1._columns, false);
            for (int i = 0; i < result._rows; i++)
            {
                for (int j = 0; j < result._columns; j++)
                {
                    result.matrix[i, j] = m1.matrix[i, j] * number;
                }
            }
            return result;
        }
        public static Matrix operator *(int number, Matrix m1)
        {
            return m1*number;
        }
        //@todo better name
        private static Matrix MatrixSum(Matrix m1, Matrix m2, Operation op)
        {
            if (!isSameSizes(m1, m2) || op == null)
            {
                throw new Exception("Wrong sizes");
            }
            Matrix result = new Matrix(m1._rows, m1._columns, false);
            for (int i = 0; i < result._rows; i++)
            {
                for (int j = 0; j < result._columns; j++)
                {
                    result.matrix[i, j] = op.Invoke(m1.matrix[i, j], m2.matrix[i, j]);
                }
            }
            return result;
        }
        public static bool isSameSizes(Matrix m1, Matrix m2)
        {
            return m1?._columns != m2?._columns || m1?._rows != m2?._rows;
        }
        private void CreateRandomMatrix()
        {
            matrix = new int[_rows, _columns];
            Random rand = new Random();
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    matrix[i, j] = rand.Next(3);
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
        private static int Add(int a, int b)
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