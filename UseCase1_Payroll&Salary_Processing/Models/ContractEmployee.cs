using UseCase1_Payroll_Salary_Processing.Models;

namespace UseCase1_Payroll_Salary_Processing.Models
{
    // Derived class for Contract employees
    public class ContractEmployee : Employee
    {
        private decimal _hourlyRate;
        private int _hoursWorked;

        public decimal HourlyRate
        {
            get => _hourlyRate;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Hourly rate cannot be negative");
                _hourlyRate = value;
            }
        }

        public int HoursWorked
        {
            get => _hoursWorked;
            set
            {
                if (value < 0 || value > 200)
                    throw new ArgumentException("Hours worked must be between 0 and 200");
                _hoursWorked = value;
            }
        }

        public ContractEmployee(int employeeId, string name, string email, string department, DateTime joinDate, decimal hourlyRate, int hoursWorked)
            : base(employeeId, name, email, department, joinDate, "Contract")
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }

        // Override salary calculation for Contract employees
        public override decimal CalculateGrossSalary()
        {
            return HourlyRate * HoursWorked;
        }
    }
}

