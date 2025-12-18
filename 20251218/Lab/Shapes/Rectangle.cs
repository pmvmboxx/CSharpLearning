using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : Quadrilateral
    {
        public Rectangle(int width, int height, ConsoleColor color)
            : base(width, height, color) { }

        public override Symbol[,] Draw()
        {
            Console.ForegroundColor = Color;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    Console.Write("*");

                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }
}
