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
