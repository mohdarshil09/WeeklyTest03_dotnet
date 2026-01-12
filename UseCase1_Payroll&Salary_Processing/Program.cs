using UseCase1_Payroll_Salary_Processing.Models;
using UseCase1_Payroll_Salary_Processing.Services;
using UseCase1_Payroll_Salary_Processing.Notifications;
using UseCase1_Payroll_Salary_Processing.Reports;

namespace UseCase1_Payroll_Salary_Processing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create PayrollProcessor instance
            PayrollProcessor payrollProcessor = new PayrollProcessor();

            // Subscribe HR and Finance notification methods to delegate (multicast)
            payrollProcessor.OnSalaryProcessed += NotificationHandler.HRNotification;
            payrollProcessor.OnSalaryProcessed += NotificationHandler.FinanceNotification;

            // Hardcoded sample data (6 employees)
            try
            {
                // Add Full-Time employees
                payrollProcessor.AddEmployee(new FullTimeEmployee(1, "Modi ji", "modiji@gmail.com", "IT", new DateTime(2020, 1, 15), 50000, 5000));
                payrollProcessor.AddEmployee(new FullTimeEmployee(2, "Ashif", "aashif@gmail.com", "HR", new DateTime(2019, 3, 20), 45000, 3000));
                payrollProcessor.AddEmployee(new FullTimeEmployee(3, "Mohan", "mohan@gamil.com", "Finance", new DateTime(2021, 6, 10), 55000, 6000));

                // Add Contract employees
                payrollProcessor.AddEmployee(new ContractEmployee(4, "Asad", "asad@gmail.com", "IT", new DateTime(2022, 2, 1), 500, 160));
                payrollProcessor.AddEmployee(new ContractEmployee(5, "Ali", "ali@gmail.com", "Marketing", new DateTime(2022, 4, 15), 450, 120));
                payrollProcessor.AddEmployee(new ContractEmployee(6, "Ram", "ram@gamil.com", "IT", new DateTime(2023, 1, 5), 600, 180));

                Console.WriteLine("=== Payroll Processing Started ===\n");

                // Process payroll (uses polymorphism - no if/else checks)
                payrollProcessor.ProcessPayroll();

                Console.WriteLine("\n=== Payroll Summary Report ===\n");

                // Generate summary report
                ReportGenerator.GenerateSummaryReport(payrollProcessor);

                // Demonstrate validation with invalid input
                Console.WriteLine("\n=== Validation Test ===\n");
                try
                {
                    var invalidEmployee = new FullTimeEmployee(7, "Test", "test@gmail.com", "IT", DateTime.Now, -1000, 0);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Validation Error: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
