using UseCase1_Payroll_Salary_Processing.Models;

namespace UseCase1_Payroll_Salary_Processing.Services
{
    // Delegate for salary processed notification
    public delegate void SalaryProcessedDelegate(Models.PaySlip paySlip);

    // Service class for payroll processing
    public class PayrollProcessor
    {
        // Generic collection to store employees
        private List<Employee> _employees;

        // Generic collection to store payslips (Dictionary for quick lookup by EmployeeId)
        private Dictionary<int, Models.PaySlip> _paySlips;

        // Delegate instance for notifications
        public SalaryProcessedDelegate? OnSalaryProcessed;

        public PayrollProcessor()
        {
            _employees = new List<Employee>();
            _paySlips = new Dictionary<int, Models.PaySlip>();
        }

        // Add employee to collection
        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        // Calculate deductions based on employee type
        private decimal CalculateDeductions(Employee employee, decimal grossSalary)
        {
            // Full-Time: 10% tax + 5% insurance
            if (employee is FullTimeEmployee)
            {
                return grossSalary * 0.10m + grossSalary * 0.05m;
            }
            // Contract: 5% tax only
            else if (employee is ContractEmployee)
            {
                return grossSalary * 0.05m;
            }
            return 0;
        }

        // Process payroll for all employees using polymorphism
        public void ProcessPayroll()
        {
            foreach (Employee employee in _employees)
            {
                // Polymorphism: CalculateGrossSalary() is called without checking type
                decimal grossSalary = employee.CalculateGrossSalary();
                decimal deductions = CalculateDeductions(employee, grossSalary);
                decimal netSalary = grossSalary - deductions;

                // Create payslip
                Models.PaySlip paySlip = new Models.PaySlip(
                    employee.EmployeeId,
                    employee.Name,
                    employee.EmployeeType,
                    grossSalary,
                    deductions,
                    netSalary
                );

                // Store payslip in dictionary
                _paySlips[employee.EmployeeId] = paySlip;

                // Invoke delegate (multicast - calls all subscribed methods)
                OnSalaryProcessed?.Invoke(paySlip);
            }
        }

        // Get payslips for reporting
        public Dictionary<int, Models.PaySlip> GetPaySlips()
        {
            return _paySlips;
        }

        // Get employees for reporting
        public List<Employee> GetEmployees()
        {
            return _employees;
        }
    }
}

