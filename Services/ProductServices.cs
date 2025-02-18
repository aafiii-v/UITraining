using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;

namespace UITraining.Services
{
	public class ProductServices : IProduct
	{
		private readonly ApplicationContext _context;

		public ProductServices(ApplicationContext context)
		{
			_context = context;
		}

		public List<Product> GetAllProducts()
		{
			var products = _context.Products.Where(x => x.ProductStatus != ProductStatus.deleted).ToList();
			return products;
		}
	}
}