using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanSach.Extension;
using QuanLyBanSach.Models;
using QuanLyBanSach.ModelViews;

namespace QuanLyBanSach.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly QlbanSachContext _context;
        public INotyfService _notyfService { get; }

        public ShoppingCartController(QlbanSachContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        public List<CartItem> GioHang
        {
            get
            {
                var gioHang = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gioHang == default(List<CartItem>))
                {
                    gioHang = new List<CartItem>();
                }

                return gioHang;
            }
        }

        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int maSach, int? soLuong)
        {
            List<CartItem> cart = GioHang;

            try
            {
                // tìm sản phẩm để thêm
                CartItem item = cart.SingleOrDefault(s => s.sach.MaSach == maSach);

                // đã có trong giỏ -> cộng thêm số lượng
                if (item != null) 
                {
                    item.soLuong = item.soLuong + soLuong.Value;
                    // update session  
                    HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                }
                else // chưa có -> tạo 1 cartItem mới
                {
                    Sach s = _context.Saches.SingleOrDefault(s => s.MaSach == maSach);

                    item = new CartItem
                    {
                        soLuong = soLuong.HasValue ? soLuong.Value : 1,
                        sach = s
                    };
                    cart.Add(item); //Thêm vào giỏ
                }

                // update session
                HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("api/cart/update")]
        public IActionResult UpdateCart(int maSach, int? soLuong)
        {
            //Lay gio hang ra de xu ly
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            try
            {
                if (cart != null)
                {
                    CartItem item = cart.SingleOrDefault(s => s.sach.MaSach == maSach);
                    if (item != null && soLuong.HasValue) // da co -> cap nhat so luong
                    {
                        item.soLuong = soLuong.Value;
                    }
					//Luu lai session
					HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                }
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("api/cart/remove")]
        public ActionResult Remove(int maSach)
        {

            try
            {
                List<CartItem> gioHang = GioHang;
                CartItem item = gioHang.SingleOrDefault(s => s.sach.MaSach == maSach);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                //luu lai session
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("TaiKhoan");
            var taiKhoan = _context.NguoiDungs.SingleOrDefault(x => x.TaiKhoan == user);
            ViewBag.taiKhoan = taiKhoan;

            return View(GioHang);
        }
    }
}
