using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPConnect.Data;
using ERPConnect.Models;
using System.Threading.Tasks;

namespace ERPConnect.Controllers
{
    public class BillingController : Controller
    {
        private readonly ERPConnectContext _context;

        // Dependency injection: the DbContext is provided via the controller's constructor.
        public BillingController(ERPConnectContext context)
        {
            _context = context;
        }

        // GET: Billing/Index
        // Retrieves a list of invoices, eagerly loading the associated Customer for each invoice.
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices
                                         .Include(i => i.Customer)
                                         .ToListAsync();
            return View(invoices);
        }

        // GET: Billing/Create
        // Presents a form for creating a new invoice.
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billing/Create
        // Processes the form data and saves the new invoice to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                // Log or inspect errors (or even display them temporarily)
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(invoice);
            }

            _context.Add(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // (Optional) Additional actions such as Edit, Details, and Delete can be added here.
    }
}
