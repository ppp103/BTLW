using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;
using QuanLyBanSach.Models.Authentication;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Authentication]
    [Area("Admin")]
	public class TacGiasController : Controller
    {
        private readonly QlbanSachContext _context;

        public TacGiasController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: Admin/TacGias
        public async Task<IActionResult> Index()
        {
              return _context.TacGia != null ? 
                          View(await _context.TacGia.ToListAsync()) :
                          Problem("Entity set 'QlbanSachContext.TacGia'  is null.");
        }

        // GET: Admin/TacGias/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.TacGia == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGia
                .FirstOrDefaultAsync(m => m.MaTg == id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        // GET: Admin/TacGias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TacGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTg,TenTg")] TacGia tacGia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tacGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tacGia);
        }

        // GET: Admin/TacGias/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.TacGia == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGia.FindAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }
            return View(tacGia);
        }

        // POST: Admin/TacGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTg,TenTg")] TacGia tacGia)
        {
            if (id != tacGia.MaTg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tacGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TacGiaExists(tacGia.MaTg))
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
            return View(tacGia);
        }

        // GET: Admin/TacGias/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.TacGia == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGia
                .FirstOrDefaultAsync(m => m.MaTg == id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        // POST: Admin/TacGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TacGia == null)
            {
                return Problem("Entity set 'QlbanSachContext.TacGia'  is null.");
            }
            var tacGia = await _context.TacGia.FindAsync(id);
            if (tacGia != null)
            {
                _context.TacGia.Remove(tacGia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TacGiaExists(int id)
        {
          return (_context.TacGia?.Any(e => e.MaTg == id)).GetValueOrDefault();
        }
    }
}
