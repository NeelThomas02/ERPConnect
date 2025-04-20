using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPConnect.Data;
using ERPConnect.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ERPConnect.Services;

namespace ERPConnect.Controllers
{
    public class BillingController : Controller
    {
        private readonly ERPConnectContext _context;
        private readonly IPdfService _pdfService;

        // Dependency injection: the DbContext is provided via the controller's constructor.
        public BillingController(ERPConnectContext context, IPdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        // GET: Billing/DownloadPdf/5
        [HttpGet]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            // Load the invoice and its customer
            var invoice = await _context.Invoices
                                        .Include(i => i.Customer)
                                        .FirstOrDefaultAsync(i => i.InvoiceId == id);
            if (invoice == null) 
                return NotFound();

            var pdfBytes = _pdfService.GenerateInvoicePdf(invoice);
            // Return a FileResult to trigger download
            return File(pdfBytes, "application/pdf", $"Invoice_{id}.pdf");
        }

        // GET: Billing/Index
        // Retrieves a list of invoices, eagerly loading the associated Customer for each invoice.
        public async Task<IActionResult> Index(string searchString)
        {
            var invoicesQuery = _context.Invoices
                                    .Include(i => i.Customer)
                                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                invoicesQuery = invoicesQuery
                    .Where(i => i.Customer.Name.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            var invoices = await invoicesQuery.ToListAsync();
            Console.WriteLine($"[Billing][Index] Retrieved {invoices.Count} invoices");
            return View(invoices);
        }

        // GET: Billing/Create
        public IActionResult Create()
        {
            var allCustomers = _context.Customers.AsNoTracking().ToList();
            Console.WriteLine($"[Billing][Create GET] Customers in DB: {allCustomers.Count}");
            ViewData["CustomerList"] = new SelectList(
                _context.Customers.ToList(),
                "CustomerId",
                "Name"
            );
            return View();
        }

        // POST: Billing/Create
        // Processes the form data and saves the new invoice to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            Console.WriteLine($"[Billing][Create POST] Received CustomerId={invoice.CustomerId}, Date={invoice.InvoiceDate}, Amount={invoice.TotalAmount}");
            // Always repopulate dropdown before checking ModelState
            ViewData["CustomerList"] = new SelectList(
                 _context.Customers.ToList(),
                 "CustomerId",
                 "Name",
                 invoice.CustomerId
             );

            if (!ModelState.IsValid)
            {
                Console.WriteLine("[Billing][Create POST] ModelState invalid:");
                foreach (var err in ModelState.Values.SelectMany(v => v.Errors))
                    Console.WriteLine("  • " + err.ErrorMessage);
                // Log or inspect errors (or even display them temporarily)
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(invoice);
            }

            _context.Add(invoice);
            await _context.SaveChangesAsync();
            Console.WriteLine($"[Billing][Create POST] Saved invoice ID {invoice.InvoiceId}");
            return RedirectToAction(nameof(Index));
        }

        // (Optional) Additional actions such as Edit, Details, and Delete can be added here.
    }
}
