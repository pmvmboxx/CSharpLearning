using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public abstract class Figure
    {
        //TODO: enum of colors => method for coverting...
        //TODO: X, Y => 
        protected int _x = 0,
                      _y = 0;

        public int X
        {
            get { return _x; }
            set 
            { 
                if (value < 0) 
                {
                    return;
                }
                _x = value; 
            }
        }

        //TODO: class Settings
        public int Y
        {
            get { return _y; }
            set
            {
                if (value < 0)
                {
                    return;
                }
                _y = value;
            }
        }

        
        public FiguresColor Color 
        { 
            get; 
            set; 
        }

        public Figure(int x, int y, FiguresColor color = FiguresColor.Blue)
        {
            Color = color;
        }

        //TODP: out enum[][] - chars => GetView()
        public abstract Symbol[,] Draw();
        
        //TODO: virtual ctor
        public abstract Figure GetCopy();
        public virtual void Move(int dX, int dY)
        { 
            _x += dX;
            _y += dY;
        }

        //public abstract void Scale(double delta);

    }
}
