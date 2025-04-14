using System;

namespace ERPConnect.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }  // Primary key
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation property (ensure you have a Customer class defined)
        public Customer Customer { get; set; }
    }
}
