namespace Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models
{
    /// <summary>
    /// Represents a product with identifying information, pricing, category, and inventory details.
    /// </summary>
    /// <remarks>Use the Product class to model items available for sale or inventory management. The class
    /// provides properties for product identification, name, price, category, and stock quantity. Property setters
    /// validate input to ensure that product data remains consistent and meaningful. All properties must be set to
    /// valid values; for example, the product ID must be positive, the name cannot be empty, and price and stock
    /// quantity cannot be negative.</remarks>
    public class Product
    {
        private int _productId;
        private string _name = string.Empty;
        private decimal _price;
        private string _category = string.Empty;
        private int _stockQuantity;

        public int ProductId
        {
            get => _productId;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Product ID must be positive");
                _productId = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Product name cannot be empty");
                _name = value;
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative");
                _price = value;
            }
        }

        public string Category
        {
            get => _category;
            set => _category = value;
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Stock quantity cannot be negative");
                _stockQuantity = value;
            }
        }

        public Product(int productId, string name, decimal price, string category, int stockQuantity)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Category = category;
            StockQuantity = stockQuantity;
        }
    }
}
