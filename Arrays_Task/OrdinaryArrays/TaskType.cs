using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdinaryArrays
{
    // [Flags] тут не нужен
    public enum TaskType 
    {
        Exit,                   // 0
        SubarrayWithZeroSum,    // 1
        SwapMinMaxRows,         // 2
        BitwiseComparison       // 3
    }
}
