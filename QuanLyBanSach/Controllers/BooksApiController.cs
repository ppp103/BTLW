using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanSach.Models;
using QuanLyBanSach.Models.BooksModels;

namespace QuanLyBanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksApiController : ControllerBase
    {
        QlbanSachContext db = new QlbanSachContext();
        [HttpGet]
        public IEnumerable<Books> GetAllBooks()
        {

			var sanPham = (from p in db.Saches
						   join a in db.TacGia on p.MaTg equals a.MaTg
						   join b in db.DanhMucSaches on p.MaDm equals b.MaDm
						   select new Books
						   {
							   MaDm = b.MaDm,
							   TenDm = b.TenDm,
							   MaTg = a.MaTg,
							   TenTg = a.TenTg,
							   MaSach = p.MaSach,
							   TenSach = p.TenSach,
							   GiaBan = p.GiaBan,
							   Anh = p.Anh,
							  

						   }).ToList();
			return sanPham;
		}
        [HttpGet("{madm}")]
        public IEnumerable<Books> GetBooksByCategory(string madm)
        {

			var sanPham = (from p in db.Saches
						   join a in db.TacGia on p.MaTg equals a.MaTg
						   join b in db.DanhMucSaches on p.MaDm equals b.MaDm
						   where p.MaDm == madm
						   select new Books
						   {
							   MaDm = b.MaDm,
							   TenDm = b.TenDm,
							   MaTg = a.MaTg,
							   TenTg = a.TenTg,
							   MaSach = p.MaSach,
							   TenSach = p.TenSach,
							   GiaBan = p.GiaBan,
							   Anh = p.Anh,
						   }).ToList();
			return sanPham;
		}
		

	}
}
