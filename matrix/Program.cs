using System;
using System.IO;
using Newtonsoft.Json;

namespace matrix
{   
    class Student {
        public int age = 19;
        public string name = "Dima";
        public int[,] array = {{1,2}, {2,3}, {3,4}};
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Polynom p1 = new Polynom("2x^3 + 3x^2 + x + 2");
                p1.Get();

                // Matrix m1 = new Matrix(3, 3, true);                
                // string filepath = Directory.GetCurrentDirectory() + "\\test.json";
                // using (StreamWriter sw = new StreamWriter(filepath, false, System.Text.Encoding.Default))
                // {
                //     string parsedMatrix = JsonConvert.SerializeObject(m1, Formatting.Indented);                   
                //     sw.WriteLine(parsedMatrix);
                // }            
                // using (StreamReader sr = new StreamReader(filepath))
                // {
                //     string fromFile = sr.ReadToEnd();                    
                //     Matrix m2 = JsonConvert.DeserializeObject<Matrix>(fromFile);
                //     m2.Print();                             
                // }                                                
            }
            catch (IndexOutOfRangeException ex) {
                Console.WriteLine(ex.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }            
        }
    }
}
