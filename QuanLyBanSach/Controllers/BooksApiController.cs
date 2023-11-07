using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
						   join nxb in db.NhaXuatBans on p.MaNxb equals nxb.MaNxb
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
                               MoTa = p.MoTa,
							   SoLuong = p.SoLuongCon,
							   NgayCapNhat = p.NgayCapNhat,
							   MaNxb = nxb.MaNxb,
							   TenNxb = nxb.TenNxb,
						   }).ToList();
			return sanPham;
		}
        [HttpGet("{maSach}")]
        public IEnumerable<Books> GetBooksByCategory(int maSach)
        {

			var sanPham = (from p in db.Saches
						   join a in db.TacGia on p.MaTg equals a.MaTg
						   join b in db.DanhMucSaches on p.MaDm equals b.MaDm
						   join nxb in db.NhaXuatBans on p.MaNxb equals nxb.MaNxb
						   where p.MaSach == maSach
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
							   NgayCapNhat = p.NgayCapNhat,
							   AnhTg = a.Anh,
							   SoLuong = p.SoLuongCon,
							   MaNxb = nxb.MaNxb,
							   MoTa = p.MoTa,
							   TenNxb = nxb.TenNxb,
						   }).ToList();
			return sanPham;
		}

		// POST: api/BooksApi
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Sach>> PostSach(Sach sach)
		{
			if (db.Saches == null)
			{
				return Problem("Entity set 'QlbanSachContext.Saches' is null.");
			}
			db.Saches.Add(sach);
			await db.SaveChangesAsync();

			return CreatedAtAction("GetSach", new { id = sach.MaSach }, sach);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutSach(int id, Sach sach)
		{
			if (id != sach.MaSach)
			{
				return BadRequest();
			}

			db.Entry(sach).State = EntityState.Modified;

			try
			{
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!SachExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSach(int id)
		{
			if (db.Saches == null)
			{
				return NotFound();
			}
			var sach = await db.Saches.FindAsync(id);
			if (sach == null)
			{
				return NotFound();
			}

			db.Saches.Remove(sach);
			await db.SaveChangesAsync();

			return NoContent();
		}
		private bool SachExists(int id)
		{
			return (db.Saches?.Any(e => e.MaSach == id)).GetValueOrDefault();
		}
	}
}
