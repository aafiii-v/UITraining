using UITraining.Models.DB;

namespace UITraining.Interfaces
{
	public interface IProduct
	{
		public List<Product> GetAllProducts();
		public Product GetProductById(int id);
		public bool UpdateProduct(Product product);
		public bool DeleteData(int id);
    }
}