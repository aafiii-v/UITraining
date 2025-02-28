using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;

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

        public IActionResult Update(int id)
        {
            var product = _interface.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            var updateProduct = _interface.UpdateProduct(product);
            if (updateProduct)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleteProduct = _interface.SoftDelete(id);
            if (deleteProduct)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Delete Product Failed!");
        }
    }
}