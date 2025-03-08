using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
	public interface ISupplier
	{
        public List<SelectListItem> Suppliers();
        public Supplier GetSupplierById(int id);
        public List<SupplierDTO> GetAllSupplier();
        public bool AddSupplier(SupplierDTO supplier);
        public bool UpdateSupplier(SupplierDTO supplier);
        public bool SoftDeleteSupplier(int id);
    }
}
