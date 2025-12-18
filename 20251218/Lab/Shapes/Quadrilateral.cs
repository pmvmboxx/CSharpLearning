using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Quadrilateral
    {
        public int Width 
        { 
            get; 
            set; 
        }
        public int Height 
        { 
            get; 
            set; 
        }

        protected Quadrilateral(int width, int height, ConsoleColor color)
            : base(color)
        {
            Width = width;
            Height = height;
        }
    }
}
