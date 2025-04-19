using System;

namespace ERPConnect.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Make this nullable so the binder won’t require it
        public Customer? Customer { get; set; }
    }
}
