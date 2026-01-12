using UseCase1_Payroll_Salary_Processing.Models;
using UseCase1_Payroll_Salary_Processing.Services;

namespace UseCase1_Payroll_Salary_Processing.Reports
{
    // Report generator class
    public class ReportGenerator
    {
        // Generate summary report
        public static void GenerateSummaryReport(PayrollProcessor payrollProcessor)
        {
            var paySlips = payrollProcessor.GetPaySlips();
            var employees = payrollProcessor.GetEmployees();

            Console.WriteLine("=== Per-Employee Breakdown ===");
            foreach (var paySlip in paySlips.Values)
            {
                Console.WriteLine($"ID: {paySlip.EmployeeId} | Name: {paySlip.EmployeeName} | Type: {paySlip.EmployeeType} | " +
                    $"Gross: ${paySlip.GrossSalary:F2} | Deductions: ${paySlip.Deductions:F2} | Net: ${paySlip.NetSalary:F2}");
            }

            Console.WriteLine("\n=== Summary Statistics ===");
            Console.WriteLine($"Total Employees Processed: {paySlips.Count}");

            // Count by type
            int fullTimeCount = employees.Count(e => e is FullTimeEmployee);
            int contractCount = employees.Count(e => e is ContractEmployee);
            Console.WriteLine($"Full-Time Employees: {fullTimeCount}");
            Console.WriteLine($"Contract Employees: {contractCount}");

            // Totals
            decimal totalPayout = paySlips.Values.Sum(p => p.NetSalary);
            decimal averageSalary = paySlips.Values.Average(p => p.NetSalary);
            Console.WriteLine($"Total Payout: ${totalPayout:F2}");
            Console.WriteLine($"Average Net Salary: ${averageSalary:F2}");

            // Highest net salary employee
            var highestPaid = paySlips.Values.OrderByDescending(p => p.NetSalary).First();
            Console.WriteLine($"Highest Net Salary: {highestPaid.EmployeeName} (ID: {highestPaid.EmployeeId}) - ${highestPaid.NetSalary:F2}");
        }
    }
}

