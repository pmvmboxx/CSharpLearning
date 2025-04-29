using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
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
            string choice = string.Empty;

            do
            {
                // TODO: добавить параметры, чтобы можно было передавать разные меню
                ShowMenu();

                Console.Write("Choose the task (or 0 to exit): ");
                choice = Console.ReadLine();

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
                        // TODO: реализовать ограничение по длине строки <=80 
                        // TODO: посмотреть про локализацию
                        int from_point = TryGetInput(DEFAULT_FROM, DEFAULT_TO, "the start point A", out int res) ? res : DEFAULT_FROM;
                        int to_point = TryGetInput(from_point, DEFAULT_TO, "the end point B", out res) ? res : DEFAULT_TO;
                        int columnQuantity = TryGetInput(COLUMN_NUMBER_RANGE_START, COLUMN_NUMBER_RANGE_END, "the column number", out res) ? res : DEFAULT_COLUMN_QUANTITY;
                        CalculateRows(from_point, to_point, columnQuantity, out int rowQuantity, out int currentNumber);

                        Console.Clear();
                        DrawAllTables(to_point, columnQuantity, rowQuantity, currentNumber);
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Incorrct input. Try again.\n");
                        break;
                }
            }
            while (choice != "0");
        }

        private static void ShowMenu()
        {
            // TODO: test
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
            string msg = string.Format("Please, enter {0} between {1} and {2}: ",
                        message, minValue, maxValue);
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
            for (int i = DEFAULT_COLUMN_START; i <= DEFAULT_COLUMN_END; i++)
            {
                Console.SetCursorPosition(positionX, positionY++);
                Console.Write($"{tableNumber} * {i} = {tableNumber * i}");
            }
        }

        private static void CalculateRows(int from_point, int to_point,
                int columnQuantity, out int rowQuantity, out int currentNumber)
        {

            int tablesQuantity = to_point - from_point + 1;
            rowQuantity = tablesQuantity / columnQuantity + tablesQuantity % columnQuantity == 0 ? 0 : 1;
            currentNumber = from_point;
        }

        private static void DrawAllTables(int to, int columnQuantity,
                int rowQuantity, int currentNumber)
        {
            bool done = false;

            for (int j = 0; j < rowQuantity && !done; j++)
            {
                for (int i = 0; i < columnQuantity; i++)
                {
                    int positionX = DEFAULT_COLUMN_WIDTH * i;
                    int positionY = DEFAULT_ROW_HEIGHT * j;
                    DrawTable(positionX, positionY, currentNumber);

                    if (currentNumber == to)
                    {
                        // TODO: как альтернативно закончить, без return (break?)
                        done = true;
                        break;
                    }

                    currentNumber++;
                }
            }
        }

        private static void TableForLoop()
        {
            Console.WriteLine("\t\t==== Multiplication Table ==== \t\t");
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    DisplayTable(i, j);
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
                    DisplayTable(i, j);
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
                    Console.WriteLine(FormatTable(i, j));
                    j++;
                } while (j <= 10);
                Console.Write("\n");
                i++;
            } while (i <= 10);
        }

        private static string FormatTable (int i, int j)
        {
            // TODO: придумать capacity
            int res = i * j;
            StringBuilder sb = new StringBuilder();

            sb.Append($"{j} * {i} = {res}");
            int spaceNumbers = DEFAULT_COLUMN_WIDTH - sb.Length;
            sb.Append(' ', spaceNumbers);

            return sb.ToString();
        }

        private static string DrawTopBoarder(int lengthForColumns, int lengthForRows) 
        {
            StringBuilder sb = new StringBuilder(lengthForColumns);
            sb.Append('');
        }

        private static string DrawLBottomBoarder() 
        {
        }

        private static string DrawMiddleLine()
        { 
        }


    }
}
