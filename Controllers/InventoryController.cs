using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;      // ← for SelectList
using Microsoft.EntityFrameworkCore;
using ERPConnect.Data;
using ERPConnect.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ERPConnect.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ERPConnectContext _context;

        public InventoryController(ERPConnectContext context)
        {
            _context = context;
        }

        // GET: Inventory/Index
        public async Task<IActionResult> Index()
        {
            var items = await _context.InventoryItems
                                      .Include(i => i.Product)
                                      .ToListAsync();
            Console.WriteLine($"[Inventory][Index] Retrieved {items.Count} items");
            return View(items);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            // Fetch products, group by Name, and select one representative per group
            var distinctProducts = _context.Products
                .AsNoTracking()
                .GroupBy(p => p.Name)
                .Select(g => g.First())
                .ToList();

            ViewData["ProductList"] = new SelectList(
                distinctProducts,
                "ProductId",
                "Name"
            );

            Console.WriteLine("[Inventory][Create GET] Loaded distinct ProductList");
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryItem item)
        {
            Console.WriteLine($"[Inventory][Create POST] Received ProductId={item.ProductId}, Quantity={item.Quantity}");

            if (!ModelState.IsValid)
            {
                // Reload the distinct list
                var distinctProducts = _context.Products
                    .AsNoTracking()
                    .GroupBy(p => p.Name)
                    .Select(g => g.First())
                    .ToList();

                ViewData["ProductList"] = new SelectList(
                    distinctProducts,
                    "ProductId",
                    "Name",
                    item.ProductId
                );

                return View(item);
            }

            _context.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
