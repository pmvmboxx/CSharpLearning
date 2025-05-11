using AverageFunctionOverloading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4_AverageOverloading
{
    internal class Program
    {
        const double EPSILON = 0.0001;

        static void Main(string[] args)
        {
            double num1 = 0.0;
            for (int i = 0; i < 10; i++)
            {
                num1 += 0.1;
            }
            Console.WriteLine($"num1: {num1}");

            //if (num1 == 1.0)
            if (Math.Abs(num1 - 1.0) < EPSILON)
            {
                Console.WriteLine("num1 == 1.0");
            }
            else
            {
                Console.WriteLine("num1 != 1.0");
            }

            // TODO: попробовать реализовать второй вариант функции
            //double avg1 = GetAverage(1, 2, 3, 4);
            //double avg2 = GetAverage(1.5, 2.5, 3.5, 4.5);
            //double avg3 = GetAverage();
            //double avg4 = GetAverage(1, 2, 3);
            //double avg5 = GetAverage(1, 2, 3, 4, 5, 6);
            //double avg6 = GetAverage(1, 2, 3, 4, 5.5);

            //Console.WriteLine($"Average 1: {avg1:F2}");
            //Console.WriteLine($"Average 2: {avg2:F2}");
            //Console.WriteLine($"Average 3: {avg3:F2}");
        }

        /// <summary>
        /// Вычисляет среднее значение трех чисел.
        /// </summary>
        /// <param name="a">Первый параметр</param>
        /// <param name="b">Второй параметр</param>
        /// <param name="c">Третий параметр</param>
        /// <returns></returns>
        public static double GetAverage(double a, double b, double c)
        {
            //const int count = 3;

            //double sum = a + b + c;

            //return sum / count;

            //double result = GetAverage(new double[] { a, b, c });
            return 0;
        }

        public static double GetAverage(double a, double b, double c, double d = 0.0, double e = 0.0) 
        {
            // TODO: IEEE754 правильное использование вещественных чисел
            //int count = 3;

            //if (d != 0.0)
            //{
            //    count++;
            //}

            //if (e != 0.0)
            //{
            //    count++;
            //}

            //double sum = a + b + c + d + e;

            //return sum / count;

            return GetAverage( a, b, c, d, e );

        }


        public static double GetAverage(IncorrectInput input, params double[] args)
        {
            if (args == null || args.Length == 0)
            {
                switch (input)
                {
                    case IncorrectInput.ReturnZero:
                        Console.WriteLine("ReturnZero");
                        break;
                    case IncorrectInput.ReturnNaN:
                        Console.WriteLine("ReturnNaN");
                        break;
                    case IncorrectInput.ThrowException:
                        Console.WriteLine("ThrowException");
                        break;
                    case IncorrectInput.IgnoreAndContinue:
                        Console.WriteLine("IgnoreAndContinue");
                        break;
                    default:
                        break;
                }
            }

            double sum = 0;
 
            for (int i = 0; i < args.Length; i++)
            {
                sum += args[i];
            }

            return sum / args.Length;
        }
    }
}