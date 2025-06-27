using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdinaryArrays
{
    internal class UI
    {
        //TODO: функция для выведения массива прямоуегольного таск 1
        //TODO: выведение до SWAP строчек и после 
        //TODO: выведение для таск 3 - результаты в dec, hex
        //TODO: использовать XML документацию

        #region ---===###$$$ Array $$$###===---
        public static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }
        #endregion

        #region ---===###$$$ Matrix $$$###===---
        public static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}
