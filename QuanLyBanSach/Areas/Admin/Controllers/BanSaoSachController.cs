using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;
using X.PagedList;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("BanSaoSach")]
	public class BanSaoSachController : Controller
	{
		QlbanSachContext db = new QlbanSachContext();

		[Route("")]
		[Route("Index")]
		public IActionResult Index()
		{
			return View();
        }

		[Route("DanhSachBanSao")]
		public IActionResult DanhSachBanSao(int? page)
		{
            var banSaoSachs = db.BanSaoSaches.AsNoTracking().ToList();

            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            int pageSize = 10;
            PagedList<BanSaoSach> lst = new PagedList<BanSaoSach>(banSaoSachs, pageNumber, pageSize);
            
			return View(lst);
        }

		[Route("Create")]
		[HttpGet]
		public IActionResult Create()
		{

			return View();
		}

		[Route("Edit")]
		public IActionResult Edit(int maBanSao)
		{
			var banSao = db.BanSaoSaches.Find(maBanSao);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach");

            return View(banSao);
		}
	}
}
