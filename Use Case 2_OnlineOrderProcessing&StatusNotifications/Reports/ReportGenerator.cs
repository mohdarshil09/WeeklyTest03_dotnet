using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Enums;
using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models;
using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Services;

namespace Use_Case_2_OnlineOrderProcessing_StatusNotifications.Reports
{
    // Report generator class
    public class ReportGenerator
    {
        // Print order summary
        public static void PrintOrderSummary(Order order, List<OrderStatusLog> statusHistory)
        {
            Console.WriteLine($"\n=== Order Summary - Order #{order.OrderId} ===");
            Console.WriteLine($"Customer: {order.Customer.Name} ({order.Customer.Email})");
            Console.WriteLine($"Order Date: {order.OrderDate:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Current Status: {order.Status}");
            Console.WriteLine($"\nOrder Items:");
            
            foreach (var item in order.OrderItems)
            {
                Console.WriteLine($"  - {item.Product.Name} (Qty: {item.Quantity}) @ ${item.UnitPrice:F2} = ${item.Subtotal:F2}");
            }

            Console.WriteLine($"\nFinancial Summary:");
            Console.WriteLine($"  Subtotal: ${order.Subtotal:F2}");
            Console.WriteLine($"  Discount: ${order.Discount:F2}");
            Console.WriteLine($"  Tax: ${order.Tax:F2}");
            Console.WriteLine($"  Total: ${order.Total:F2}");

            // Print status history timeline
            Console.WriteLine($"\n=== Status History Timeline ===");
            if (statusHistory.Count == 0)
            {
                Console.WriteLine("  No status changes recorded.");
            }
            else
            {
                foreach (var log in statusHistory)
                {
                    Console.WriteLine($"  [{log.Timestamp:yyyy-MM-dd HH:mm:ss}] {log.OldStatus} → {log.NewStatus}");
                }
            }
        }

        // Print all orders grouped by customer
        public static void PrintAllOrdersReport(OrderService orderService)
        {
            var orders = orderService.GetAllOrders();
            var customers = orders.Values.Select(o => o.Customer).DistinctBy(c => c.CustomerId).ToList();

            Console.WriteLine("\n=== All Orders Report ===");
            
            foreach (var customer in customers)
            {
                var customerOrders = orderService.GetOrdersByCustomer(customer.CustomerId);
                Console.WriteLine($"\nCustomer: {customer.Name} (ID: {customer.CustomerId})");
                Console.WriteLine($"Total Orders: {customerOrders.Count}");
                
                foreach (var order in customerOrders)
                {
                    var history = orderService.GetStatusHistoryForOrder(order.OrderId);
                    Console.WriteLine($"  Order #{order.OrderId}: Status = {order.Status}, Total = ${order.Total:F2}");
                }
            }
        }
    }
}
