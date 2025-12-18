using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Square : Quadrilateral
    {
        public Square(int side, ConsoleColor color)
            : base(side, side, color) { }

        public override Symbol[,] Draw()
        {
            Console.ForegroundColor = Color;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }
}
