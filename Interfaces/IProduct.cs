using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
	public interface IProduct
	{
		public List<ProductDTO> GetAllProducts();
		public Product GetProductById(int id);
		public bool UpdateProduct(ProductDTO product);
		public bool AddProduct(ProductDTO product);
		public bool SoftDelete(int id);
	}
}