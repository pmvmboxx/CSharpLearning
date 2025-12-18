using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Circle : Figure
    {
        public int Radius 
        { 
            get; 
            set; 
        }

        public Circle(int x, int y, int radius, FiguresColor color = FiguresColor.Green) 
                : base(x, y, color)
        {
            Radius = radius;
        }

        public override Symbol[,] Draw()
        {
            Symbol[,] result = new Symbol[Radius * 2 + 1, Radius * 2 + 1];

            //Console.ForegroundColor = Color;

            double thickness = 0.4;
            double rIn = Radius - thickness;
            double rOut = Radius + thickness;

            for (int y = Radius; y >= -Radius; y--)
            {
                for (int x = -Radius; x < Radius; x++)
                {
                    double value = x * x + y * y;
                    if (value >= rIn * rIn && value <= rOut * rOut)
                    {
                        result[Radius - y, x + Radius] = Symbol.Star;

                        //Console.Write("*");
                    }   
                    else
                    {
                        result[Radius - y, x + Radius] = Symbol.None;
                        //Console.Write(" ");
                    }
                        
                }
                // Console.WriteLine();
            }

            //Console.ResetColor();
            return result;
        }


        public override Figure GetCopy()
        {
            return new Circle(_x, _y, Radius, Color);
        }
    }
}
