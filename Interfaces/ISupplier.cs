using Microsoft.AspNetCore.Mvc.Rendering;

namespace UITraining.Interfaces
{
	public interface ISupplier
	{
        public List<SelectListItem> Suppliers();
    }
}
