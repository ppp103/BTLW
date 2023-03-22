using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;
using System.Diagnostics;
using System.Linq;
using X.PagedList;

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
			return View();
		}
		public IActionResult HienThiSanPham(int? page)
		{
			var masach = db.Saches.ToList();
            var matg = db.TacGia.ToList();
            var madm = db.DanhMucSaches.ToList();
			int pageNumber = page == null || page < 1 ? 1 : page.Value;
			int pageSize = 8;
			var masach1 = db.Saches.ToList().OrderBy(x => x.TenSach);
			PagedList<Sach> lst = new PagedList<Sach>(masach1, pageNumber, pageSize);
			ViewBag.masach = masach;
			ViewBag.matg = matg;
            ViewBag.madm = madm;
            return View(lst);

        }
		
		public IActionResult HienThiTheoDanhMuc(string madm)
		{
			var masach = db.Saches.Where(x => x.MaDm == madm).ToList();
			var matg = db.TacGia.ToList();
			var dm = db.DanhMucSaches.ToList();
			ViewBag.matg = matg;
			ViewBag.madm = dm;
			return View(masach);
        }
		public IActionResult ChiTietSanPham(string sac)
		{
			var ctsach = db.Saches.SingleOrDefault(x => x.MaSach == sac);
			var matg = db.TacGia.ToList();
			var dm = db.DanhMucSaches.ToList();
			var masach = db.Saches.ToList();
			ViewBag.matg = matg;
			ViewBag.madm = dm;
			ViewBag.masach = masach;
			return View(ctsach);
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

		public IActionResult Login()
		{
			return View();
		}

		public IActionResult SignUp()
		{
			return View();
		}

		public IActionResult Cart()
		{
			return View();
		}
	}
}