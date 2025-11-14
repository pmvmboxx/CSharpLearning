using StudentManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.UI
{
    internal class ConsoleViewer
    {
        public static int ShowMenu(string title, string[] menuItems)
        {
            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("=== " + title + " ===\n");

                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {menuItems[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menuItems[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex == 0) ? menuItems.Length - 1 : selectedIndex - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex == menuItems.Length - 1) ? 0 : selectedIndex + 1;
                }

            } while (key != ConsoleKey.Enter);

            return selectedIndex;
        }


        public void GetStatus(Status status)
        {
            switch (status)
            {
                case Status.OK:
                    Console.WriteLine("Operation completed successfully.");
                    break;
                case Status.EmptyFirstName:
                    Console.WriteLine("First name can't be empty.");
                    break;
                case Status.EmptyLastName:
                    Console.WriteLine("Last name can't be empty.");
                    break;
                case Status.InvalidBirthday:
                    Console.WriteLine("Invalid birthday date.");
                    break;
                case Status.BadOperation:
                    Console.WriteLine("Bad operation.");
                    break;
                case Status.NotFound:
                    Console.WriteLine("Student not found.");
                    break;
                default:
                    Console.WriteLine("Unknown status.");
                    break;
            }
        }

        //public void PrintInfo()
        //{
        //    Console.WriteLine($"RecordBook: {RecordBookNumber}, Name: {FirstName} {LastName}, Birth: {BirthDate.ToShortDateString()}");
        //    Console.WriteLine("Grades: " + string.Join(", ", Grades));
        //}
    }
}
