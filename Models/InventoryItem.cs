namespace ERPConnect.Models
{
    public class InventoryItem
    {
        public int InventoryItemId { get; set; }    
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Navigation property
        public Product Product { get; set; }
    }
}
