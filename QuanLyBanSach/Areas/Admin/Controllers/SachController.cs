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
    public class SachController : Controller
    {
        private readonly QlbanSachContext _context;

        public SachController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: Admin/Sach
        public async Task<IActionResult> Index()
        {
            var qlbanSachContext = _context.Saches.Include(s => s.MaDmNavigation).Include(s => s.MaNxbNavigation).Include(s => s.MaTgNavigation);
            return View(await qlbanSachContext.ToListAsync());
        }

        // GET: Admin/Sach/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Saches == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.MaDmNavigation)
                .Include(s => s.MaNxbNavigation)
                .Include(s => s.MaTgNavigation)
                .FirstOrDefaultAsync(m => m.MaSach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // GET: Admin/Sach/Create
        public IActionResult Create()
        {
            ViewData["MaDm"] = new SelectList(_context.DanhMucSaches, "MaDm", "MaDm");
            ViewData["MaNxb"] = new SelectList(_context.NhaXuatBans, "MaNxb", "MaNxb");
            ViewData["MaTg"] = new SelectList(_context.TacGia, "MaTg", "MaTg");
            return View();
        }

        // POST: Admin/Sach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSach,TenSach,GiaBan,MoTa,SoLuongBs,Anh,NgayCapNhat,SoLuongCon,MaNxb,MaTg,MaDm")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDm"] = new SelectList(_context.DanhMucSaches, "MaDm", "MaDm", sach.MaDm);
            ViewData["MaNxb"] = new SelectList(_context.NhaXuatBans, "MaNxb", "MaNxb", sach.MaNxb);
            ViewData["MaTg"] = new SelectList(_context.TacGia, "MaTg", "MaTg", sach.MaTg);
            return View(sach);
        }

        // GET: Admin/Sach/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Saches == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            ViewData["MaDm"] = new SelectList(_context.DanhMucSaches, "MaDm", "MaDm", sach.MaDm);
            ViewData["MaNxb"] = new SelectList(_context.NhaXuatBans, "MaNxb", "MaNxb", sach.MaNxb);
            ViewData["MaTg"] = new SelectList(_context.TacGia, "MaTg", "MaTg", sach.MaTg);
            return View(sach);
        }

        // POST: Admin/Sach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSach,TenSach,GiaBan,MoTa,SoLuongBs,Anh,NgayCapNhat,SoLuongCon,MaNxb,MaTg,MaDm")] Sach sach)
        {
            if (id != sach.MaSach)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.MaSach))
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
            ViewData["MaDm"] = new SelectList(_context.DanhMucSaches, "MaDm", "MaDm", sach.MaDm);
            ViewData["MaNxb"] = new SelectList(_context.NhaXuatBans, "MaNxb", "MaNxb", sach.MaNxb);
            ViewData["MaTg"] = new SelectList(_context.TacGia, "MaTg", "MaTg", sach.MaTg);
            return View(sach);
        }

        // GET: Admin/Sach/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Saches == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.MaDmNavigation)
                .Include(s => s.MaNxbNavigation)
                .Include(s => s.MaTgNavigation)
                .FirstOrDefaultAsync(m => m.MaSach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Admin/Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Saches == null)
            {
                return Problem("Entity set 'QlbanSachContext.Saches'  is null.");
            }
            var sach = await _context.Saches.FindAsync(id);
            if (sach != null)
            {
                _context.Saches.Remove(sach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(string id)
        {
          return (_context.Saches?.Any(e => e.MaSach == id)).GetValueOrDefault();
        }
    }
}
