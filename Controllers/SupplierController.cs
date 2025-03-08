using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
	public class SupplierController : Controller
	{
		private readonly ISupplier _supplier;

		public SupplierController(ISupplier supplier)
		{
			_supplier = supplier;
		}

        public IActionResult Index()
        {
			var suppliers = _supplier.GetAllSupplier();
            return View(suppliers);
        }

        public IActionResult Update(int id)
        {
            var product = _supplier.GetSupplierById(id);
            return View(product);
        }

        [HttpPost]
		public IActionResult Update(SupplierDTO supplier)
		{
            if (supplier.id == 0)
            {
                var addSupplier = _supplier.AddSupplier(supplier);
                if (addSupplier)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var updateSupplier = _supplier.UpdateSupplier(supplier);
                if (updateSupplier)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleteSupplier = _supplier.SoftDeleteSupplier(id);
            if (deleteSupplier)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Delete Supplier Failed");
        }
    }
}
