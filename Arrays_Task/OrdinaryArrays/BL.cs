using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrdinaryArrays
{
    internal class BL
    {
        //TODO: заполенение массива фейковыми данными рандом+
        //TODO: функция для смены местами SwapRows+
        //TODO: функция для пошуку подмассивов. сумма которого равняется 0 - существует\не существует +
        //TODO: нахождение мин в массиве+
        //TODO: нахождение макс в массиве+
        //TODO: функция, котрая запрашивает размерность массива+
        //TODO: функция, котрая запрашивает заполенение массива+
        //TODO: логика для dec\hex+

        #region ---===###$$$ Array Size & Filling $$$###===---
        /// <summary>
        /// Requests to set the size of the array. If not set - takes default size 10.
        /// </summary>
        /// 

        //TODO: (текст, условие, дефолтное знаечние) 
        public static int SetArraySizeFromUser() 
        {
            Console.Write("Чи хочете ввести розмір масиву самостійно? (так/ні): ");
            //TODO: ReadKey --> можно получить ENUM, где можно полуичть все клавиши 
            //TODO: ConsoleKey.Escape escape = ConsoleKey.Escape;

            string answer = Console.ReadLine()?.Trim().ToLower(); // ? - проверяет null bли нет, .trim() - удаление пробелов, ToLower() - нижний регистр

            int size;

            if (answer == "так")
            { 

                Console.Write("Введіть кількість елементів: ");
                while (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
                    Console.Write("Некоректне значення. Спробуйте ще раз: ");
            }
            else
            {
                size = 10;
                Console.WriteLine("Використано розмір масиву за замовчуванням - 10 елементів");
            }

            return size;
        }

        /// <summary>
        /// Requests to fill the array. If - no, fills it with random values (default range [-200, 200]).
        /// </summary>
        public static void FillArrayFromUserOrRandom(int[] array, int min = -200, int max = 200) 
        {
            Console.Write("Чи хочете ввести елементи масиву самостійно? (так/ні): ");
            string answer = Console.ReadLine()?.Trim().ToLower(); 

            if (answer == "так")
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write($"Введіть елемент [{i}]: ");
                    while (!int.TryParse(Console.ReadLine(), out array[i]))
                        Console.Write("Некоректне значення. Спробуйте ще раз: ");
                }
            }
            else
            {
                Random rand = new Random();
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = rand.Next(min, max + 1);
                }
                Console.WriteLine("Масив заповнено випадковими значеннями.");
            }
        }
        #endregion

        #region ---===###$$$ Matrix Dimesions & Filling $$$###===---

        /// <summary>
        /// Requests to set the matrix's dimensions. If not set - takes default dimesnsions 3*4.
        /// </summary>
        /// <param name="defaultRows"></param>
        /// <param name="defaultCols"></param>
        /// <returns>A tuple with matrix's dimesions.</returns>
        public static (int rows, int cols) SetDimensionsMatrixFromUser(int defaultRows = 3, int defaultCols = 4)
        {
            Console.Write("Чи хочете задати розмірність матриці вручну? (так/ні): ");
            string answer = Console.ReadLine()?.Trim().ToLower(); // ? - проверяет null bли нет, .trim() - удаление пробелов, ToLower() - нижний регистр

            int rows, cols;

            if (answer == "так")
            {
                Console.Write("Введіть кількість рядків: ");

                while (!int.TryParse(Console.ReadLine(), out rows) || rows <= 0)
                    Console.Write("Некоректне значення. Спробуйте ще раз: ");

                Console.Write("Введіть кількість стовпців: ");

                while (!int.TryParse(Console.ReadLine(), out cols) || cols <= 0)
                    Console.Write("Некоректне значення. Спробуйте ще раз: ");
            }
            else
            {
                rows = defaultRows;
                cols = defaultCols;
            }

            Console.WriteLine($"Розмірність: {rows} x {cols}");
            return (rows, cols);
        }


        /// <returns>A tuple with matrix's dimesions.</returns>
        public static (int rows, int cols) GetMatrixDimensions(int[,] matrix)
        {
            return (matrix.GetLength(0), matrix.GetLength(1));
        }

        /// <summary>
        ///  Requests to fill the matrix. If - no, fills the matrix with random values (default range [-10, 10]).
        /// </summary>
        public static void FillMatrixFromUserOrRandom(int[,] matrix, int rows, int cols, int min = -10, int max = 10)
        {
            Console.Write("Чи хочете ввести значення масиву вручну? (так/ні): ");
            string answer = Console.ReadLine()?.Trim().ToLower();  // ? - проверяет null bли нет, .trim() - удаление пробелов, ToLower() - нижний регистр

            if (answer == "так")
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Console.Write($"Введіть елемент [{i},{j}]: ");
                        while (!int.TryParse(Console.ReadLine(), out matrix[i, j]))
                        {
                            Console.Write("Некоректне значення. Спробуйте ще раз: ");
                        }
                    }
                }
            }
            else
            {
                Random rand = new Random();
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = rand.Next(min, max + 1);
                    }
                }
            }
        }
        #endregion

        #region ---===###$$$ ZeroSubarray $$$###===---
        public static bool HasZeroSubarray(int[] array)
        {
            // хранит сумму и индекс, на котром она была достигнута
            Dictionary<int, int> prefixSums =  new Dictionary<int, int>();
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];

                // если сумма равна 0, значит подмассив с 0-го индекса до i
                if (sum == 0)
                {
                    PrintSubarray(array, 0, i);
                    return true;
                }

                // если такая сумма уже была, значит подмассив между индексом (prefixSums[sum]+1) и i равен 0
                if (prefixSums.ContainsKey(sum))
                {
                    int start = prefixSums[sum] + 1;
                    Console.WriteLine("Існує підмасив із сумою 0:");
                    PrintSubarray(array, start, i);
                    return true;
                }

                // запоминаем текущую сумму и индекс
                prefixSums[sum] = i;
            }

            Console.WriteLine("Підмасиву із сумою 0 не знайдено.");
            return false;
        }

        public static void PrintSubarray(int[] array, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Console.Write(array[i]);
                if (i < end) Console.Write(" + ");
            }
            Console.WriteLine(" = 0");
        }

        #endregion

        #region ---===###$$$ Swap min&max rows $$$###===---
        /// <returns>A row index with a max value.</returns>
        public static int GetMaxRowIndex(int[,] matrix, int rows, int cols)
        {
            int max = matrix[0, 0];
            int maxRow = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                        maxRow = i;
                    }
                }
            }
            return maxRow;
        }

        /// <returns>A row index with a min value.</returns>
        public static int GetMinRowIndex(int[,] matrix, int rows, int cols)
        {
            int min = matrix[0, 0];
            int minRow = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] < min)
                    {
                        min = matrix[i, j];
                        minRow = i;
                    }
                }
            }

            return minRow;
        }

        /// <summary>
        /// Swaps a row with a min value and a row with a max value in the matrix.
        /// </summary>
        public static void SwapRows(int[,] matrix, int row1, int row2)
        {
            if (row1 == row2)
            {
                return;
            }

            int cols = matrix.GetLength(1);

            for (int j = 0; j < cols; j++)
            {
                int temp = matrix[row1, j];
                matrix[row1, j] = matrix[row2, j];
                matrix[row2, j] = temp;

            }
        }
        #endregion

        #region ---===###$$$ Bitwise comparison $$$###===---

        public static void CompareBitwise(int[] arr1, int[] arr2)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\na = ");
            Console.WriteLine(ArrayToString(arr1));

            Console.Write("b = ");
            Console.WriteLine(ArrayToString(arr2));
            Console.ResetColor();

            int[] resultArr = BitwiseXnorCompare(arr1, arr2);

            Console.WriteLine("\nРезультати побітового порівняння:");

            for (int i = 0; i < arr1.Length; i++)
            {
                PrintBitwiseComparison(i, arr1[i], arr2[i], resultArr[i]);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nРезультуючий масив:");
            Console.WriteLine(ArrayToString(resultArr));
            Console.ResetColor();
        }

        public static string ArrayToString(int[] arr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < arr.Length; i++)
            {
                sb.Append(arr[i]);
                if (i != arr.Length - 1)
                    sb.Append(", ");
            }
            sb.Append("]");
            return sb.ToString();
        }

        public static int[] BitwiseXnorCompare(int[] arr1, int[] arr2)
        {
            int length = arr1.Length;
            int[] result = new int[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = ~(arr1[i] ^ arr2[i]) & 0xFFFF; // XNOR для 16 біт
            }

            return result;
        }

        public static string ToBinaryString(int number)
        {
            return Convert.ToString(number, 2).PadLeft(16, '0');
        }

        public static void PrintBitwiseComparison(int index, int a, int b, int result)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\narr1[{index}] = {a}");
            Console.WriteLine($"arr2[{index}] = {b}");
            Console.ResetColor();

            Console.WriteLine("\nBinary:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(ToBinaryString(a));
            Console.WriteLine(ToBinaryString(b));
            Console.WriteLine(new string('-', 16));
            Console.WriteLine(ToBinaryString(result));
            Console.ResetColor();

            Console.WriteLine($"Decimal: {result}");
            Console.WriteLine($"Hex: {result:X4}");
        } 
        #endregion
    }
}
