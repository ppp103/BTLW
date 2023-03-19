using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;
using X.PagedList;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("NguoiDung")]
    public class NguoiDungController : Controller
    {
        QlbanSachContext db = new QlbanSachContext();

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

		[Route("DanhSachNguoiDung")]
        public IActionResult DanhSachNguoiDung(int? page)
        {
            var nguoiDungs = db.NguoiDungs.AsNoTracking().ToList();

            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            int pageSize = 10;
            PagedList<NguoiDung> lst = new PagedList<NguoiDung>(nguoiDungs, pageNumber, pageSize);

            return View(lst);
        }
    }
}
