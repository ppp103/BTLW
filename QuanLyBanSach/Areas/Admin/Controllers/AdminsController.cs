using Microsoft.AspNetCore.Mvc;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    public class AdminsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
