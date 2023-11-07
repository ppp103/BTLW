using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;
using QuanLyBanSach.Models.BooksModels;

namespace QuanLyBanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly QlbanSachContext _context;

        public CategoryController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhMucSach>>> GetDanhMucSaches()
        {
          if (_context.DanhMucSaches == null)
          {
              return NotFound();
          }
            return await _context.DanhMucSaches.ToListAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public IEnumerable<Books> GetBooksByCategory(int id)
		{
			var sanPham = (from p in _context.Saches
						   join a in _context.TacGia on p.MaTg equals a.MaTg
						   join b in _context.DanhMucSaches on p.MaDm equals b.MaDm
						   where p.MaDm == id
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

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDanhMucSach(int id, DanhMucSach danhMucSach)
        {
            if (id != danhMucSach.MaDm)
            {
                return BadRequest();
            }

            _context.Entry(danhMucSach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhMucSachExists(id))
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

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DanhMucSach>> PostDanhMucSach(DanhMucSach danhMucSach)
        {
          if (_context.DanhMucSaches == null)
          {
              return Problem("Entity set 'QlbanSachContext.DanhMucSaches'  is null.");
          }
            _context.DanhMucSaches.Add(danhMucSach);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDanhMucSach", new { id = danhMucSach.MaDm }, danhMucSach);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhMucSach(int id)
        {
            if (_context.DanhMucSaches == null)
            {
                return NotFound();
            }
            var danhMucSach = await _context.DanhMucSaches.FindAsync(id);
            if (danhMucSach == null)
            {
                return NotFound();
            }

            _context.DanhMucSaches.Remove(danhMucSach);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DanhMucSachExists(int id)
        {
            return (_context.DanhMucSaches?.Any(e => e.MaDm == id)).GetValueOrDefault();
        }
    }
}
