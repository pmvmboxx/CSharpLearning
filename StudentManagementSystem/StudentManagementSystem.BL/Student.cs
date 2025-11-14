namespace StudentManagementSystem.BL
{
    public struct Student
    {

        private long _recordBookNumber;
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;
        private Status status;
        private int[] _grades;

        #region ---+++===$$$ Properties $$$===+++---

        public long RecordBookNumber
        {
            get
            {
                return _recordBookNumber;
            }
            set
            {
                status = Status.OK;

                if (value <= 0)
                {
                    status = Status.RecordBookNumberShouldBePositive;
                }
                else
                {
                    _recordBookNumber = value;

                }
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                status = Status.OK;

                if (string.IsNullOrWhiteSpace(value))
                {
                    status = Status.EmptyFirstName;
                }
                else
                {
                    _firstName = value;
                }
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                status = Status.OK;

                if (string.IsNullOrWhiteSpace(value))
                {
                    status = Status.EmptyLastName;
                }
                else
                {
                    _lastName = value;
                }
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                status = Status.OK;

                if (value > DateTime.Now)
                {

                    status = Status.InvalidBirthday;
                }
                else
                {
                    _birthDate = value;
                }
            }
        }

        // TODO: encapsulation of grades array
        // TODO: реализовать, как индексатор
        // TODO: grades count
        public int[] Grades
        {
            get
            {
                return _grades ?? new int[0]; 
            }
            set
            {
                status = Status.OK;

                if (value == null || value.Length == 0)
                {
                    status = Status.EmptyGrades;
                }
                else 
                {
                    _grades = value;
                }
            }
        }

        #endregion 

        // констуруктор
        //TODO: clone 
        //TODO: params for grades - для добавления нескольких оценок
        public Student(long recordBookNumber, string firstName, string lastName, DateTime birthDate, params int[] grades)
        {
            _firstName = string.Empty;
            _lastName = string.Empty; 
            //_grades = Array.Empty<int>();
            

            RecordBookNumber = recordBookNumber;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Grades = grades;

        }
        public double GetAverageGrade()
        {
            return Grades.Length == 0 ? 0 : Grades.Average();
        }
        public double AverageGrade
        {
            get
            {
                return GetAverageGrade();
            }
            set
            {
                _grades = new int[] { (int)Math.Round(value) };
            }
        }

        public Status GetLastOperationStatus()
        {
            return status;
        }

        public override string ToString()
        {
            return $"{RecordBookNumber} | {LastName} {FirstName} | Date of birth: {BirthDate:dd.MM.yyyy}";
        }


    }
}
