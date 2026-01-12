using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Enums;
using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models;
using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Services;
using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Notifications;
using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Reports;

namespace Use_Case_2_OnlineOrderProcessing_StatusNotifications
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create OrderService instance
            OrderService orderService = new OrderService();

            // Subscribe notification methods to delegate (multicast)
            orderService.OnStatusChanged += NotificationHandler.CustomerNotification;
            orderService.OnStatusChanged += NotificationHandler.LogisticsNotification;

            try
            {
                // Load products (5 products)
                Console.WriteLine("=== Loading Products ===\n");
                orderService.AddProduct(new Product(1, "Laptop", 80000, "Electronics", 10));
                orderService.AddProduct(new Product(2, "Mouse", 500, "Electronics", 50));
                orderService.AddProduct(new Product(3, "Keyboard", 2000, "Electronics", 30));
                orderService.AddProduct(new Product(4, "Monitor", 15000, "Electronics", 20));
                orderService.AddProduct(new Product(5, "Webcam", 3000, "Electronics", 15));
                Console.WriteLine("5 products loaded successfully.\n");

                // Load customers (3 customers)
                Console.WriteLine("=== Loading Customers ===\n");
                var customer1 = new Customer(1, "Rajesh Kumar", "rajesh@gmail.com", "123 Main St, Mumbai");
                var customer2 = new Customer(2, "Priya Sharma", "priya@gmail.com", "456 Park Ave, Delhi");
                var customer3 = new Customer(3, "Amit Patel", "amit@gmail.com", "789 Oak Rd, Bangalore");
                
                orderService.AddCustomer(customer1);
                orderService.AddCustomer(customer2);
                orderService.AddCustomer(customer3);
                Console.WriteLine("3 customers loaded successfully.\n");

                // Create orders (4 orders with multiple items)
                Console.WriteLine("=== Creating Orders ===\n");

                // Order 1
                var order1 = orderService.CreateOrder(1, customer1, DateTime.Now);
                order1.AddItem(new OrderItem(orderService.GetProduct(1)!, 1, 80000));
                order1.AddItem(new OrderItem(orderService.GetProduct(2)!, 2, 500));
                order1.CalculateTotals();
                Console.WriteLine($"Order #1 created successfully. Total: ${order1.Total:F2}");

                // Order 2
                var order2 = orderService.CreateOrder(2, customer2, DateTime.Now);
                order2.AddItem(new OrderItem(orderService.GetProduct(3)!, 1, 2000));
                order2.AddItem(new OrderItem(orderService.GetProduct(4)!, 1, 15000));
                order2.AddItem(new OrderItem(orderService.GetProduct(5)!, 1, 3000));
                order2.CalculateTotals();
                Console.WriteLine($"Order #2 created successfully. Total: ${order2.Total:F2}");

                // Order 3
                var order3 = orderService.CreateOrder(3, customer1, DateTime.Now);
                order3.AddItem(new OrderItem(orderService.GetProduct(1)!, 1, 80000));
                order3.AddItem(new OrderItem(orderService.GetProduct(4)!, 2, 15000));
                order3.CalculateTotals();
                Console.WriteLine($"Order #3 created successfully. Total: ${order3.Total:F2}");

                // Order 4
                var order4 = orderService.CreateOrder(4, customer3, DateTime.Now);
                order4.AddItem(new OrderItem(orderService.GetProduct(2)!, 5, 500));
                order4.AddItem(new OrderItem(orderService.GetProduct(3)!, 2, 2000));
                order4.CalculateTotals();
                Console.WriteLine($"Order #4 created successfully. Total: ${order4.Total:F2}\n");

                // Process order status changes
                Console.WriteLine("=== Processing Order Status Changes ===\n");

                // Order 1: Created → Paid → Packed → Shipped → Delivered
                Console.WriteLine("Processing Order #1:");
                orderService.ChangeOrderStatus(1, OrderStatus.Paid);
                orderService.ChangeOrderStatus(1, OrderStatus.Packed);
                orderService.ChangeOrderStatus(1, OrderStatus.Shipped);
                orderService.ChangeOrderStatus(1, OrderStatus.Delivered);

                // Order 2: Created → Paid → Packed
                Console.WriteLine("\nProcessing Order #2:");
                orderService.ChangeOrderStatus(2, OrderStatus.Paid);
                orderService.ChangeOrderStatus(2, OrderStatus.Packed);

                // Order 3: Created → Cancelled
                Console.WriteLine("\nProcessing Order #3:");
                orderService.ChangeOrderStatus(3, OrderStatus.Cancelled);

                // Order 4: Created → Paid
                Console.WriteLine("\nProcessing Order #4:");
                orderService.ChangeOrderStatus(4, OrderStatus.Paid);

                // Test invalid transitions
                Console.WriteLine("\n=== Testing Invalid Transitions ===\n");
                orderService.ChangeOrderStatus(2, OrderStatus.Delivered); // Cannot deliver before shipping
                orderService.ChangeOrderStatus(3, OrderStatus.Paid); // Cannot progress cancelled order

                // Print order summaries with status history
                Console.WriteLine("\n=== Order Summaries ===\n");
                var order1History = orderService.GetStatusHistoryForOrder(1);
                ReportGenerator.PrintOrderSummary(order1, order1History);

                var order2History = orderService.GetStatusHistoryForOrder(2);
                ReportGenerator.PrintOrderSummary(order2, order2History);

                var order3History = orderService.GetStatusHistoryForOrder(3);
                ReportGenerator.PrintOrderSummary(order3, order3History);

                var order4History = orderService.GetStatusHistoryForOrder(4);
                ReportGenerator.PrintOrderSummary(order4, order4History);

                // Print all orders report
                ReportGenerator.PrintAllOrdersReport(orderService);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
