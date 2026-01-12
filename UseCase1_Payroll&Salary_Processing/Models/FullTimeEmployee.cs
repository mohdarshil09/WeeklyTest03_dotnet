using UseCase1_Payroll_Salary_Processing.Models;

namespace UseCase1_Payroll_Salary_Processing.Models
{
    // Derived class for Full-Time employees
    public class FullTimeEmployee : Employee
    {
        private decimal _monthlySalary;
        private decimal _bonus;

        public decimal MonthlySalary
        {
            get => _monthlySalary;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Salary cannot be negative");
                _monthlySalary = value;
            }
        }

        public decimal Bonus
        {
            get => _bonus;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Bonus cannot be negative");
                _bonus = value;
            }
        }

        public FullTimeEmployee(int employeeId, string name, string email, string department, DateTime joinDate, decimal monthlySalary, decimal bonus)
            : base(employeeId, name, email, department, joinDate, "Full-Time")
        {
            MonthlySalary = monthlySalary;
            Bonus = bonus;
        }

        // Override salary calculation for Full-Time employees
        public override decimal CalculateGrossSalary()
        {
            return MonthlySalary + Bonus;
        }
    }
}

