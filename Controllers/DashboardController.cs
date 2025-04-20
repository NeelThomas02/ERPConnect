using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPConnect.Data;
using ERPConnect.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ERPConnect.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ERPConnectContext _context;
        public DashboardController(ERPConnectContext context)
            => _context = context;

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel
            {
                TotalProducts = await _context.Products.CountAsync(),
                TotalInventoryRecords = await _context.InventoryItems.CountAsync(),
                TotalInventoryQuantity = await _context.InventoryItems.SumAsync(i => i.Quantity),
                TotalCustomers = await _context.Customers.CountAsync(),
                TotalInvoices = await _context.Invoices.CountAsync(),
                RecentInvoices = await _context.Invoices
                    .Include(i => i.Customer)
                    .OrderByDescending(i => i.InvoiceDate)
                    .Take(5)
                    .ToListAsync(),
                RecentInventoryItems = await _context.InventoryItems
                    .Include(i => i.Product)
                    .OrderByDescending(i => i.InventoryItemId)
                    .Take(5)
                    .ToListAsync()
            };

            return View(vm);
        }
    }
}
