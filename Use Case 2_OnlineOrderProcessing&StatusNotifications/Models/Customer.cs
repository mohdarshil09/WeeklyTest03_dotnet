namespace Use_Case_2_OnlineOrderProcessing_StatusNotifications.Models
{
    /// <summary>
    /// Represents a customer with identifying and contact information.
    /// </summary>
    /// <remarks>The Customer class encapsulates details such as customer ID, name, email address, and
    /// physical address. Instances of this class are typically used to store and transfer customer data within business
    /// applications. All properties must be set to valid values; see individual property documentation for
    /// constraints.</remarks>
    public class Customer
    {
        private int _customerId;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _address = string.Empty;

        public int CustomerId
        {
            get => _customerId;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Customer ID must be positive");
                _customerId = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Customer name cannot be empty");
                _name = value;
            }
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public Customer(int customerId, string name, string email, string address)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            Address = address;
        }
    }
}
