namespace UseCase1_Payroll_Salary_Processing.Models
{
    // PaySlip class to store payroll results
    public class PaySlip
    {

        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeType { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }


        /// <summary>
        /// Initializes a new instance of the PaySlip class with the specified employee and salary details.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee for whom the pay slip is generated.</param>
        /// <param name="employeeName">The full name of the employee.</param>
        /// <param name="employeeType">The employment type of the employee, such as 'Full-Time', 'Part-Time', or 'Contractor'.</param>
        /// <param name="grossSalary">The gross salary amount before deductions are applied.</param>
        /// <param name="deductions">The total amount of deductions to be subtracted from the gross salary.</param>
        /// <param name="netSalary">The net salary amount after all deductions have been applied.</param>
        public PaySlip(int employeeId, string employeeName, string employeeType, decimal grossSalary, decimal deductions, decimal netSalary)
        {

           
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            EmployeeType = employeeType;
            GrossSalary = grossSalary;
            Deductions = deductions;
            NetSalary = netSalary;
        }
    }
}

