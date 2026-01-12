using Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models;
namespace Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models
{
    /// <summary>
    /// Represents a single item within an order, including the associated product, quantity, and unit price.
    /// </summary>
    /// <remarks>An OrderItem encapsulates the details of a product purchased as part of an order. It provides
    /// access to the product information, the quantity ordered, and the price per unit at the time of purchase. The
    /// Subtotal property calculates the total cost for this item based on the quantity and unit price. This class is
    /// typically used as part of an order management or e-commerce system to track individual line items.</remarks>
    public class OrderItem
    {
        private Product _product = null!;
        private int _quantity;
        private decimal _unitPrice;
        public Product Product
        {
            get => _product;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Product));
                _product = value;
            }
        }
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Quantity must be positive");
                _quantity = value;
            }
        }

        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Unit price cannot be negative");
                _unitPrice = value;
            }
        }

        public decimal Subtotal => Quantity * UnitPrice;

        public OrderItem(Product product, int quantity, decimal unitPrice)
        {
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}