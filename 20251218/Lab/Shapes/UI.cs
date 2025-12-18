using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class UI
    {
        public static void Draw(Figure f)
        {
            Symbol[,] view = f.Draw();

            for (int i = 0; i < view.GetLength(0); i++)
            {
                for (int j = 0; j < view.GetLength(1); j++)
                {
                    if (view[i, j] != Symbol.None)
                    {
                        Console.SetCursorPosition(i + f.Y, j + f.X);
                        Console.Write((char)(view[i, j]));
                    }
                }
            }
        }
    }
}
