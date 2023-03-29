using Microsoft.AspNetCore.Mvc;
using QuanLyBanSach.Extension;
using QuanLyBanSach.ModelViews;

namespace QuanLyBanSach.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(cart);
        }

    }
}
