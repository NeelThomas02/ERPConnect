using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPConnect.Data;
using ERPConnect.Models;
using System.Threading.Tasks;

namespace ERPConnect.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ERPConnectContext _context;
        public CustomersController(ERPConnectContext context)
            => _context = context;

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult Create()
            => View();

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            _context.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
