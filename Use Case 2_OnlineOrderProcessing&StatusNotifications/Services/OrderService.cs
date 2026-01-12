using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Enums;
using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models;

namespace Use_Case_2_OnlineOrderProcessing_StatusNotifications.Services
{
    /// <summary>
    /// Represents a method that is called when the status of an order changes.
    /// </summary>
    /// <remarks>Use this delegate to handle notifications or perform actions in response to order status
    /// transitions, such as updating user interfaces or triggering business logic.</remarks>
    
    public delegate void OrderStatusChangedDelegate(Order order, OrderStatus oldStatus, OrderStatus newStatus);

    /// <summary>
    /// Provides operations for managing products, customers, and orders, including creation, retrieval, and status
    /// tracking within the order management system.
    /// </summary>
    /// <remarks>OrderService maintains in-memory collections for products, customers, and orders, and tracks
    /// order status changes. It supports validating order status transitions and notifies subscribers of status changes
    /// via the OnStatusChanged delegate. This class is not thread-safe; concurrent access should be synchronized
    /// externally if used in multi-threaded scenarios.</remarks>
    public class OrderService
    {
        // Generic collections
        private Dictionary<int, Product> _products; // Quick lookup by ProductId
        private Dictionary<int, Order> _orders; // Quick lookup by OrderId
        private List<OrderStatusLog> _statusHistory; // Status change history
        private Dictionary<int, Customer> _customers; // Quick lookup by CustomerId

        // Delegate instance for notifications
        public OrderStatusChangedDelegate? OnStatusChanged;

        public OrderService()
        {
            _products = new Dictionary<int, Product>();
            _orders = new Dictionary<int, Order>();
            _statusHistory = new List<OrderStatusLog>();
            _customers = new Dictionary<int, Customer>();
        }

        // Add product
        public void AddProduct(Product product)
        {
            _products[product.ProductId] = product;
        }

        // Get product by ID
        public Product? GetProduct(int productId)
        {
            return _products.ContainsKey(productId) ? _products[productId] : null;
        }

        // Add customer
        public void AddCustomer(Customer customer)
        {
            _customers[customer.CustomerId] = customer;
        }

        // Get customer by ID
        public Customer? GetCustomer(int customerId)
        {
            return _customers.ContainsKey(customerId) ? _customers[customerId] : null;
        }

        // Create order
        public Order CreateOrder(int orderId, Customer customer, DateTime orderDate)
        {
            var order = new Order(orderId, customer, orderDate);
            _orders[orderId] = order;
            return order;
        }

        // Get order by ID
        public Order? GetOrder(int orderId)
        {
            return _orders.ContainsKey(orderId) ? _orders[orderId] : null;
        }

        // Get all orders
        public Dictionary<int, Order> GetAllOrders()
        {
            return _orders;
        }

        // Get status history
        public List<OrderStatusLog> GetStatusHistory()
        {
            return _statusHistory;
        }

        // Get status history for specific order
        public List<OrderStatusLog> GetStatusHistoryForOrder(int orderId)
        {
            return _statusHistory.Where(log => log.OrderId == orderId).OrderBy(log => log.Timestamp).ToList();
        }

        // Validate status transition
        private bool IsValidTransition(OrderStatus oldStatus, OrderStatus newStatus)
        {
            // Cancelled orders cannot progress
            if (oldStatus == OrderStatus.Cancelled)
                return false;

            // Cannot cancel if already delivered
            if (newStatus == OrderStatus.Cancelled && oldStatus == OrderStatus.Delivered)
                return false;

            // Valid transitions
            return (oldStatus, newStatus) switch
            {
                (OrderStatus.Created, OrderStatus.Paid) => true,
                (OrderStatus.Created, OrderStatus.Cancelled) => true,
                (OrderStatus.Paid, OrderStatus.Packed) => true,
                (OrderStatus.Paid, OrderStatus.Cancelled) => true,
                (OrderStatus.Packed, OrderStatus.Shipped) => true,
                (OrderStatus.Packed, OrderStatus.Cancelled) => true,
                (OrderStatus.Shipped, OrderStatus.Delivered) => true,
                (OrderStatus.Shipped, OrderStatus.Cancelled) => false, // Cannot cancel after shipping
                _ => false
            };
        }

        // Change order status with validation
        public bool ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {
            var order = GetOrder(orderId);
            if (order == null)
            {
                Console.WriteLine($"Error: Order {orderId} not found.");
                return false;
            }

            OrderStatus oldStatus = order.Status;

            // Validate transition
            if (!IsValidTransition(oldStatus, newStatus))
            {
                Console.WriteLine($"Error: Invalid status transition from {oldStatus} to {newStatus} for Order {orderId}.");
                return false;
            }

            // Change status
            order.Status = newStatus;

            // Record in history
            var log = new OrderStatusLog(orderId, oldStatus, newStatus, DateTime.Now);
            _statusHistory.Add(log);

            // Invoke delegate (multicast - calls all subscribed methods)
            OnStatusChanged?.Invoke(order, oldStatus, newStatus);

            return true;
        }

        // Get orders by customer
        public List<Order> GetOrdersByCustomer(int customerId)
        {
            return _orders.Values.Where(o => o.Customer.CustomerId == customerId).ToList();
        }
    }
}
