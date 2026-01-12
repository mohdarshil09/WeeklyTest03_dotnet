namespace Use_Case_2_OnlineOrderProcessing_StatusNotifications.Enums
{
    /// <summary>
    /// Specifies the possible states of an order in the order processing workflow.
    /// </summary>
    /// <remarks>Use this enumeration to track and manage the lifecycle of an order, from creation through
    /// fulfillment or cancellation. The values represent distinct stages, allowing systems to determine appropriate
    /// actions or transitions based on the current status.</remarks>
    public enum OrderStatus
    {
        Created,
        Paid,
        Packed,
        Shipped,
        Delivered,
        Cancelled
    }
}
