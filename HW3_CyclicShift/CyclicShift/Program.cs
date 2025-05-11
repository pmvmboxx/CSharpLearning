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
            Console.Write("Enter value for A: ");
            int A = int.Parse(Console.ReadLine());

            Console.Write("Enter value for B: ");
            int B = int.Parse(Console.ReadLine());

            Console.Write("Enter value for C: ");
            int C = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nBefore shift: A = {A}, B = {B}, C = {C}");

            CyclicShift(ref A, ref B, ref C);

            Console.WriteLine($"After shift:  A = {A}, B = {B}, C = {C}");
        }

        public static void CyclicShift(ref int A, ref int B, ref int C) 
        {
            // A → B, B → C, C → A

            A = A + B + C;
            B = A - (B + C); // B = A
            C = A - (B + C); // C = B
            A = A - (B + C); // A = C
        }
    }
}
