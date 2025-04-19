 namespace ERPConnect.Models
 {
     public class InventoryItem
     {
         public int InventoryItemId { get; set; }    
         public int ProductId { get; set; }
         public int Quantity { get; set; }

        // Navigation property (optional, so validation won't complain)
        public Product? Product { get; set; }
     }
 }
