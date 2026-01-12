using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Enums;
using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models;
namespace Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models
{
    /// <summary>
    /// Represents a customer order, including order details, items, status, and calculated totals.
    /// </summary>
    /// <remarks>An Order encapsulates all information related to a single purchase transaction, including the
    /// customer, items purchased, order date, and financial calculations such as subtotal, discount, tax, and total
    /// amount. Use the AddItem method to add items to the order, and call CalculateTotals to update the financial
    /// fields based on the current items. The OrderItems collection is read-only from outside the class; items must be
    /// added using AddItem. The Status property tracks the current state of the order (such as Created, Processed, or
    /// Completed).</remarks>
    public class Order
    {
        private int _orderId;
        private Customer _customer = null!;
        private List<OrderItem> _orderItems;
        private OrderStatus _status;
        private DateTime _orderDate;
        private decimal _subtotal;
        private decimal _discount;
        private decimal _tax;
        private decimal _total;

        public int OrderId
        {
            get => _orderId;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Order ID must be positive");
                _orderId = value;
            }
        }

        public Customer Customer
        {
            get => _customer;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Customer));
                _customer = value;
            }
        }

        public List<OrderItem> OrderItems
        {
            get => _orderItems;
            private set => _orderItems = value;
        }

        public OrderStatus Status
        {
            get => _status;
            set => _status = value;
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set => _orderDate = value;
        }

        public decimal Subtotal
        {
            get => _subtotal;
            set => _subtotal = value;
        }

        public decimal Discount
        {
            get => _discount;
            set => _discount = value;
        }

        public decimal Tax
        {
            get => _tax;
            set => _tax = value;
        }

        public decimal Total
        {
            get => _total;
            set => _total = value;
        }

        public Order(int orderId, Customer customer, DateTime orderDate)
        {
            OrderId = orderId;
            Customer = customer;
            OrderDate = orderDate;
            _orderItems = new List<OrderItem>();
            _status = OrderStatus.Created;
        }

        /// <summary>
        /// Adds the specified item to the order.
        /// </summary>
      
        public void AddItem(OrderItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _orderItems.Add(item);
        }

        // Calculate totals
        public void CalculateTotals()
        {
            _subtotal = _orderItems.Sum(item => item.Subtotal);
            
            // Simple discount: 10% if subtotal > 1000
            _discount = _subtotal > 1000 ? _subtotal * 0.10m : 0;
            
            // Tax: 8% on (subtotal - discount)
            _tax = (_subtotal - _discount) * 0.08m;
            
            _total = _subtotal - _discount + _tax;
        }
    }
}
