namespace UITraining.Models.DB
{
	public class Supplier
	{
		public int id { get; set; }
		public string NameSupplier { get; set; }
		public string SupplierAddress { get; set; }

		public ICollection<Product> Products { get; set; } = new List<Product>();
	}
}