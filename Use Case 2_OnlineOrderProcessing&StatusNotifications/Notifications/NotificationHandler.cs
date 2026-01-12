using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Enums;
using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models;

namespace Use_Case_2_OnlineOrderProcessing_StatusNotifications.Notifications
{
    // Notification handler class
    public class NotificationHandler
    {
        // Customer Notification method 
        public static void CustomerNotification(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            Console.WriteLine($"[Customer Notification] Order #{order.OrderId} status changed from {oldStatus} to {newStatus}. " +
                $"Customer: {order.Customer.Name} ({order.Customer.Email})");
        }

        // Logistics Notification method (subscribed to delegate)
        public static void LogisticsNotification(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            if (newStatus == OrderStatus.Shipped || newStatus == OrderStatus.Delivered)
            {
                Console.WriteLine($"[Logistics Notification] Order #{order.OrderId} is now {newStatus}. " +
                    $"Shipping Address: {order.Customer.Address}. Total Items: {order.OrderItems.Count}");
            }
        }
    }
}
