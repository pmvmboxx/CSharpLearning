using StudentManagementSystem.BL;

namespace StudentManagementSystem.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Student student1 = new Student(1, "Maryna", "Ponomarenko", new DateTime(2005, 1, 15));
            //Console.WriteLine(student1);

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string[] menuItems = {
             "Знайти студента",
             "Знайти групу",
             "Створити нову групу",
             "Створити нового студента",
             "Редагувати дані студента",
             "Вийти"
            };

            Group[] allGroups = new Group[5];
            int groupCount = 0;
            ConsoleViewer statuses = new ConsoleViewer();

            TaskType choice;

            do
            {
                int selectedOption = ConsoleViewer.ShowMenu("STUDENT MANAGEMENT SYSTEM", menuItems);

                choice = (TaskType)selectedOption;

                switch (choice)
                {
                    case TaskType.AddNewStudent:
                        Console.Write("Введіть номер групи для додавання студента: ");
                        string targetGroup = Console.ReadLine();

                        Group groupToAdd = Array.Find(allGroups, g => g.GroupNumber == targetGroup);

                        Console.WriteLine("Введіть дані студента:");
                        long recordBook = long.Parse(Console.ReadLine());
                        string firstName = Console.ReadLine();
                        string lastName = Console.ReadLine();
                        DateTime birthDate = DateTime.Parse(Console.ReadLine());

                        Student student = new Student(recordBook, firstName, lastName, birthDate, 70, 80, 95);
                        groupToAdd.Add(student);

                        Console.WriteLine("Студента додано.");
                        break;

                    case TaskType.CreateNewGroup:

                        Console.Write("Введіть номер групи: ");
                        string groupNumber = Console.ReadLine();

                        Console.Write("Введіть рік початку: ");
                        int startYear = int.Parse(Console.ReadLine());

                        Console.WriteLine("Виберіть тип ступеня (0-Bachelors, 1-Masters, 2-PhD, 3-TrainingCourse): ");
                        DegreeType degree = (DegreeType)int.Parse(Console.ReadLine());

                        Group newGroup = new Group(groupNumber, startYear, degree);
                        allGroups[groupCount] = newGroup;
                        groupCount++;

                        Console.WriteLine("Групу створено.");
                        break;

                    //TODO: отдельный метод для foreach
                    case TaskType.FindAStudent:

                        Console.Write("Введіть номер заліковки студента: ");
                        long searchRB = long.Parse(Console.ReadLine());

                        //foreach (var g in allGroups)
                        //{
                        //    Student found = g.FindStudent(searchRB);
                        //    if (g.ErrorStatus == Status.OK)
                        //    {
                        //        Console.WriteLine($"Студент знайдено у групі {g.GroupNumber}: {found}");
                        //        break;
                        //    }
                        //}
                        break;
                    case TaskType.FindAGroup:
                        Console.Write("Введіть текст для пошуку: ");
                        string query = Console.ReadLine();

                        foreach (var g in allGroups)
                        {
                            int[] indices = g.SearchStudents(query);
                            if (g.ErrorStatus == Status.OK)
                            {
                                Console.WriteLine($"Група: {g.GroupNumber}");
                                foreach (int i in indices)
                                {
                                    
                                }
                            }
                        }
                        break;

                    case TaskType.EditAStudent:
                        //Console.Write("Введіть номер групи: ");
                        //string groupToRemoveFrom = Console.ReadLine();

                        //Group groupRemove = Array.Find(allGroups, g => g.GroupNumber == groupToRemoveFrom);

                        //Console.Write("Введіть номер заліковки студента для видалення: ");
                        //long rbToRemove = long.Parse(Console.ReadLine());

                        //groupRemove.RemoveStudent(rbToRemove);
                        //Console.WriteLine("Операція завершена.");
                        //break;

                        break;
                }

            }
            while (choice != TaskType.Exit);
        }

        //TODO: UI
        //public void PrintGroup()
        //{
        //    Console.WriteLine($"Группа: {GroupNumber}, Курс: {GetCourse()}, Студентов: {_studentCount}");
        //    for (int i = 0; i < _studentCount; i++)
        //    {
        //        Console.WriteLine(_students[i]);
        //    }
        //}
    }
}
