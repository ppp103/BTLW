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
    public class NhaXuatBansController : Controller
    {
        private readonly QlbanSachContext _context;

        public NhaXuatBansController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: Admin/NhaXuatBans
        public async Task<IActionResult> Index()
        {
              return _context.NhaXuatBans != null ? 
                          View(await _context.NhaXuatBans.ToListAsync()) :
                          Problem("Entity set 'QlbanSachContext.NhaXuatBans'  is null.");
        }

        // GET: Admin/NhaXuatBans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NhaXuatBans == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _context.NhaXuatBans
                .FirstOrDefaultAsync(m => m.MaNxb == id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return View(nhaXuatBan);
        }

        // GET: Admin/NhaXuatBans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaXuatBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNxb,TenNxb")] NhaXuatBan nhaXuatBan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhaXuatBan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaXuatBan);
        }

        // GET: Admin/NhaXuatBans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.NhaXuatBans == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _context.NhaXuatBans.FindAsync(id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }
            return View(nhaXuatBan);
        }

        // POST: Admin/NhaXuatBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNxb,TenNxb")] NhaXuatBan nhaXuatBan)
        {
            if (id != nhaXuatBan.MaNxb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhaXuatBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhaXuatBanExists(nhaXuatBan.MaNxb))
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
            return View(nhaXuatBan);
        }

        // GET: Admin/NhaXuatBans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.NhaXuatBans == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _context.NhaXuatBans
                .FirstOrDefaultAsync(m => m.MaNxb == id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return View(nhaXuatBan);
        }

        // POST: Admin/NhaXuatBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.NhaXuatBans == null)
            {
                return Problem("Entity set 'QlbanSachContext.NhaXuatBans'  is null.");
            }
            var nhaXuatBan = await _context.NhaXuatBans.FindAsync(id);
            if (nhaXuatBan != null)
            {
                _context.NhaXuatBans.Remove(nhaXuatBan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhaXuatBanExists(string id)
        {
          return (_context.NhaXuatBans?.Any(e => e.MaNxb == id)).GetValueOrDefault();
        }
    }
}
