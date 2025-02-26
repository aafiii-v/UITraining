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

		public Product GetProductById(int id)
		{
			var product = _context.Products.Where(x => x.Id == id && x.ProductStatus != ProductStatus.deleted).FirstOrDefault();

			if (product == null)
			{
				return new Product();
			}

			return product;
		}

		public bool UpdateProduct(Product product)
		{
			var data = _context.Products.FirstOrDefault(x => x.Id == product.Id);
			if (data == null)
			{
				return false;
			}

			data.Name = product.Name;
			data.Description = product.Description;
			data.Stoct = product.Stoct;
			data.Price = product.Price;
			data.ProductStatus = product.ProductStatus;

			_context.Products.Update(data);
			_context.SaveChanges();

			return true;
		}
	}
}