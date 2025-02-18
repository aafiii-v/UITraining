using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;

namespace UITraining.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _interface;

        public ProductController(IProduct interfaces)
        {
            _interface = interfaces;
        }
        public IActionResult Index()
        {
            var products = _interface.GetAllProducts();
            return View(products);
        }
    }
}
