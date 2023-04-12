using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;
using QuanLyBanSach.Models.Authentication;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authentication]
	public class NguoiDungsController : Controller
    {
        private readonly QlbanSachContext _context;
        public INotyfService _notyfService { get; }

        public NguoiDungsController(QlbanSachContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;

        }

        // GET: Admin/NguoiDungs
        public async Task<IActionResult> Index()
        {
              return _context.NguoiDungs != null ? 
                          View(await _context.NguoiDungs.ToListAsync()) :
                          Problem("Entity set 'QlbanSachContext.NguoiDungs'  is null.");
        }

        // GET: Admin/NguoiDungs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.NguoiDungs == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(m => m.MaNd == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNd,HoTenNd,Email,MatKhau,DienThoai,TaiKhoan,TrangThaiNd,Anh,NgayTao,DiaChi")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                if (_context.NguoiDungs.SingleOrDefault(nd => nd.TaiKhoan == nguoiDung.TaiKhoan) != null)
                {
                    _notyfService.Error("Tên tài khoản đã tồn tại");
                    return View(nguoiDung);
                }

                if (_context.NguoiDungs.SingleOrDefault(nd => nd.Email == nguoiDung.Email) != null)
                {
					_notyfService.Error("Email đã được sử dụng");
					return View(nguoiDung);
				}

				nguoiDung.NgayTao = DateTime.Now;
				nguoiDung.TrangThaiNd = "online";
				_context.Add(nguoiDung);
				await _context.SaveChangesAsync();
				_notyfService.Success("Tạo thành công");
				return RedirectToAction(nameof(Index));

			}
			return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.NguoiDungs == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNd,HoTenNd,Email,MatKhau,DienThoai,TaiKhoan,TrangThaiNd,Anh,NgayTao,DiaChi")] NguoiDung nguoiDung)
        {
            if (id != nguoiDung.MaNd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nguoiDung);
                    _notyfService.Success("Sửa thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiDungExists(nguoiDung.MaNd))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.NguoiDungs == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs
                .FirstOrDefaultAsync(m => m.MaNd == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NguoiDungs == null)
            {
                return Problem("Entity set 'QlbanSachContext.NguoiDungs'  is null.");
            }
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung != null)
            {
                _context.NguoiDungs.Remove(nguoiDung);
                _notyfService.Success("Xóa thành công");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiDungExists(int id)
        {
          return (_context.NguoiDungs?.Any(e => e.MaNd == id)).GetValueOrDefault();
        }

    }
}
