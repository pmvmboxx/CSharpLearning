using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclicShift
{
    internal class Program
    {
        static void Main()
        {
            int choice;

            Console.Write("Enter value for A: ");
            int A = int.Parse(Console.ReadLine());

            Console.Write("Enter value for B: ");
            int B = int.Parse(Console.ReadLine());

            Console.Write("Enter value for C: ");
            int C = int.Parse(Console.ReadLine());

            do {
                Console.Write("Choose 1-5 or 0 to exit: ");
                choice = int.Parse(Console.ReadLine());

                Console.WriteLine($"\nBefore shift: A = {A}, B = {B}, C = {C}");

                switch (choice)
                {
                    case 1:
                        CyclicShift1(ref A, ref B, ref C);
                        break;
                    case 2:
                        CyclicShift2(ref A, ref B, ref C);
                        break;
                    case 3:
                        CyclicShift3(ref A, ref B, ref C);
                        break;
                    case 4:
                        CyclicShift4(ref A, ref B, ref C);
                        break;
                    case 5:
                        CyclicShift5(ref A, ref B, ref C);
                        break;
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine($"After shift : A = {A}, B = {B}, C = {C}");
            } while (choice != 0);
        }

        public static void CyclicShift1(ref int A, ref int B, ref int C) 
        {
            // A → B, B → C, C → A

            A = A + B + C;
            B = A - (B + C); // B = A
            C = A - (B + C); // C = B
            A = A - (B + C); // A = C
        }

        public static void CyclicShift2(ref int A, ref int B, ref int C)
        {

            A = A * B * C; 
            B = A / (B * C); 
            C = A / (B * C); 
            A = A / (B * C); 
        }

        public static void CyclicShift3(ref int A, ref int B, ref int C)
        {

            // XOR A, B
            A = A ^ B;
            B = A ^ B;  // B = (A ^ B) ^ B = A
            A = A ^ B;  // A = (A ^ B) ^ A = B

            // XOR B, C
            B = B ^ C;
            C = B ^ C;  // C = (B ^ C) ^ C = B
            B = B ^ C;  // B = (B ^ C) ^ B = C
        }

        public static void CyclicShift4(ref int A, ref int B, ref int C)
        {

            int[] arr = { A, B, C };

            // A → B, B → C, C → A
            arr = new int[] { arr[2], arr[0], arr[1] };

            A = arr[0];
            B = arr[1];
            C = arr[2];
        }

        public static void CyclicShift5(ref int A, ref int B, ref int C)
        {
            (A, B, C) = (C, A, B); // C → A, A → B, B → C
        }
    }
}
