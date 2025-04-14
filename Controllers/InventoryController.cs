using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPConnect.Data;
using ERPConnect.Models;
using System.Threading.Tasks;

namespace ERPConnect.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ERPConnectContext _context;

        // Constructor: Dependency injection provides the DbContext.
        public InventoryController(ERPConnectContext context)
        {
            _context = context;
        }

        // GET: Inventory/Index
        // Retrieves the list of inventory items including each associated Product.
        public async Task<IActionResult> Index()
        {
            var inventoryItems = await _context.InventoryItems
                                               .Include(i => i.Product)
                                               .ToListAsync();
            return View(inventoryItems);
        }

        // GET: Inventory/Create
        // Returns the view to create a new InventoryItem.
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        // Processes form submission to add a new InventoryItem.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryItem item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // Additional methods for Edit, Details, and Delete can be added here.
    }
}
