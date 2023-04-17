using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietDonHangsController : Controller
    {
        private readonly QlbanSachContext _context;

        public ChiTietDonHangsController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: Admin/ChiTietDonHangs
        public async Task<IActionResult> Index()
        {
            var qlbanSachContext = _context.ChiTietDonHangs.Include(c => c.MaDonHangNavigation).Include(c => c.MaSachNavigation);
            return View(await qlbanSachContext.ToListAsync());
        }

        // GET: Admin/ChiTietDonHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChiTietDonHangs == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHangs
                .Include(c => c.MaDonHangNavigation)
                .Include(c => c.MaSachNavigation)
                .FirstOrDefaultAsync(m => m.MaCtdonHang == id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return View(chiTietDonHang);
        }

        // GET: Admin/ChiTietDonHangs/Create
        public IActionResult Create(int? id)
        {
            ViewData["MaDonHang"] = new SelectList(_context.DonHangs, "MaDonHang", "MaDonHang");
            ViewData["MaSach"] = new SelectList(_context.Saches, "MaSach", "TenSach");
            ViewBag.MaDonHang = id;
            return View();
        }

        // POST: Admin/ChiTietDonHangs/Creates
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCtdonHang,MaSach,SoLuong,GiamGia,ThanhTien,MaDonHang")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                var sach = _context.ChiTietDonHangs.FirstOrDefault(c => c.MaSach == chiTietDonHang.MaSach);
                if (sach != null)
                {
                    var ctdh = _context.ChiTietDonHangs.FirstOrDefault(c => c.MaSach == chiTietDonHang.MaSach);
                    ctdh.SoLuong += chiTietDonHang.SoLuong;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                _context.Add(chiTietDonHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["MaDonHang"] = new SelectList(_context.DonHangs, "MaDonHang", "MaDonHang", chiTietDonHang.MaDonHang);
            ViewData["MaSach"] = new SelectList(_context.Saches, "MaSach", "TenSach", chiTietDonHang.MaSach);
            return View(chiTietDonHang);
        }

        // GET: Admin/ChiTietDonHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChiTietDonHangs == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHangs.FindAsync(id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }
            ViewData["MaDonHang"] = new SelectList(_context.DonHangs, "MaDonHang", "GhiChu", chiTietDonHang.MaDonHang);
            ViewData["MaSach"] = new SelectList(_context.Saches, "MaSach", "MoTa", chiTietDonHang.MaSach);
            return View(chiTietDonHang);
        }

        // POST: Admin/ChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCtdonHang,MaSach,SoLuong,GiamGia,ThanhTien,MaDonHang")] ChiTietDonHang chiTietDonHang)
        {
            if (id != chiTietDonHang.MaCtdonHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietDonHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietDonHangExists(chiTietDonHang.MaCtdonHang))
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
            ViewData["MaDonHang"] = new SelectList(_context.DonHangs, "MaDonHang", "GhiChu", chiTietDonHang.MaDonHang);
            ViewData["MaSach"] = new SelectList(_context.Saches, "MaSach", "MoTa", chiTietDonHang.MaSach);
            return View(chiTietDonHang);
        }

        // GET: Admin/ChiTietDonHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChiTietDonHangs == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHangs
                .Include(c => c.MaDonHangNavigation)
                .Include(c => c.MaSachNavigation)
                .FirstOrDefaultAsync(m => m.MaCtdonHang == id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return View(chiTietDonHang);
        }

        // POST: Admin/ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChiTietDonHangs == null)
            {
                return Problem("Entity set 'QlbanSachContext.ChiTietDonHangs'  is null.");
            }
            var chiTietDonHang = await _context.ChiTietDonHangs.FindAsync(id);
            if (chiTietDonHang != null)
            {
                _context.ChiTietDonHangs.Remove(chiTietDonHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietDonHangExists(int id)
        {
          return (_context.ChiTietDonHangs?.Any(e => e.MaCtdonHang == id)).GetValueOrDefault();
        }
    }
}
