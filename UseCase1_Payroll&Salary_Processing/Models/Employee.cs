namespace UseCase1_Payroll_Salary_Processing.Models
{
    // Base Employee class with encapsulation
    public abstract class Employee
    {
        // Private fields (encapsulation)
        private int _employeeId;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _department = string.Empty;
        private DateTime _joinDate;
        private string _employeeType = string.Empty;

        // Public properties
        public int EmployeeId
        {
            get => _employeeId;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Employee ID must be positive");
                _employeeId = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");
                _name = value;
            }
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Department
        {
            get => _department;
            set => _department = value;
        }

        public DateTime JoinDate
        {
            get => _joinDate;
            set => _joinDate = value;
        }

        public string EmployeeType
        {
            get => _employeeType;
            protected set => _employeeType = value;
        }

        // Constructor
        protected Employee(int employeeId, string name, string email, string department, DateTime joinDate, string employeeType)
        {
            EmployeeId = employeeId;
            Name = name;
            Email = email;
            Department = department;
            JoinDate = joinDate;
            EmployeeType = employeeType;
        }

        // Virtual method for salary calculation (polymorphism)
        public abstract decimal CalculateGrossSalary();
    }
}

