using Microsoft.AspNetCore.Mvc;

namespace QuanLyBanSach.Controllers
{
	public class AjaxController : Controller
	{
		public IActionResult HeaderCart()
		{
			return ViewComponent("HeaderCart");
		}
		public IActionResult HeaderFavourites()
		{
			return ViewComponent("NumberCart");
		}
	}
}
