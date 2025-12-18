using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    //TODO: composition
    internal class Container /*: Figure*/
    {
        private Figure _parent;
        List<Figure> _children;
    }
}
