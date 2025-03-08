using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        private readonly ISupplier _supplier;

        public ProductController(IProduct interfaces, ISupplier supplier)
        {
            _product = interfaces;
            _supplier = supplier;
        }
        public IActionResult Index()
        {
            var products = _product.GetAllProducts();
            return View(products);
        }

        public IActionResult Update(int id)
        {
            ViewBag.Supplier = _supplier.Suppliers();
            var product = _product.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(ProductDTO product)
        {
            if (product.Id == 0)
            {
                var addProduct = _product.AddProduct(product);
                if (addProduct)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var updateProduct = _product.UpdateProduct(product);
                if (updateProduct)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleteProduct = _product.SoftDeleteProduct(id);
            if (deleteProduct)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Delete Product Failed!");
        }
    }
}