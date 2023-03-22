using Microsoft.AspNetCore.Mvc;
using QuanLyBanSach.Models;
using System.Diagnostics;

namespace QuanLyBanSach.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		QlbanSachContext db = new QlbanSachContext();
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			var tensach = db.Saches.ToList();
			var tacgia = db.TacGia.ToList();
			ViewBag.tacgia = tacgia;
			var nxb = db.NhaXuatBans.ToList();
			ViewBag.nxb = nxb;
			return View(tensach);
		}
	
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}