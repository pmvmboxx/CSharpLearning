using Menu;

namespace OrdinaryArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string[] menuItems = {
             "Підмасив із сумою 0",
             "Обмін рядків у матриці (мін/макс)",
             "Побітове порівняння масивів"
            };

            TaskType choice;

            do
            {
                int selectedOption = ConsoleViewer.ShowMenuWithArrows("TASK MENU", menuItems);

                choice = (TaskType)selectedOption;

                switch (choice)
                {
                    case TaskType.Exit:
                        Console.WriteLine("До побачення!");
                        break;

                    case TaskType.SubarrayWithZeroSum:
                        int size = BL.SetArraySizeFromUser(); // запрашиваем размерность массива
                        int[] arr = new int[size];
                        BL.FillArrayFromUserOrRandom(arr); // запрашиваем заполнение массива
                        UI.PrintArray(arr); // выводим начальный массив

                        BL.HasZeroSubarray(arr);// функция для поиска подмассивов
                        // true\false + вывод подмассива
                        break;

                    case TaskType.SwapMinMaxRows:
                        int rows;
                        int cols;

                        (rows, cols) = BL.SetDimensionsMatrixFromUser(); // запрашиваем размерность матрицы
                        int[,] matrix = new int[rows, cols];
                        BL.FillMatrixFromUserOrRandom(matrix, rows, cols); // запрашиваем заполнение матрицы

                        UI.PrintMatrix(matrix); // выводим початкову матрицу

                        int maxRow = BL.GetMaxRowIndex(matrix, rows, cols);  // находим мин и макс
                        int minRow = BL.GetMinRowIndex(matrix, rows, cols);

                        // в result
                        BL.SwapRows(matrix, maxRow, minRow); // свапаем

                        UI.PrintMatrix(matrix);// выводим мин и макс + новую матрицу 
                        break;

                    case TaskType.BitwiseComparison:
                        int[] arr1 = new int[10]; // 2 одномерных массива с 10 цілих чисел
                        int[] arr2 = new int[10];

                        BL.FillArrayFromUserOrRandom(arr1);  // запрашиваем заполнение массивов
                        BL.FillArrayFromUserOrRandom(arr2);

                        UI.PrintArray(arr1); // выводим начальные массивы
                        UI.PrintArray(arr2); 

                        BL.CompareBitwise(arr1, arr2);  // побітове порівняння кожного елемента массиву - выводим dec \ hex               
                        break;
                }

            }
            while (choice != TaskType.Exit);
        }
    }
}
