using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Controllers
{
    public class AccessController : Controller
    {
        QlbanSachContext db = new QlbanSachContext();
		public INotyfService _notyfService { get; }

		public AccessController(QlbanSachContext context, INotyfService notyfService)
		{
			db = context;
			_notyfService = notyfService;
		}

		// Dang nhap
		[HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("TaiKhoan") == null)
            {
                return View();
            }
            else
            {
				return RedirectToAction("Index", "Home");
			}
        }

        [HttpPost]
        public IActionResult Login(NguoiDung nguoiDung)
        {
            if(HttpContext.Session.GetString("Admin") == null)
            {
                var admin = db.Admins.Where(x => x.TenDangNhap.Equals(nguoiDung.TaiKhoan) && 
                                            x.MatKhau.Equals(nguoiDung.MatKhau)).FirstOrDefault();
                if(admin != null)
                {
                    HttpContext.Session.SetString("Admin", admin.TenDangNhap.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }

            if (HttpContext.Session.GetString("TaiKhoan") == null)
            {
                var u = db.NguoiDungs.Where(x => x.TaiKhoan.Equals(nguoiDung.TaiKhoan)
                && x.MatKhau.Equals(nguoiDung.MatKhau)).FirstOrDefault();

                if (u != null)
                {
                    HttpContext.Session.SetString("TaiKhoan", u.TaiKhoan.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(nguoiDung);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Dang ky
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tài khoản đã tồn tại chưa
                var u = db.NguoiDungs.Where(x => x.TaiKhoan.Equals(nguoiDung.TaiKhoan)).FirstOrDefault();
                if (u != null)
                {
                    ModelState.AddModelError("TaiKhoan", "Tài khoản đã được sử dụng");
                    return View(nguoiDung);
                }

                // Thêm người dùng mới vào cơ sở dữ liệu
                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();

                // Đăng nhập người dùng mới đăng ký
                HttpContext.Session.SetString("TaiKhoan", nguoiDung.TaiKhoan);
                return RedirectToAction("Login", "Access");
            }

            return View(nguoiDung);
        }

        //Dang xuat
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TaiKhoan");
            return RedirectToAction("Login", "Access");
        }
    }
}
