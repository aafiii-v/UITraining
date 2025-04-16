using static UITraining.Models.GeneralStatus;
using System.ComponentModel.DataAnnotations;

namespace UITraining.Models.DTO
{
    public class UserAccessDTO
    {
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Username { get; set; }
		public string Password { get; set; }
		public string MatchPassword { get; set; }
		public GeneralStatusData UserStatus { get; set; }
	}
}
