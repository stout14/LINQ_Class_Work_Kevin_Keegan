using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Demo_LINQ_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> listOfNumbers = new List<int>()
            {
                1,2,3,32,27,6,9,13,7,17,20,21
            };

            Console.Write("Evens by foreach: ");
            foreach (int n in GetEvenNumbers(listOfNumbers))
            {
                Console.Write(n + " ");
            }
            Console.WriteLine();

            Console.Write("Evens by LINQ:    ");
            foreach (int n in GetEvenNumbersLINQ(listOfNumbers))
            {
                Console.Write(n + " ");
            }
            Console.WriteLine();

            Console.ReadKey();
        }

        public static List<int> GetEvenNumbers(List<int> listOfNumbers)
        {
            List<int> listOfEvens = new List<int>();

            foreach (int n in listOfNumbers)
            {
                if (n % 2 == 0)
                {
                    listOfEvens.Add(n);
                }
            }

            listOfEvens.Sort();

            return listOfEvens;
        }

        public static List<int> GetEvenNumbersLINQ(List<int> listOfNumbers)
        {
            //List<int> listOfEvens = new List<int>();

            var listOfEvens = (
                from n in listOfNumbers
                where n % 2 == 0
                orderby n
                select n
                )
                .ToList();

            return listOfEvens;
        }
    }
}
