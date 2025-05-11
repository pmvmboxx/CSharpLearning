using HW1_MultiplicationTable;
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
            string[] menuItems = {
                "Table with For Loop",
                "Table with While Loop",
                "Table with DoWhile Loop",
                "Multiplication Table Task",
                "exit"
            };

            string choice = string.Empty;

            do
            {
                // TODO: добавить параметры, чтобы можно было передавать разные меню
                int selectedOption = ConsoleViewer.ShowMenu("TASK MENU", menuItems);

                Console.Write("Choose the task (or 0 to exit): ");
                choice = selectedOption.ToString();

                switch (choice)
                {
                    case "1":

                        TableForLoop(DEFAULT_COLUMN_START, DEFAULT_COLUMN_END);
                        break;
                    case "2":
                        TableWhileLoop(DEFAULT_COLUMN_START, DEFAULT_COLUMN_END);
                        break;
                    case "3":
                        TableDoWhileLoop(DEFAULT_COLUMN_START, DEFAULT_COLUMN_END);
                        break;
                    case "4":
                        // TODO: реализовать ограничение по длине строки <=80 
                        // TODO: посмотреть про локализацию
                        int from_point = TryGetInput(DEFAULT_FROM, DEFAULT_TO, "the start point A", out int res) ? res : DEFAULT_FROM;
                        int to_point = TryGetInput(from_point, DEFAULT_TO, "the end point B", out res) ? res : DEFAULT_TO;
                        int columnQuantity = TryGetInput(COLUMN_NUMBER_RANGE_START,
                            COLUMN_NUMBER_RANGE_END, "the column number", out res)
                            ? res : DEFAULT_COLUMN_QUANTITY;
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

        private static bool TryGetInput(int minValue, int maxValue, string message, out int res)
        {
            string msg = string.Format("Please, enter {0} between {1} and {2}: ",
                        message, minValue, maxValue);
            Console.Write(msg);
            bool fOK = false;

            do
            {
                string? str = Console.ReadLine();
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

        private static void CalculateRows(int from_point, int to_point,
                int columnQuantity, out int rowQuantity, out int currentNumber)
        {

            int tablesQuantity = to_point - from_point + 1;
            rowQuantity = (int)Math.Ceiling((double)(to_point - from_point + 1) / columnQuantity);
            currentNumber = from_point;
        }

        private static void DrawAllTables(int to, int columnQuantity,
                int rowQuantity, int currentNumber)
        {
            int tableWidth = DEFAULT_COLUMN_WIDTH;
            int tableHeight = DEFAULT_COLUMN_END;

            int totalWidth = columnQuantity * tableWidth + 2;
            int totalHeight = rowQuantity * tableHeight + 6;  

            Console.WriteLine("\t==== Multiplication Table ==== \t");
            ConsoleViewer.DrawBoarders(totalWidth, totalHeight);

            bool done = false;

            for (int row = 0; row < rowQuantity && !done; row++)
            {
                int posY = row * (tableHeight + 1) + 1;

                for (int col = 0; col < columnQuantity; col++)
                {
                    int posX = col * tableWidth + 1; 

                    for (int i = 1; i <= DEFAULT_COLUMN_END; i++)
                    {
                        Console.SetCursorPosition(posX, posY + i);
                        string content = $"{currentNumber} * {i} = {currentNumber * i}";
                        Console.Write(content);
                    }

                    if (currentNumber == to)
                    {
                        done = true;
                        break;
                    }

                    currentNumber++;
                }

                if (row < rowQuantity - 1)
                {
                    Console.SetCursorPosition(1, posY + tableHeight);
                    Console.WriteLine(new string('-', totalWidth - 2));
                }
            }
        }



        private static void TableForLoop(int startNumber, int endNumber)
        {
            
            for (int i = 1; i <= 10; i++)
            {
                for (int j = startNumber; j <= endNumber; j++)
                {
                    Console.Write(FormatTable(i, j));
                    if (j == 10)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }

        private static void TableWhileLoop(int startNumber, int endNumber)
        {
            Console.WriteLine("\t\t==== Multiplication Table ==== \t\t");
            int i = 1;
            while (i <= 10)
            {
                int j = startNumber;
                while (j <= endNumber)
                {
                    Console.Write(FormatTable(i, j));
                    j++;
                }
                Console.Write("\n");
                i++;
            }
        }

        private static void TableDoWhileLoop(int startNumber, int endNumber)
        {
            Console.WriteLine("\t\t==== Multiplication Table ==== \t\t");
            int i = 1;
            do
            {
                int j = startNumber;
                do
                {
                    Console.Write(FormatTable(i, j));
                    j++;
                } while (j <= endNumber);
                Console.Write("\n");
                i++;
            } while (i <= 10);
        }

        private static string FormatTable(int i, int j)
        {
            // TODO: придумать capacity
            int res = i * j;
            StringBuilder sb = new StringBuilder();

            sb.Append($"{j} * {i} = {res}");
            int spaceNumbers = DEFAULT_COLUMN_WIDTH - sb.Length;
            sb.Append(' ', spaceNumbers);

            return sb.ToString();
        }
    }
}
