using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Interfaces;
using UITraining.Models;

namespace UITraining.Services
{
	public class SupplierServices : ISupplier
	{
		private readonly ApplicationContext _context;

		public SupplierServices(ApplicationContext context)
		{
			_context = context;
		}

		public List<SelectListItem> Suppliers()
		{
			var datas = _context.Suppliers
				.Select(x => new SelectListItem
				{
					Text = x.NameSupplier,
					Value = x.id.ToString()
				}).ToList();
			return datas;
		}
	}
}