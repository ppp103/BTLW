using Microsoft.AspNetCore.Mvc;
using QuanLyBanSach.Models;
using QuanLyBanSach.Models.Authentication;

namespace QuanLyBanSach.Controllers
{
    public class ContactController : Controller
    {
        QlbanSachContext db = new QlbanSachContext();
        public IActionResult Index()
		{
			var user = HttpContext.Session.GetString("TaiKhoan");
			var taiKhoan = db.NguoiDungs.SingleOrDefault(x => x.TaiKhoan == user);
			ViewBag.taiKhoan = taiKhoan;

			return View();
        }
    }
}
