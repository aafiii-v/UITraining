using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Migrations;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
	public class UserAccessController : Controller
	{
		private readonly IUserAccess _userAccessInterface;
		private readonly ApplicationContext _context;

		public UserAccessController(IUserAccess useraccess, ApplicationContext context)
		{
			_userAccessInterface = useraccess;
			_context = context;
		}

		public IActionResult RegisterUser()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Index()
		{
			var data = _userAccessInterface.GetlistUser();
			return View(data);
		}

		public IActionResult EditUser(int id)
		{
			var data = _userAccessInterface.GetUserById(id);
			return View(data);
		}

		[HttpPost]
		public IActionResult EditUser(UserAccessDTO userAccessDTO)
		{
			var data = _userAccessInterface.EditUser(userAccessDTO);
			if (data)
			{
				return RedirectToAction(nameof(Index));
			}
			return View();
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var data = _userAccessInterface.DeleteUser(id);
			if (data)
			{
				return RedirectToAction(nameof(Index));
			}
			return BadRequest("Gagal menghapus User.");
		}

		[HttpPost]
		public IActionResult Login(UserAccessDTO loginDTO)
		{
			try
			{
				var datauser = _userAccessInterface.LoginValidation(loginDTO.Username, loginDTO.Password);
				if (datauser)
				{
					return RedirectToAction("Index", "Dashboard");
				}

				TempData["ErrorMessage"] = "Username atau Password salah!";
				return View(loginDTO);
			}
			catch (Exception)
			{
				TempData["ErrorMessage"] = "Terjadi kesalahan saat login.";
				return View(loginDTO);
			}
		}

		[HttpPost]
		public IActionResult RegisterUser(UserAccessDTO userAccessDTO)
		{
			if (userAccessDTO.Password.Length < 7)
			{
				TempData["ErrorMessage"] = "Password minimal 7 karakter";
				return View(userAccessDTO);
			}

			if (userAccessDTO.Password != userAccessDTO.MatchPassword)
			{
				TempData["ErrorMessage"] = "Password dan Konfirmasi Password harus sama!";
				return View(userAccessDTO);
			}

			var data = _context.UserAccesses
				.FirstOrDefault(x => x.Username == userAccessDTO.Username);
			if (data != null)
			{
				TempData["ErrorMessage"] = "Username sudah digunakan. Silakan pilih username lain.";
				return View(userAccessDTO);
			}

			try
			{
				var datauser = _userAccessInterface.InsertUserAccess(userAccessDTO);
				if (datauser)
				{
					TempData["SuccessMessage"] = "Registrasi berhasil! Silakan login.";
					return RedirectToAction("Login", "UserAccess");
				}

				TempData["ErrorMessage"] = "Gagal mendaftarkan user. Silakan coba lagi.";
				return View(userAccessDTO);
			}
			catch (Exception)
			{
				TempData["ErrorMessage"] = "Terjadi kesalahan saat mendaftarkan user.";
				return View(userAccessDTO);
			}
		}
	}
}