using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DTO
{
	public class SupplierDTO
	{
		public int id { get; set; }
		public string NameSupplier { get; set; }
		public string SupplierAddress { get; set; }
		public GeneralStatusData StatusSupplier { get; set; }
	}
}
