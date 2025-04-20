using System.Collections.Generic;

namespace ERPConnect.Models
{
    public class DashboardViewModel
    {
        // Totals
        public int TotalProducts { get; set; }
        public int TotalInventoryRecords { get; set; }
        public int TotalInventoryQuantity { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalInvoices { get; set; }

        // Recent activity
        public List<Invoice> RecentInvoices { get; set; } = new();
        public List<InventoryItem> RecentInventoryItems { get; set; } = new();
    }
}
