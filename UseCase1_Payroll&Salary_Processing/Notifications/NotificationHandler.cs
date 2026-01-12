using UseCase1_Payroll_Salary_Processing.Models;

namespace UseCase1_Payroll_Salary_Processing.Notifications
{
    // Notification handler class
    public class NotificationHandler
    {
        // HR Notification method (subscribed to delegate)
        public static void HRNotification(PaySlip paySlip)
        {
            Console.WriteLine($"[HR Notification] Employee {paySlip.EmployeeId} ({paySlip.EmployeeName}) - Net Salary: ${paySlip.NetSalary:F2}");
        }

        // Finance Notification method (subscribed to delegate)
        public static void FinanceNotification(PaySlip paySlip)
        {
            Console.WriteLine($"[Finance Notification] Employee {paySlip.EmployeeId} ({paySlip.EmployeeName}) - Gross: ${paySlip.GrossSalary:F2}, Net: ${paySlip.NetSalary:F2}");
        }
    }
}

