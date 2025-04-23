using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_UpdatedMultiplicationTable
{
    internal class Program
    {
        const int DEFAULT_COLUMN_WIDTH = 15;
        const int DEFAULT_FROM = 1;
        const int DEFAULT_TO = 10;
        const int DEFAULT_COLUMN_START = 1;
        const int DEFAULT_COLUMN_END = 10;
        const int DEFAULT_COLUMN_QUANTITY = 4;
        const int DEFAULT_ROW_HEIGHT = 12;
        const int COLUMN_NUMBER_RANGE_START = 1;
        const int COLUMN_NUMBER_RANGE_END = 9;

        static void Main(string[] args)
        {
            while (true)
            {
                ShowMenu();

                Console.Write("Choose the task (or 0 to exit): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TableForLoop();
                        break;
                    case "2":
                        TableWhileLoop();
                        break;
                    case "3":
                        TableDoWhileLoop();
                        break;
                    case "4":
                        MultiplicationTableTask();
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Incorrct input. Try again.\n");
                        break;
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n=== TASK MENU ===");
            Console.WriteLine("1. Table with For Loop");
            Console.WriteLine("2. Table with While Loop");
            Console.WriteLine("3. Table with DoWhile Loop");
            Console.WriteLine("4. Multiplication Table Task");
            Console.WriteLine("0. exit");
            Console.WriteLine();
        }

        private static bool TryGetInput(int minValue, int maxValue, string message, out int res)
        {
            string msg = string.Format("Please, enter {0} between {1} and {2}: ", message, minValue, maxValue);
            Console.Write(msg);
            bool fOK = false;

            do
            {
                string str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    res = -1;
                    return false;
                }

                fOK = int.TryParse(str, out res)
                        && res >= minValue
                        && res <= maxValue;

            } while (!fOK);

            return true;
        }

        private static void DrawTable(int positionX, int positionY, int tableNumber)
        {
            var start_x = positionX;
            var start_y = positionY;

            for (int i = DEFAULT_COLUMN_START; i <= DEFAULT_COLUMN_END; i++)
            {
                Console.SetCursorPosition(start_x, start_y++);
                Console.Write($"{tableNumber} * {i} = {tableNumber * i}");
            }
        }

        private static void MultiplicationTableTask()
        {
            int from_point = TryGetInput(DEFAULT_FROM, DEFAULT_TO, "the start point A", out int res) ? res : DEFAULT_FROM;
            int to_point = TryGetInput(from_point, DEFAULT_TO, "the end point B", out res) ? res : DEFAULT_TO;
            int columnQuantity = TryGetInput(COLUMN_NUMBER_RANGE_START, COLUMN_NUMBER_RANGE_END, "the column number", out res) ? res : DEFAULT_COLUMN_QUANTITY;
            int tablesQuantity = to_point - from_point + 1;
            int rowQuantity = tablesQuantity / columnQuantity + tablesQuantity % columnQuantity == 0 ? 0 : 1;
            int currentNumber = from_point;


            //int rowQuantity = (int)Math.Ceiling((double)(to_point - from_point + 1) / columnQuantity);
            //int rowQuantity = (int)Math.Round((double)(to_point - from_point + 1) / columnQuantity);

            /*Math.Ceiling() - always rounds up to the next whole number.
             * Принимает - decimal/double.
             * Возвращает - не целочисленное значение, а значение типа decimal/double.*/

            /* Math.Round() - rounds to the nearest whole number.
             * Если дробная часть a находится на равном расстоянии от двух целых чисел (четного и нечетного), возвращается четное число.
             * Принимает - decimal/double.
             * Возвращает - целое число, ближайшее к значению параметра a.
             * Возвращает не целочисленное значение, а значение типа decimal/double.*/

            Console.Clear();
            DrawAllTables(DEFAULT_COLUMN_WIDTH, DEFAULT_ROW_HEIGHT, to_point, columnQuantity, rowQuantity, currentNumber);
        }

        private static void DrawAllTables(int DEFAULT_COLUMN_WIDTH, int DEFAULT_ROW_HEIGHT, int to, int columnQuantity, int rowQuantity, int currentNumber)
        {
            for (int j = 0; j < rowQuantity; j++)
            {
                for (int i = 0; i < columnQuantity; i++)
                {
                    int positionX = DEFAULT_COLUMN_WIDTH * i;
                    int positionY = DEFAULT_ROW_HEIGHT * j;
                    DrawTable(positionX, positionY, currentNumber);

                    if (currentNumber == to)
                    {
                        return;
                    }

                    currentNumber++;
                }
            }
        }

        private static void TableForLoop()
        {
            const int columnLength = 15;
            Console.WriteLine("\t\t==== Multiplication Table ==== \t\t");
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    int res = i * j;
                    var str = $"{j} * {i} = {res}";
                    var spaceNumbers = columnLength - str.Length;
                    str += new string(' ', spaceNumbers);
                    Console.Write(str);
                    if (j == 10)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }

        private static void TableWhileLoop()
        {
            const int columnLength = 15;
            Console.WriteLine("\t\t==== Multiplication Table ==== \t\t");
            int i = 1;
            while (i <= 10)
            {
                int j = 1;
                while (j <= 10)
                {
                    int res = i * j;
                    var str = $"{j} * {i} = {res}";
                    var spaceNumbers = columnLength - str.Length;
                    str += new string(' ', spaceNumbers);
                    Console.Write(str);
                    j++;
                }
                Console.Write("\n");
                i++;
            }
        }

        private static void TableDoWhileLoop()
        {
            const int columnLength = 15;
            Console.WriteLine("\t\t==== Multiplication Table ==== \t\t");
            int i = 1;
            do
            {
                int j = 1;
                do
                {
                    int res = i * j;
                    var str = $"{j} * {i} = {res}";
                    var spaceNumbers = columnLength - str.Length;
                    str += new string(' ', spaceNumbers);
                    Console.Write(str);
                    j++;
                } while (j <= 10);
                Console.Write("\n");
                i++;
            } while (i <= 10);
        }
    }
}
