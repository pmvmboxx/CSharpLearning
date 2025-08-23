namespace StudentManagementSystem.BL
{
    public struct Student
    {

        private long _recordBookNumber;
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;
        private Status status;

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

        public Student(long recordBookNumber, string firstName, string lastName, DateTime birthDate)
        {
            _recordBookNumber = 0;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _birthDate = DateTime.MinValue;

            RecordBookNumber = recordBookNumber;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;

        }

        // GetLastOperationStatus() - readonly
        // enum - LastOperationStatus(Ok, InvalidBirthday, EmptyFirstName, etc.) - alternative to exceptions
        public void ShowStatus(Status status)
        {
            switch (status)
            {
                case Status.OK:
                    Console.WriteLine("Student is valid.");
                    break;
                case Status.EmptyFirstName:
                    Console.WriteLine("First name is missing.");
                    break;
                case Status.EmptyLastName:
                    Console.WriteLine("Last name is missing.");
                    break;
                case Status.InvalidBirthday:
                    Console.WriteLine("Birthday is not valid.");
                    break;
                default:
                    Console.WriteLine("Unknown error.");
                    break;
            }
        }

        public override string ToString()
        {
            return $"{RecordBookNumber} | {LastName} {FirstName} | Date of birth: {BirthDate:dd.MM.yyyy}";
        }


    }
}
