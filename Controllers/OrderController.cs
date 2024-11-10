using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ST10028058_CLDV6212_POE_Final.Data;
using ST10028058_CLDV6212_POE_Final.Models;

namespace ST10028058_CLDV6212_POE_Final.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ApplicationDbContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // List all orders
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Fetching all orders...");
            var orders = await _context.Orders
                .Include(o => o.Product)
                .ToListAsync();
            _logger.LogInformation("Orders fetched successfully.");
            return View(orders);
        }

        // Show create order form
        public IActionResult Create()
        {
            _logger.LogInformation("Preparing to create a new order...");

            // Fetch products and populate ViewBag.ProductList as a SelectList
            var products = _context.Products.ToList();
            ViewBag.ProductList = new SelectList(products, "Product_Id", "Product_Name");

            // Check for empty product list
            if (products.Count == 0)
            {
                _logger.LogWarning("No products found.");
                ModelState.AddModelError("", "No products found. Please add products before creating an order.");
            }

            _logger.LogInformation("Create order form is ready.");
            return View();
        }

        // Process the creation of an order
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            _logger.LogInformation("Creating a new order...");
            _logger.LogInformation($"Selected ProductId: {order.ProductId}");

            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                _context.Add(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order created successfully.");
                return RedirectToAction(nameof(Index));
            }

            // Log validation errors
            _logger.LogWarning("Model validation failed.");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError($"Validation error: {error.ErrorMessage}");
            }

            // Reload ProductList in case of validation failure
            ViewBag.ProductList = new SelectList(_context.Products.ToList(), "Product_Id", "Product_Name");

            return View(order);
        }

        // Delete an order
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Order deletion failed. No OrderId provided.");
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                _logger.LogWarning($"Order with id {id} not found.");
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation($"Deleting order with id {id}...");
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                _logger.LogWarning($"Order with id {id} not found.");
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Order with id {id} deleted successfully.");
            return RedirectToAction(nameof(Index));
        }
    }
}
