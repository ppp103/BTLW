using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;
using X.PagedList;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BanSaoController : Controller
    {
        private readonly QlbanSachContext _context;
        public INotyfService _notyfService { get; }

        public BanSaoController(QlbanSachContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;

        }

        // GET: Admin/BanSao
        public async Task<IActionResult> Index(int? page)
        {
            var banSaoSachs = _context.BanSaoSaches.OrderByDescending(banSao => banSao.MaSach).Reverse().ToList();

            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            int pageSize = 10;
            PagedList<BanSaoSach> lst = new PagedList<BanSaoSach>(banSaoSachs, pageNumber, pageSize);

            return View(lst);
        }

        // GET: Admin/BanSao/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.BanSaoSaches == null)
            {
                return NotFound();
            }

            var banSaoSach = await _context.BanSaoSaches
                .Include(b => b.MaSachNavigation)
                .FirstOrDefaultAsync(m => m.MaBanSao == id);
            if (banSaoSach == null)
            {
                return NotFound();
            }

            return View(banSaoSach);
        }

        // GET: Admin/BanSao/Create
        public IActionResult Create()
        {
            ViewData["MaSach"] = new SelectList(_context.Saches, "MaSach", "TenSach");
            return View();
        }

        // POST: Admin/BanSao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBanSao,TinhTrangBs,MaSach")] BanSaoSach banSaoSach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(banSaoSach);
                _notyfService.Success("Tạo thành công");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSach"] = new SelectList(_context.Saches, "MaSach", "TenSach", banSaoSach.MaSach);
            return View(banSaoSach);
        }

        // GET: Admin/BanSao/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.BanSaoSaches == null)
            {
                return NotFound();
            }

            var banSaoSach = await _context.BanSaoSaches.FindAsync(id);
            if (banSaoSach == null)
            {
                return NotFound();
            }
            ViewData["MaSach"] = new SelectList(_context.Saches, "MaSach", "MaSach", banSaoSach.MaSach);
            return View(banSaoSach);
        }

        // POST: Admin/BanSao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBanSao,TinhTrangBs,MaSach")] BanSaoSach banSaoSach)
        {
            if (id != banSaoSach.MaBanSao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banSaoSach);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Sửa thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BanSaoSachExists(banSaoSach.MaBanSao))
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
            ViewData["MaSach"] = new SelectList(_context.Saches, "MaSach", "MaSach", banSaoSach.MaSach);
            return View(banSaoSach);
        }

        // GET: Admin/BanSao/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.BanSaoSaches == null)
            {
                return NotFound();
            }

            var banSaoSach = await _context.BanSaoSaches
                .Include(b => b.MaSachNavigation)
                .FirstOrDefaultAsync(m => m.MaBanSao == id);
            if (banSaoSach == null)
            {
                return NotFound();
            }

            return View(banSaoSach);
        }

        // POST: Admin/BanSao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BanSaoSaches == null)
            {
                return Problem("Entity set 'QlbanSachContext.BanSaoSaches'  is null.");
            }
            var banSaoSach = await _context.BanSaoSaches.FindAsync(id);
            if (banSaoSach != null)
            {
                _context.BanSaoSaches.Remove(banSaoSach);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool BanSaoSachExists(int id)
        {
          return (_context.BanSaoSaches?.Any(e => e.MaBanSao == id)).GetValueOrDefault();
        }
    }
}
