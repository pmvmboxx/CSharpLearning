using StudentManagementSystem.BL;
using StudentManagementSystem.DAL;

namespace StudentManagementSystem.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Student student1 = new Student(1, "Maryna", "Ponomarenko", new DateTime(2005, 1, 15));
            //Console.WriteLine(student1);

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string filePath = "groups.csv";
            CsvGroupRepository repo = new CsvGroupRepository(filePath);

            Group[] allGroups = repo.LoadGroups();
            int groupCount = allGroups.Length;

            string[] menuItems = {
             "Вийти",
             "Створити нову групу",
             "Створити нового студента",                                                      
             "Знайти студента",
             "Знайти групу",
             "Редагувати дані студента",
             "Перевести групу на наступний курс",
             "Зберегти групи"
            };

            //Group[] allGroups = new Group[5];
            //int groupCount = 0;
            ConsoleViewer statuses = new ConsoleViewer();

            TaskType choice;

            do
            {
                int selectedOption = ConsoleViewer.ShowMenu("STUDENT MANAGEMENT SYSTEM", menuItems);

                choice = (TaskType)selectedOption;

                switch (choice)
                {
                    case TaskType.Exit:
                        break;
                    case TaskType.Create:
                        Console.Clear();
                        Console.WriteLine("=== Створення групи ===\n");

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
                        Console.WriteLine();

                        break;
                    case TaskType.Add:

                        if (groupCount == 0)
                        {
                            Console.WriteLine("Спочатку створіть хоча б одну групу.");
                            break;
                        }


                        string[] groupNames = new string[groupCount];
                        for (int i = 0; i < groupCount; i++)
                        {
                            groupNames[i] = $"{allGroups[i].GroupNumber} ({allGroups[i].Degree})";
                        }

                        int selectedGroupIndex = ConsoleViewer.ShowMenu("Виберіть групу для додавання студента", groupNames);
                        Group groupToAdd = allGroups[selectedGroupIndex];

                        Console.Clear();
                        Console.WriteLine($"Обрано групу: {groupToAdd.GroupNumber}\n");


                        Console.Write("Введіть номер студентського: ");
                        long recordBook = long.Parse(Console.ReadLine());
                        Console.Write("Введіть ім'я: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Введіть прізвище: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Введіть дату народження (у форматі рррр-мм-дд): ");
                        DateTime birthDate = DateTime.Parse(Console.ReadLine());

                        Student student = new Student(recordBook, firstName, lastName, birthDate, 70, 80, 95);
                        groupToAdd.Add(student);

                        Console.WriteLine("Студента додано.");
                        break;

                    case TaskType.FindAStudent:
                        Console.Clear();
                        Console.WriteLine("=== Пошук студента ===\n");

                        Console.Write("Введіть номер заліковки або ім'я/прізвище студента: ");
                        string query = Console.ReadLine();

                        bool foundAny = false;

                        foreach (var g in allGroups)
                        {
                            if (string.IsNullOrWhiteSpace(g.GroupNumber))
                            {
                                continue;
                            }


                            // за номером заліковки
                            if (long.TryParse(query, out long recordBookNumber))
                            {
                                if (g.FindStudent(recordBookNumber, out Student foundStudent))
                                {
                                    Console.WriteLine($"\nСтудента знайдено у групі {g.GroupNumber}:");
                                    Console.WriteLine(foundStudent);
                                    foundAny = true;
                                    break;
                                }
                            }
                            else
                            {
                                // за ім'ям або прізвищем
                                int[] indices = g.SearchStudents(query);
                                if (g.ErrorStatus == Status.OK && indices.Length > 0)
                                {
                                    Console.WriteLine($"\nГрупа: {g.GroupNumber}");
                                    foreach (int i in indices)
                                    {
                                        Console.WriteLine(g.GetStudentInfo(i));
                                    }
                                    foundAny = true;
                                }
                            }
                        }

                        if (!foundAny)
                        {
                            Console.WriteLine("\nСтудента не знайдено.");
                        }

                        Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...");
                        Console.ReadKey();
                        break;
                    case TaskType.FindAGroup:
                        Console.Clear();
                        Console.WriteLine("=== Пошук групи ===\n");

                        Console.Write("Введіть текст для пошуку (ім'я/прізвище/номер заліковки): ");
                        string queryGroup = Console.ReadLine();

                        bool foundGroup = false;

                        foreach (var g in allGroups)
                        {
                            if (string.IsNullOrWhiteSpace(g.GroupNumber))
                            {
                                continue;
                            }


                            int[] indices = g.SearchStudents(queryGroup);
                            if (g.ErrorStatus == Status.OK && indices.Length > 0)
                            {
                                Console.WriteLine($"\nГрупа: {g.GroupNumber} ({g.Degree}, {g.StartYear} р.)");
                                Console.WriteLine($"Студенти, які відповідають запиту \"{queryGroup}\":");

                                foreach (int i in indices)
                                {
                                    Console.WriteLine($" - {g.GetStudentInfo(i)}");
                                }

                                foundGroup = true;
                            }
                        }

                        if (!foundGroup)
                        {
                            Console.WriteLine("\nЖодної групи не знайдено.");
                        }

                        Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...");
                        Console.ReadKey();
                        break;

                    case TaskType.EditAStudent:
                        Console.Clear();
                        Console.WriteLine("=== Редагування студента ===");

                        string[] editGroupNames = allGroups.Select(g => $"{g.GroupNumber} ({g.Degree})").ToArray();
                        int editGroupIndex = ConsoleViewer.ShowMenu("Виберіть групу для редагування", editGroupNames);
                        Group selectedGroup = allGroups[editGroupIndex];

                        Console.Write("\nВведіть номер заліковки студента, якого хочете редагувати: ");
                        if (!long.TryParse(Console.ReadLine(), out long editRecordBook))
                        {
                            Console.WriteLine("Невірний формат номера заліковки.");
                            Console.ReadKey();
                            break;
                        }

                        if (selectedGroup.FindStudent(editRecordBook, out Student studentToEdit))
                        {
                            Console.WriteLine("\nСтудента знайдено:");
                            Console.WriteLine(studentToEdit);

                            Console.WriteLine("\nЯке поле ви хочете змінити?");
                            string[] editOptions =
                            {
                                "Змінити ім'я",
                                "Змінити прізвище",
                                "Змінити дату народження",
                                "Змінити середній бал",
                                "Видалити цього студента",
                                "Перевести студента в іншу групу",
                                "Вийти без змін"
                            };

                            int editChoice = ConsoleViewer.ShowMenu("Редагування", editOptions);

                            bool isEdited = false;
                            bool isDeleted = false;

                            switch (editChoice)
                            {
                                case 0:
                                    Console.Write("Введіть нове ім'я: ");
                                    studentToEdit.FirstName = Console.ReadLine();
                                    break;
                                case 1:
                                    Console.Write("Введіть нове прізвище: ");
                                    studentToEdit.LastName = Console.ReadLine();
                                    break;
                                case 2:
                                    Console.Write("Введіть нову дату народження (рррр-мм-дд): ");
                                    if (DateTime.TryParse(Console.ReadLine(), out DateTime newBirth))
                                    {
                                        studentToEdit.BirthDate = newBirth;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Невірний формат дати.");
                                    }
                                    break;
                                case 3:
                                    Console.Write("Введіть новий середній бал: ");
                                    if (double.TryParse(Console.ReadLine(), out double newGrade))
                                    {
                                        studentToEdit.AverageGrade = newGrade;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Невірний формат числа.");
                                    }
                                    break;
                                case 4:
                                    Console.Write("\nВи впевнені, що хочете видалити цього студента? (y/n): ");
                                    var confirm = Console.ReadKey().Key;
                                    if (confirm == ConsoleKey.Y)
                                    {
                                        selectedGroup.RemoveStudent(editRecordBook, out _);
                                        allGroups[editGroupIndex] = selectedGroup;
                                        Console.WriteLine("\nСтудента видалено!");
                                        isDeleted = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nОперацію скасовано.");
                                    }
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    Console.Clear();
                                    Console.WriteLine("=== Переведення студента в іншу групу ===");

                                    var targetGroupNames = allGroups
                                        .Where((g, idx) => idx != editGroupIndex)
                                        .Select(g => $"{g.GroupNumber} ({g.Degree})")
                                        .ToArray();

                                    if (targetGroupNames.Length == 0)
                                    {
                                        Console.WriteLine("Немає інших груп для переведення.");
                                        break;
                                    }

                                    int targetIndex = ConsoleViewer.ShowMenu("Оберіть нову групу", targetGroupNames);
                                    Group targetGroup = allGroups
                                        .Where((g, idx) => idx != editGroupIndex)
                                        .ElementAt(targetIndex);

                                    selectedGroup.RemoveStudent(editRecordBook, out _);
                                    allGroups[editGroupIndex] = selectedGroup;

                                    targetGroup.Add(studentToEdit);
                                    allGroups[Array.IndexOf(allGroups, targetGroup)] = targetGroup;

                                    Console.WriteLine($"\nСтудента переведено до групи {targetGroup.GroupNumber}!");
                                    isDeleted = true;
                                    break;
                                default:
                                    Console.WriteLine("Редагування скасовано.");
                                    break;
                            }

                            if (isEdited)
                            {
                                selectedGroup.UpdateStudent(editRecordBook, studentToEdit);
                                allGroups[editGroupIndex] = selectedGroup;
                                Console.WriteLine("\nДані студента оновлено!");
                            }

                            if (!isEdited && !isDeleted)
                            {
                                Console.WriteLine("\nЗміни не внесено.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nСтудента з таким номером заліковки не знайдено.");
                        }

                        Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...");
                        Console.ReadKey();
                        break;
                    case TaskType.AdvanceAGroup:
                        Console.Write("Введіть номер групи: ");
                        string groupToRemoveFrom = Console.ReadLine();

                        Group groupRemove = Array.Find(allGroups, g => g.GroupNumber == groupToRemoveFrom);

                        Console.Write("Введіть номер заліковки студента для видалення: ");
                        long rbToRemove = long.Parse(Console.ReadLine());

                        Student? removedStudent;

                        groupRemove.RemoveStudent(rbToRemove, out removedStudent);

                        if (removedStudent != null)
                        {
                            Console.WriteLine($"Студента {removedStudent.Value.FirstName} {removedStudent.Value.LastName} видалено.");
                        }
                        else
                        {
                            Console.WriteLine("Студента з таким номером заліковки не знайдено.");
                        }

                        Console.WriteLine("Операція завершена.");
                        break;
                    case TaskType.SaveGroups:
                        repo.SaveGroups(allGroups.Take(groupCount).ToArray());
                        Console.WriteLine("Групи збережено у CSV.");
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
