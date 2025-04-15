namespace HW1_MultiplicationTable
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //TableForLoop();
            //TableWhileLoop();
            //TableDoWhileLoop();
            TableOutput();
            Console.ReadKey();
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
            var x = positionX;
            var y = positionY;
            for (int i = 1; i <= 10; i++)
            {
                Console.SetCursorPosition(x, y++);
                Console.Write($"{tableNumber} * {i} = {tableNumber * i}");
            }
        }

        private static void TableOutput()
        {
            const int defaultColumnLength = 15;
            const int defaultFrom = 1;
            const int defaultTo = 10;
            const int defaultColumnsNumber = 4;
            const int defaultRowHeight = 12;

            int from = GetInput(1, 10, "Enter the start point A: ") ?? defaultFrom;
            int to = GetInput(from, 10, "Enter the end point B: ") ?? defaultTo;
            int columnNumber = GetInput(1, 9, "Enter the column number: ") ?? defaultColumnsNumber;
            int rowNumbers = (int)Math.Ceiling((double)(to - from + 1) / columnNumber);
            int currentNumber = from;

            Console.Clear();
            DrawAllTables(defaultColumnLength, defaultRowHeight, to, columnNumber, rowNumbers, currentNumber);
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
    }
}
