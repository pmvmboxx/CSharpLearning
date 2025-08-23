using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BL
{
    //TODO: перевод студентов\группы на след курс + архивная информация 
    //TODO: оценки студентов, привязка оценок к семестру

    public struct Group
    {
        public const int DEFAULT_SIZE = 30;
        public const int MIN_GROUP_STARTYEAR = 1900;

        private string _groupNumber;
        private int _startYear;
        private Student[] _students;
        private int _studentCount;
        private DegreeType _degreeType;

        // autoproperty
        public Status ErrorStatus
        {
            get;
            private set;
        }


        public string GroupNumber
        {
            get
            {
                return _groupNumber;
            }
            set
            {
                ErrorStatus = Status.OK;

                if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorStatus = Status.EmptyGroupNumber;
                }
                else
                {
                    _groupNumber = value;
                }
            }
        }

        public int StartYear
        {
            get
            {
                return _startYear;
            }
            set
            {
                ErrorStatus = Status.OK; 

                if (value < MIN_GROUP_STARTYEAR || value > DateTime.Now.Year)
                {
                    ErrorStatus = Status.InvalidStartYear;
                }
                else
                {
                    _startYear = value;
                }
            }
        }

        public DegreeType Degree
        {
            get => _degreeType;
            set
            {
                ErrorStatus = Status.OK;

                if (!Enum.IsDefined(typeof(DegreeType), value))
                {
                    ErrorStatus = Status.InvalidDegreeType;
                }
                else
                {
                    _degreeType = value;
                }
            }
        }

        public Group(string groupNumber, int startYear, DegreeType degreeType, int groupSize = DEFAULT_SIZE)
        {
            //_groupNumber = string.Empty;
            //_startYear = 0;
            _students = new Student[groupSize];
            //_studentCount = 0;
            //_degreeType = DegreeType.Bachelors; 
            ErrorStatus = Status.OK;

            GroupNumber = groupNumber;
            StartYear = startYear;
            Degree = degreeType;
        }


        public int GetCourse()
        {
            int currentYear = DateTime.Now.Year;
            int course = currentYear - StartYear + 1;
            int maxCourse;

            switch (Degree)
            {
                case DegreeType.Bachelors:
                    maxCourse = 4;
                    break;
                case DegreeType.Masters:
                    maxCourse = 2;
                    break;
                case DegreeType.PhD:
                    maxCourse = 5;
                    break;
                case DegreeType.TrainingCourse:
                    maxCourse = 1;
                    break;
                default:
                    maxCourse = 4;
                    break;
            }

            if (course > maxCourse)
            {
                course = maxCourse;
            }

            return course;
        }

        public void AddStudent(Student student)
        {
            ErrorStatus = Status.BadOperation;

            if (_studentCount >= _students.Length)
            {
                ResizeStudentsArray();
            }

            _students[_studentCount] = student;
            _studentCount++;

            ErrorStatus = Status.OK;
        }

        //TODO: 2?
        //TODO: передать размер в параметры
        //TODO: array.resize
        private void ResizeStudentsArray()
        {
            ErrorStatus = Status.BadOperation;

            int newSize = _students.Length + 5;
            Student[] newArray = new Student[newSize];
            for (int i = 0; i < _students.Length; i++)
            {
                newArray[i] = _students[i];
            }
            _students = newArray;

            ErrorStatus = Status.OK;

#if DEBUG
            Console.WriteLine("Added +5 students.");
#endif
        }


        //TODO: поиск группы по текстовому полю -> поиск имя\фамилия\ID (схема обработки + как предоставить результат поиска)
        //TODO: запретить измененние номера зачетки, кроме контруктора
        //TODO: разделение на 2 функ-ии(поиск+новые значения) + показать, какие поля разрешено изменить
        //TODO: возвращать индекс
        //TODO: reduce the number of returns

        public Student FindStudent(long recordBookNumber)
        {
            //bool found = Tr

            int position = FindStudentPosition(recordBookNumber);
            if (position != -1)
            {
                ErrorStatus = Status.OK;
                return _students[position];
            }
            else
            {
                ErrorStatus = Status.NotFound;
                return default(Student);
            }
        }

        public int[] SearchStudents(string searchTerm)
        {
            List<int> positions = new List<int>();

            for (int i = 0; i < _studentCount; i++)
            {
                if (_students[i].FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
                        || _students[i].LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
                        || _students[i].RecordBookNumber.ToString().Contains(searchTerm))
                {
                    positions.Add(i);
                }
            }

            if (positions.Count == 0)
                ErrorStatus = Status.NotFound;
            else
                ErrorStatus = Status.OK;

            return positions.ToArray();
        }

        //TODO: for1 - find(позиция студента), for2 - shift
        //TODO: использовать для studetn transfer
        //TODO: enum Status
        //TODO: array.copy
        //TODO: returns
        public Student RemoveStudent(long recordBookNumber)
        {
            int position = FindStudentPosition(recordBookNumber);
            if (position != -1)
            {
                Student removed = _students[position];
                for (int i = position; i < _studentCount - 1; i++)
                {
                    _students[i] = _students[i + 1];
                }
                _studentCount--;
                ErrorStatus = Status.OK;
                return removed;
            }
            else
            {
                ErrorStatus = Status.NotFound;
                return default(Student);
            }
        }

        private int FindStudentPosition(long recordBookNumber)
        {
            int position = -1;
            for (int i = 0; i < _studentCount; i++)
            {
                if (_students[i].RecordBookNumber == recordBookNumber)
                {
                    position = i;
                    break;
                }
            }

            return position;
        }

    }
}
