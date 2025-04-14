using Microsoft.EntityFrameworkCore;
using ERPConnect.Models;

namespace ERPConnect.Data
{
    public class ERPConnectContext : DbContext
    {
        public ERPConnectContext(DbContextOptions<ERPConnectContext> options)
            : base(options)
        { }

        // Inventory Module
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Product> Products { get; set; }

        // Billing Module
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
