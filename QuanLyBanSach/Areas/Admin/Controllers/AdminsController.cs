﻿using System;
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
    public class AdminsController : Controller
    {
        private readonly QlbanSachContext _context;

        public AdminsController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: Admin/Admins
        public async Task<IActionResult> Index()
        {
              return _context.Admins != null ? 
                          View(await _context.Admins.ToListAsync()) :
                          Problem("Entity set 'QlbanSachContext.Admins' is null.");
        }

        // GET: Admin/Admins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admins = await _context.Admins
                .FirstOrDefaultAsync(m => m.MaAd == id);
            if (admins == null)
            {
                return NotFound();
            }

            return View(admins);
        }

        // GET: Admin/Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaAd,MatKhau,TenDangNhap,HoTenAd")] Admins admins)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admins);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admins);
        }

        // GET: Admin/Admins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admins = await _context.Admins.FindAsync(id);
            if (admins == null)
            {
                return NotFound();
            }
            return View(admins);
        }

        // POST: Admin/Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaAd,MatKhau,TenDangNhap,HoTenAd")] Admins admins)
        {
            if (id != admins.MaAd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admins);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminsExists(admins.MaAd))
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
            return View(admins);
        }

        // GET: Admin/Admins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admins = await _context.Admins
                .FirstOrDefaultAsync(m => m.MaAd == id);
            if (admins == null)
            {
                return NotFound();
            }

            return View(admins);
        }

        // POST: Admin/Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Admins == null)
            {
                return Problem("Entity set 'QlbanSachContext.Admins' is null.");
            }
            var admins = await _context.Admins.FindAsync(id);
            if (admins != null)
            {
                _context.Admins.Remove(admins);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminsExists(string id)
        {
          return (_context.Admins?.Any(e => e.MaAd == id)).GetValueOrDefault();
        }
    }
}
