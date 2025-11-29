using StudentManagementSystem.BL;
using System.Globalization;
using System.Text;
using CSVProcessor;

namespace StudentManagementSystem.DAL
{
    public class CsvGroupRepository
    {
        private readonly string _filePath;

        public CsvGroupRepository(string filePath)
        {
            _filePath = filePath;
        }

        //оценки
        //public void SaveGroups(Group[] groups)
        //{
        //    using (var writer = new StreamWriter(_filePath, false, Encoding.UTF8))
        //    {
        //        foreach (var group in groups)
        //        {
        //            if (string.IsNullOrWhiteSpace(group.GroupNumber)) continue;

        //            // Упаковуємо студентів в один рядок
        //            string[] studentStrings = new string[group.StudentCount];
        //            for (int i = 0; i < group.StudentCount; i++)
        //            {
        //                var s = group.GetStudent(i);

        //                string studentStr =
        //                    $"{s.RecordBookNumber} {s.FirstName} {s.LastName} {s.BirthDate:yyyy-MM-dd} {s.Grade1} {s.Grade2} {s.Grade3}";

        //                studentStrings[i] = studentStr;
        //            }

        //            string line =
        //                $"{group.GroupNumber};{group.StartYear};{group.Degree};{string.Join("|", studentStrings)}";

        //            writer.WriteLine(line);
        //        }
        //    }
        //}


        //TODO: ReadGroups -> ReadStudents
        //TODO: доделать чтение перечня групп -> ReadStudents 
        //TODO: array of groups
        public static void ReadStudents(ref Group g, string fileName)
        {
            CSVReader reader = new CSVReader(fileName);
            while (!reader.EndOfData)
            {
                string[] sParts = reader.GetLine();
                if (sParts == null)
                {
                    continue;   
                }

                //foreach (var studentStr in studentStrings)
                {
                    //var sParts = studentStrings.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    //Student student1 = new Student(1, "Maryna", "Ponomarenko", new DateTime(2005, 1, 15));

                    if (/*sParts.Length >= 7 &&*/
                        long.TryParse(sParts[0], out long rb) &&
                        DateTime.TryParse(sParts[3], out DateTime birth)   /*&&
                       double.TryParse(sParts[4], out double g1) &&
                        double.TryParse(sParts[5], out double g2) &&
                        double.TryParse(sParts[6], out double g3)*/)
                    {
                        Student student = new Student(rb, sParts[1], sParts[2], birth);
                        g.Add(student);
                    }
                }
            }
            reader.Close();
        }
        //TODO: вычитка группы -> вычитка студента
        //TODO: faculty
        public static List<Group> ReadGroups(string fileName)
        {
            //var lines = File.ReadAllLines(_filePath, Encoding.UTF8);
            List<Group> groups = new List<Group>();

            CSVReader reader = new CSVReader(fileName);

            while (!reader.EndOfData)
            {
                string[] gParts = reader.GetLine();
                if (gParts == null)
                {
                    continue;
                }

                string groupNumber = gParts[0];
                int startYear = int.Parse(gParts[1]);
                DegreeType degree = Enum.Parse<DegreeType>(gParts[2]);

                groups.Add(new Group(groupNumber, startYear, degree));


            }
            reader.Close();

            return groups;
        }
        public static void ReadStudents(ref Group g)
        {
            string fileName = g.GroupNumber + ".csv";      

            CSVReader reader = new CSVReader(fileName);

            while (!reader.EndOfData)
            {
                string[] sParts = reader.GetLine();
                if (sParts == null)
                {
                    continue;
                }

                if (/*sParts.Length >= 7 &&*/
                    long.TryParse(sParts[0], out long rb) &&
                    DateTime.TryParse(sParts[3], out DateTime birth)) //&&
                    //double.TryParse(sParts[4], out double g1) &&
                    //double.TryParse(sParts[5], out double g2) &&
                    //double.TryParse(sParts[6], out double g3))
                {
                    Student student = new Student(rb, sParts[1], sParts[2], birth);
                    g.Add(student);
                }

            }

            reader.Close();
        }

        //var sParts = studentStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        //                    if (sParts.Length >= 7 &&
        //                        long.TryParse(sParts[0], out long rb) &&
        //                        DateTime.TryParse(sParts[3], out DateTime birth) &&
        //                        double.TryParse(sParts[4], out double g1) &&
        //                        double.TryParse(sParts[5], out double g2) &&
        //                        double.TryParse(sParts[6], out double g3))
        //                    {
        //                        Student student = new Student(rb, sParts[1], sParts[2], birth, g1, g2, g3);
        //                        group.Add(student);

        //public void SaveGroups(Group[] groups)
        //{
        //    using (var writer = new StreamWriter(_filePath, false, Encoding.UTF8))
        //    {
        //        foreach (var group in groups)
        //        {
        //            if (string.IsNullOrWhiteSpace(group.GroupNumber)) continue;

        //            // Упаковуємо студентів в один рядок
        //            string[] studentStrings = new string[group.StudentCount];
        //            for (int i = 0; i < group.StudentCount; i++)
        //            {
        //                var s = group.GetStudent(i);

        //                string studentStr =
        //                    $"{s.RecordBookNumber} {s.FirstName} {s.LastName} {s.BirthDate:yyyy-MM-dd} {s.Grade1} {s.Grade2} {s.Grade3}";

        //                studentStrings[i] = studentStr;
        //            }

        //            string line =
        //                $"{group.GroupNumber};{group.StartYear};{group.Degree};{string.Join("|", studentStrings)}";

        //            writer.WriteLine(line);
        //        }
        //    }

        //}

        //TODO: WriteStudents()
        public static void WriteGroups(string fileName, List<Group> source)
        {
            CSVWriter writer = new CSVWriter(fileName);

            for (int i = 0; i < source.Count; i++)
            {
                string[] lines = new string[]
                {
                    source[i].GroupNumber,
                    source[i].StartYear.ToString(),
                    source[i].Degree.ToString()
                };

                writer.Write(lines);
            }
            writer.Close();
        }

        public static void WriteStudents(Group g)
        {
            CSVWriter writer = new CSVWriter(g.GroupNumber + ".csv");

            for (int i = 0; i < g.StudentCount; i++)
            {
                if(!g.GetStudentInfo(i, out Student current))
                {
                    continue;
                }

                string[] lines = new string[]
                { 
                    current.RecordBookNumber.ToString(),
                    current.FirstName.ToString(),
                    current.LastName.ToString(),
                    current.BirthDate.ToString(),
                };

                writer.Write(lines);
            }
            writer.Close();
        }

        //public StudentManagementSystem.BL.Group[] LoadGroups()
        //{
        //    CSVReader reader = new CSVReader(_filePath);
        //    while (!reader.EndOfData)
        //    {
        //        string[] studentStrings = reader.GetLine();
        //        StudentManagementSystem.BL.Group group = new StudentManagementSystem.BL.Group(groupNumber, startYear, degree);

        //        foreach (var studentStr in studentStrings)
        //        {
        //            var sParts = studentStr.Split(' ');

        //            if (sParts.Length >= 7 &&
        //                long.TryParse(sParts[0], out long rb) &&
        //                DateTime.TryParse(sParts[3], out DateTime birth) &&
        //                double.TryParse(sParts[4], out double g1) &&
        //                double.TryParse(sParts[5], out double g2) &&
        //                double.TryParse(sParts[6], out double g3))
        //            {
        //                Student student = new Student(rb, sParts[1], sParts[2], birth, g1, g2, g3);
        //                group.Add(student);
        //            }
        //        }
        //    }

        //        if (!File.Exists(_filePath))
        //        {
        //            return new StudentManagementSystem.BL.Group[0];
        //        }

        //        var lines = File.ReadAllLines(_filePath, Encoding.UTF8);
        //        var groups = new StudentManagementSystem.BL.Group[lines.Length];

        //        int idx = 0;
        //        foreach (var line in lines)
        //        {
        //            if (string.IsNullOrWhiteSpace(line)) continue;
        //            //magic numbers
        //            var parts = line.Split(';');
        //            if (parts.Length < 4)
        //            {
        //                continue;
        //            }

        //            string groupNumber = parts[0];
        //            int startYear = int.Parse(parts[1]);
        //            DegreeType degree = Enum.Parse<DegreeType>(parts[2]);

        //            StudentManagementSystem.BL.Group group = new StudentManagementSystem.BL.Group(groupNumber, startYear, degree);
        //            //magic numbers
        //            //TODO: csv - comma(как параметр в конструкторе)
        //            //TODO: byte 
        //            string studentsPart = parts[3];
        //            if (!string.IsNullOrWhiteSpace(studentsPart))
        //            {
        //                string[] studentStrings = studentsPart.Split('|');
        //                foreach (var studentStr in studentStrings)
        //                {
        //                   
        //                    }
        //                }
        //            }

        //            groups[idx++] = group;
        //        }

        //        //обрезаем массив до фактического количества групп
        //        Array.Resize(ref groups, idx);

        //        return groups;
        //    }
    }
}
