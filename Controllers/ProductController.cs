using AGB_Bank.Data;
using Microsoft.AspNetCore.Mvc;



namespace AGB_Bank.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult card()
        {
            var products = _context.carteProduct.ToList();
            return View(products);
        }

        public IActionResult Pack()
        {
            var products = _context.packProduct.ToList();
            return View(products);
        }

        public IActionResult Credit()
        {
            var products = _context.creditProduct.ToList();
            return View(products);
        }

    }

}


