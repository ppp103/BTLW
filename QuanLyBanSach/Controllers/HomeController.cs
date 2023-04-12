using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Extension;
using QuanLyBanSach.Models;
using QuanLyBanSach.Models.Authentication;
using QuanLyBanSach.ModelViews;
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
			var sachbanchay = db.Saches.Where(x => x.MaDm == 1).ToList();
			ViewBag.sachbanchay = sachbanchay;

			var sachsapra = db.Saches.Where(x => x.MaDm == 3).ToList();
			ViewBag.sachsapra = sachsapra;

			var tensach = db.Saches.ToList();

			var tacgia = db.TacGia.ToList();
			ViewBag.tacgia = tacgia;

			var nxb = db.NhaXuatBans.ToList();
			ViewBag.nxb = nxb;

			var user = HttpContext.Session.GetString("TaiKhoan");
			ViewBag.user = user;
			var taiKhoan = db.NguoiDungs.SingleOrDefault(x => x.TaiKhoan == user);
			ViewBag.taiKhoan = taiKhoan;

			var adminAcc = HttpContext.Session.GetString("Admin");
			var admin = db.Admins.SingleOrDefault(x => x.TenDangNhap == adminAcc);
			ViewBag.admin = admin;

			return View(tensach);
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

            var user = HttpContext.Session.GetString("TaiKhoan");
            var taiKhoan = db.NguoiDungs.SingleOrDefault(x => x.TaiKhoan == user);
            ViewBag.taiKhoan = taiKhoan;

            return View(lst);

        }
		public IActionResult Contact() 
		{
			return View(); 
		}

        public IActionResult HienThiTheoDanhMuc(int madm)
		{
			var masach = db.Saches.Where(x => x.MaDm == madm).ToList();
			var matg = db.TacGia.ToList();
			var dm = db.DanhMucSaches.ToList();
			ViewBag.matg = matg;
			ViewBag.madm = dm;

            var user = HttpContext.Session.GetString("TaiKhoan");
            var taiKhoan = db.NguoiDungs.SingleOrDefault(x => x.TaiKhoan == user);
            ViewBag.taiKhoan = taiKhoan;

            return View(masach);
        }
        public IActionResult ChiTietSanPham(int sac)
		{
			var ctsach = db.Saches.SingleOrDefault(x => x.MaSach == sac);
			var matg = db.TacGia.ToList();
			var dm = db.DanhMucSaches.ToList();
			var masach = db.Saches.ToList();
			ViewBag.matg = matg;
			ViewBag.madm = dm;
			ViewBag.masach = masach;

            var user = HttpContext.Session.GetString("TaiKhoan");
            var taiKhoan = db.NguoiDungs.SingleOrDefault(x => x.TaiKhoan == user);
            ViewBag.taiKhoan = taiKhoan;

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
        public IActionResult Cart()
		{
			var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
			return View(cart);
		}
	}
}