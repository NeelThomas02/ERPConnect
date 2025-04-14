namespace ERPConnect.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }  // Primary key for the Customer
        public string Name { get; set; }
        public string Email { get; set; }
        // Add other customer-related properties here if needed.
    }
}
