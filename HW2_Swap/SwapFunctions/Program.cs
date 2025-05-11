using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapFunctions
{
    internal class Program
    {
        private static int aResult;
        private static int bResult;

        static void Main(string[] args)
        {
            bool running = true;

            do
            {
                int arg1 = 3;
                int arg2 = 7;

                int arg1New;
                int arg2New;

                Console.WriteLine($"Before swap:\t a = {arg1} b = {arg2}");
                Console.Write("Choose a method [1-12] or 0 to exit: ");
                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "0":
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;
                    case "1":
                        arg1New = Swap1(arg1, arg2, out arg2New);
                        Console.WriteLine($"After swap:\t a = {arg1New} b = {arg2New}\n");
                        break;
                    case "2":
                        (arg1New, arg2New) = Swap2(arg1, arg2);
                        string s = String.Format("After swap:\t a = {0} b = {1}\n", arg1New, arg2New);
                        Console.WriteLine(s);
                        break;
                    case "3":
                        Swap3(ref arg1, ref arg2);
                        Console.WriteLine($"After swap:\t a = {arg1} b = {arg2}\n");
                        break;
                    case "4":
                        Swap4(ref arg1, ref arg2);
                        Console.WriteLine($"After swap:\t a = {arg1} b = {arg2}\n");
                        break;
                    case "5":
                        Swap5(ref arg1, arg2, out arg2New);
                        Console.WriteLine($"After swap:\t a = {arg1} b = {arg2New}\n");
                        break;
                    case "6":
                        Swap6(ref arg1, ref arg2);
                        Console.WriteLine($"After swap:\t a = {arg1} b = {arg2}\n");
                        break;
                    case "7":
                        Swap7(arg1, arg2, out arg1New, out arg2New);
                        Console.WriteLine($"After swap:\t a = {arg1New} b = {arg2New}\n");
                        break;
                    case "8":
                        //Swap8();
                        break;
                    case "9":
                        Swap9(out arg1New, out arg2New, arg1, arg2);
                        Console.WriteLine($"After swap:\t a = {arg1New} b = {arg2New}\n");
                        break;
                    case "10":
                        arg2New = 0;
                        arg1New = Swap10(arg1, arg2, ref arg2New);
                        Console.WriteLine($"After swap:\t a = {arg1New} b = {arg2New}\n");
                        break;
                    case "11":
                        Swap11(arg1, arg2);
                        Console.WriteLine($"After swap:\t a = {aResult} b = {bResult}\n");
                        break;
                    case "12":
                        int[] nums = { arg1, arg2 };
                        Swap12(nums);
                        Console.WriteLine($"After swap:\t a = {nums[0]} b = {nums[1]}\n");
                        break;
                    default:
                        Console.WriteLine("Invalid input. Try again.");
                        break;
                }
            } while (running);
        }

        // by value
        public static int Swap1(int a, int b, out int res)
        {
            res = a;
            return b;
        }

        // tuple
        public static (int, int) Swap2(int a, int b)
        {
            return (b, a);
        }

        // by reference
        public static void Swap3(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static void Swap4(ref int a, ref int b)
        {
            a += b;
            b = a - b;
            a = a - b;
        }

        public static void Swap5(ref int a, int b, out int bNew)
        {
            int temp = a;
            a = b;
            bNew = temp;
        }

        // XOR operator
        public static void Swap6(ref int a, ref int b)
        {
            a ^= b;
            b ^= a;
            a ^= b;
        }

        public static void Swap7(int a, int b, out int aNew, out int bNew)
        {
            aNew = b;
            bNew = a;
        }

        public static void Swap8(int a, int b, out int aNew, out int bNew)
        {
            int temp = a;
            aNew = b;
            bNew = temp;
        }

        public static void Swap9(out int a, out int b, int aNew, int bNew)
        {
            int temp = aNew;
            a = bNew;
            b = temp;
        }

        public static int Swap10(int a, int b, ref int res)
        {
            res = a;
            return b;
        }

        // global variables
        public static void Swap11(int a, int b)
        {
            aResult = b;
            bResult = a;
        }

        // array
        public static void Swap12(int[] arr)
        {
            if (arr.Length >= 2)
            {
                int temp = arr[0];
                arr[0] = arr[1];
                arr[1] = temp;
            }
        }
        // TODO: use in 
    }
}
