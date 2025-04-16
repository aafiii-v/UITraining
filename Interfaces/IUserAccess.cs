using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
	public interface IUserAccess
	{
		public bool InsertUserAccess(UserAccessDTO dto);

		public UserAccess GetUserById(int id);

		public List<UserAccessDTO> GetlistUser();

		public bool EditUser(UserAccessDTO userAccessDTO);

		public bool DeleteUser(int id);

		public bool LoginValidation(string username, string password);
	}
}
