using Microsoft.AspNetCore.Identity;
using UITraining.Helper;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Services
{
	public class UserAccessServices : IUserAccess
	{
		private readonly ApplicationContext _context;
		private readonly string _pepper;
		private readonly string _iteration;

		public UserAccessServices(ApplicationContext context, IConfiguration configuration)
		{
			_pepper = configuration.GetSection("Security:Pepper").Value ?? "";
			_iteration = configuration.GetSection("Security:Iteration").Value ?? "";
			_context = context;
		}

		public bool InsertUserAccess(UserAccessDTO dto)
		{

			var GenerateSalt = Helper.Hasher.GenerateSalt();

			var user = new UserAccess
			{
				Name = dto.Name,
				Username = dto.Username,
				Password = dto.Password,
				AccessDate = DateTime.Now,
				Salt = GenerateSalt,
				UserStatus = GeneralStatus.GeneralStatusData.published,
				PasswordHash = Hasher.ComputeHash(dto.Password, GenerateSalt, _pepper, Convert.ToInt32(_iteration))
			};

			_context.Add(user);
			_context.SaveChanges();

			return true;
		}

		public UserAccess GetUserById(int id)
		{
			var data = _context.UserAccesses.Where(x => x.Id == id && x.UserStatus != GeneralStatusData.deleted).FirstOrDefault();
			if (data == null)
			{
				return new UserAccess();
			}

			return data;
		}

		public List<UserAccessDTO> GetlistUser()
		{
			var data = _context.UserAccesses.Where(x => x.UserStatus != GeneralStatusData.deleted).Select(x => new UserAccessDTO
			{
				Id = x.Id,
				Name = x.Name,
				Username = x.Username,
				Password = x.Password,
				MatchPassword = x.Password,
				UserStatus = x.UserStatus

			}).ToList();
			return data;
		}

		public bool EditUser(UserAccessDTO userAccessDTO)
		{
			var data = _context.UserAccesses.FirstOrDefault(x => x.Id == userAccessDTO.Id);
			if (data == null)
			{
				return false;
			}

			data.AccessDate = DateTime.Now;
			data.UserStatus = userAccessDTO.UserStatus;

			_context.UserAccesses.Update(data);
			_context.SaveChanges();
			return true;
		}

		public bool DeleteUser(int id)
		{
			var data = _context.UserAccesses.FirstOrDefault(x => x.Id == id);
			if (data == null)
			{
				return false;
			}

			data.UserStatus = GeneralStatusData.deleted;
			_context.SaveChanges();
			return true;
		}

		public bool LoginValidation(string username, string password)
		{
			var user = _context.UserAccesses
				.FirstOrDefault(x => 
				x.Username == username && 
				x.UserStatus == GeneralStatusData.published);

			if (user == null)
            {
                return false;
            }

			var PasswordHash = Hasher.ComputeHash(password, user.Salt, _pepper, Convert.ToInt32(_iteration));
			
            if (username == user.Username && user.PasswordHash == PasswordHash)
			{
				return true;
			}

			return false;
		}
	}
}
