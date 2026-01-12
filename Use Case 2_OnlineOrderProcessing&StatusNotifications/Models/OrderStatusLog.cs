using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Enums;

namespace Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models
{
    /// <summary>
    /// Represents a log entry recording a change in the status of an order, including the previous and new status, the
    /// time of the change, and any associated notes.
    /// </summary>
    /// <remarks>Use this class to track the history of status changes for an order. Each instance captures a
    /// single transition, allowing for detailed auditing and analysis of order processing events.</remarks>
    public class OrderStatusLog
    {
        public int OrderId { get; set; }
        public OrderStatus OldStatus { get; set; }
        public OrderStatus NewStatus { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; } = string.Empty;

        public OrderStatusLog(int orderId, OrderStatus oldStatus, OrderStatus newStatus, DateTime timestamp, string notes = "")
        {
            OrderId = orderId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            Timestamp = timestamp;
            Notes = notes;
        }
    }
}
