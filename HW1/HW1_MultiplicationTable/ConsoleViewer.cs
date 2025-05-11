using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_MultiplicationTable
{
    public class ConsoleViewer
    {
        public static void DrawBoarders(int totalWidth, int totalHeight)
        {
            Console.SetCursorPosition(0, 1);
            Console.Write("╔" + new string('═', totalWidth - 2) + "╗");

            for (int i = 2; i < totalHeight - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("║");
                Console.SetCursorPosition(totalWidth - 1, i);
                Console.Write("║");
            }

            Console.SetCursorPosition(0, totalHeight - 1);
            Console.Write("╚" + new string('═', totalWidth - 2) + "╝");
        }

        //public static void DrawInnerSeparator(int totalWidth)
        //{
        //    Console.Write('╟'); 
        //    Console.Write(new string('─', totalWidth));
        //    Console.WriteLine('╢'); 
        //}

        public static int ShowMenu(string title, string[] options)
        {
            Console.WriteLine("=== " + title + " ===");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            string? input;
            int choice;

            do
            {
                Console.WriteLine("Choose an option: ");
                input = Console.ReadLine();

                if (!int.TryParse(input, out choice) || choice < 1 || choice > options.Length)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and {0}: ", options.Length);
                }

            }
            while (!int.TryParse(input, out choice) || choice < 1 || choice > options.Length);
            return choice;
        }

        public static int ShowMenuWithArrows(string title, string[] options)
        {
            int selected = 0;
            ConsoleKey key;

            Console.CursorVisible = false;

            do
            {
                Console.Clear();
                Console.WriteLine("=== " + title + " ===\n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"{(i + 1).ToString().PadLeft(2)}. {options[i]}");

                    // Reset colors
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selected = (selected == 0) ? options.Length - 1 : selected - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selected = (selected + 1) % options.Length;
                        break;
                }

            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;
            Console.Clear();

            return selected + 1;
        }
    }

}
