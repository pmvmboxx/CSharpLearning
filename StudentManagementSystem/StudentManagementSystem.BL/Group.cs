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
        public const int DEFAULT_SIZE = 30; // дефолтный размер группы
        public const int MIN_GROUP_STARTYEAR = 1900; // минимальный год начала обучения
        public const int DEFAULT_RESIZE_VALUE = 2; // на сколько увеличивать массив студентов при нехватке места

        private string _groupNumber; // номер группы
        private int _startYear; // год начала обучения
        private Student[] _students; // массив студентов
        private int _studentCount; // количество студентов (размерность массива)
        private DegreeType _degreeType; // тип курса(длительность)

        #region ---+++===$$$ Properties $$$===+++---
        
        // статус последней операции
        public Status ErrorStatus
        {
            get;
            private set; // чтобы нельзя было изменить статус извне
        }

        // количество студентов в группе
        // и проверка на пустоту
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

        // год начала обучения
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

        // тип курса(длительность)
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

        #endregion

        // конструктор
        public Group(string groupNumber, int startYear, DegreeType degreeType, int groupSize = DEFAULT_SIZE)
        {
            _groupNumber = string.Empty;
            _students = new Student[groupSize];
            ErrorStatus = Status.OK;

            GroupNumber = groupNumber;
            StartYear = startYear;
            Degree = degreeType;
        }

        #region ---+++===$$$ Methods $$$===+++---

        // C - create
        public void AddStudent(Student student)
        {
            ErrorStatus = Status.BadOperation;

            if (_studentCount >= _students.Length)
            {
                Array.Resize(ref _students, _studentCount + 1);
            }

            _students[_studentCount] = student;
            _studentCount++;

            ErrorStatus = Status.OK;
        }


        //TODO: поиск группы по текстовому полю -> поиск имя\фамилия\ID (схема обработки + как предоставить результат поиска)
        //TODO: запретить измененние номера зачетки, кроме контруктора
        //TODO: разделение на 2 функ-ии(поиск+новые значения) + показать, какие поля разрешено изменить
        //TODO: возвращать индекс
        //TODO: reduce the number of returns

        // R - read
        public bool FindStudent(long recordBookNumber, out Student foundStudent)
        {
            bool isFound;
            int position = FindStudentPosition(recordBookNumber);

            if (position != -1)
            {
                foundStudent = _students[position];  
                ErrorStatus = Status.OK;
                isFound = true;                        // студент найден
            }
            else
            {
                foundStudent = default(Student);    
                ErrorStatus = Status.NotFound;
                isFound = false;                       // студент не найден
            }

            return isFound;
        }

        public int[] SearchStudents(string searchTerm)
        {
            int[] tempPositions = new int[_studentCount]; // тут храним позиции найденных студентов, которые подходят под заданую строку
            int count = 0; // счетчик для количества найденных студентов

            // проходимся по студентам и ищем совпадения
            for (int i = 0; i < _studentCount; i++)
            {
                if (_students[i].FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
                        || _students[i].LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
                        || _students[i].RecordBookNumber.ToString().Contains(searchTerm))
                {
                    tempPositions[count++] = i; // доабвляем индекс найденного студента в массив
                }
            }

            if (count == 0)
            {
                ErrorStatus = Status.NotFound;
                tempPositions = new int[0]; // ничего не найдено
            }
            else
            {
                ErrorStatus = Status.OK;
                Array.Resize(ref tempPositions, count); // найдено, и resize к количеству найденных совпадений
            }

            return tempPositions;
        }

        // U - update?

        //D - delete
        public Student? RemoveStudent(long recordBookNumber)
        {
            Student? removedStudent = null; // deleted student
            int position = FindStudentPosition(recordBookNumber);

            if (position != -1)
            {
                removedStudent = _students[position];

                // сдвигаем элементы массива, чтобы заполнить "дырку"
                if (position < _studentCount - 1)
                {
                    Array.Copy(_students, position + 1, _students, position, _studentCount - position - 1);
                }

                _studentCount--;
                ErrorStatus = Status.OK;
            }
            else
            {
                ErrorStatus = Status.NotFound;
            }

            return removedStudent; 
        }
        #endregion

        #region  ---+++===$$$ Auxiliary methods $$$===+++---

        // поиск позиции студента в массиве по номеру зачетки
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

        // получение текущего курса
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
    }
    #endregion

}
