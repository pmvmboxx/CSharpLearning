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
        const int DEFAULT_COLUMN_NUMBER = 4;
        const int DEFAULT_ROW_HEIGHT = 12;

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
                        Output();
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

        private static int? GetInput(int minValue, int maxValue, string message)
        {
            Console.Write(message);
            do
            {
                var str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                if (int.TryParse(str, out var res) && res >= minValue && res <= maxValue)
                {
                    return res;
                }
                Console.WriteLine($"Please, enter the correct value between {minValue} and {maxValue}: ");
            } while (true);
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

        private static void Output()
        {

            int from = GetInput(1, 10, "Enter the start point A: ") ?? DEFAULT_FROM;
            int to = GetInput(from, 10, "Enter the end point B: ") ?? DEFAULT_FROM;
            int columnNumber = GetInput(1, 9, "Enter the column number: ") ?? DEFAULT_COLUMN_NUMBER;
            int rowNumbers = (int)Math.Ceiling((double)(to - from + 1) / columnNumber);
            int currentNumber = from;

            Console.Clear();
            DrawAllTables(DEFAULT_COLUMN_WIDTH, DEFAULT_ROW_HEIGHT, to, columnNumber, rowNumbers, currentNumber);
        }

        private static void DrawAllTables(int defaultColumnLength, int defaultRowHeight, int to, int columnNumber, int rowNumbers, int currentNumber)
        {
            for (int j = 0; j < rowNumbers; j++)
            {
                for (int i = 0; i < columnNumber; i++)
                {
                    int positionX = defaultColumnLength * i;
                    int positionY = defaultRowHeight * j;
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
            Console.WriteLine("\t\t==== Multiplication Table ==== \t\t");
            int i = 1;
            while (i <= 10)
            {
                int j = 1;
                while (j <= 10)
                {
                    int res = i * j;
                    Console.Write($"{j} * {i} = {res}\t");
                    j++;
                }
                Console.Write("\n");
                i++;
            }
        }

        private static void TableDoWhileLoop()
        {
            Console.WriteLine("\t\t==== Multiplication Table ==== \t\t");
            int i = 1;
            do
            {
                int j = 1;
                do
                {
                    int res = i * j;
                    Console.Write($"{j} * {i} = {res}\t");
                    j++;
                } while (j <= 10);
                Console.Write("\n");
                i++;
            } while (i <= 10);
        }
    }
}
