using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPConnect.Data;
using ERPConnect.Models;
using System.Threading.Tasks;

namespace ERPConnect.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ERPConnectContext _context;
        public ProductsController(ERPConnectContext context)
            => _context = context;

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
            => View();

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
