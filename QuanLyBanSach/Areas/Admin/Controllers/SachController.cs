using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Helpper;
using QuanLyBanSach.Models;
using X.PagedList;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SachController : Controller
    {
        private readonly QlbanSachContext _context;
        public INotyfService _notyfService { get; }

        public SachController(QlbanSachContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        //NamAIDS
        // GET: Admin/Sach
        public async Task<IActionResult> Index(int page = 1, string MaDm = "")
        {
            var pageNumber = page;
            var pageSize = 10;

            List<Sach> lst = new List<Sach>();

            if(MaDm != "")
            {
                lst = _context.Saches.Where(x => x.MaDm == MaDm).OrderByDescending(x=>x.MaSach).Reverse().ToList();
            }
            else
            {
                lst = _context.Saches.OrderByDescending(x => x.MaSach).Reverse().ToList();
            }

            PagedList<Sach> models = new PagedList<Sach>(lst.AsQueryable(), pageNumber, pageSize);

            ViewData["DanhMuc"] = new SelectList(_context.DanhMucSaches, "MaDm", "TenDm");
            
            ViewBag.MaDm = MaDm; 
            ViewBag.page = pageNumber;

            return View(models);
        }

        public IActionResult Filter(int? page, string MaDm = "")
        {
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            var url = $"/Admin/Sach?MaDm={MaDm}";
            if(MaDm == 0.ToString())
            {
                url = $"/Admin/Sach";
            }
            return Json(new {status = "success", redirectUrl = url});
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
            ViewData["MaDm"] = new SelectList(_context.DanhMucSaches, "MaDm", "TenDm");
            ViewData["MaNxb"] = new SelectList(_context.NhaXuatBans, "MaNxb", "TenNxb");
            ViewData["MaTg"] = new SelectList(_context.TacGia, "MaTg", "TenTg");
            return View();
        }

        // POST: Admin/Sach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSach,TenSach,GiaBan,MoTa,SoLuongBs,Anh,NgayCapNhat,SoLuongCon,MaNxb,MaTg,MaDm")] Sach sach, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
                if (ModelState.IsValid)
                {
                    try{
                        sach.TenSach = Utilities.ToTitleCase(sach.TenSach);
                        if (fThumb != null)
                        {
                            string extension = Path.GetExtension(fThumb.FileName);
                            string image = Utilities.SEOUrl(sach.TenSach);
                            sach.Anh = await Utilities.UploadFile(fThumb, @"books", image.ToLower());
                        }
                        if (string.IsNullOrEmpty(sach.Anh)) sach.Anh = "default.jpg";

                        sach.NgayCapNhat = DateTime.Now;
                        sach.SoLuongBs = sach.SoLuongCon;
                        _context.Add(sach);
                        await _context.SaveChangesAsync();
                        _notyfService.Success("Tạo thành công");
                        return RedirectToAction(nameof(Index));
                    }catch(DbUpdateException ex)
                    {
                        _notyfService.Error("Không được trùng mã sách");
                        return View(sach);
                    }
                }
            ViewData["MaDm"] = new SelectList(_context.DanhMucSaches, "MaDm", "TenDm", sach.MaDm);
            ViewData["MaNxb"] = new SelectList(_context.NhaXuatBans, "MaNxb", "TenNxb", sach.MaNxb);
            ViewData["MaTg"] = new SelectList(_context.TacGia, "MaTg", "TenTg", sach.MaTg);

            _notyfService.Error("Vui lòng điền đủ thông tin");

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

            ViewData["MaDm"] = new SelectList(_context.DanhMucSaches, "MaDm", "TenDm", sach.MaDm);
            ViewData["MaNxb"] = new SelectList(_context.NhaXuatBans, "MaNxb", "TenNxb", sach.MaNxb);
            ViewData["MaTg"] = new SelectList(_context.TacGia, "MaTg", "TenTg", sach.MaTg);

            return View(sach);
        }

        // POST: Admin/Sach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSach,TenSach,GiaBan,MoTa,SoLuongBs,Anh,NgayCapNhat,SoLuongCon,MaNxb,MaTg,MaDm")] Sach sach, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != sach.MaSach)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sach.TenSach = Utilities.ToTitleCase(sach.TenSach);
                    if (string.IsNullOrEmpty(sach.Anh)) sach.Anh = "img-01";

                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(sach.TenSach);
                        sach.Anh = await Utilities.UploadFile(fThumb, @"books", image.ToLower());
                    }

                    sach.NgayCapNhat = DateTime.Now;
                    sach.SoLuongBs = sach.SoLuongCon;


                    _context.Update(sach);
                    await _context.SaveChangesAsync();

                    _notyfService.Success("Cập nhật thành công");
                    return RedirectToAction(nameof(Index));
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
            ViewData["MaDm"] = new SelectList(_context.DanhMucSaches, "MaDm", "TenDm", sach.MaDm);
            ViewData["MaNxb"] = new SelectList(_context.NhaXuatBans, "MaNxb", "TenNxb", sach.MaNxb);
            ViewData["MaTg"] = new SelectList(_context.TacGia, "MaTg", "TenTg", sach.MaTg);

            _notyfService.Error("Vui lòng điền đủ thông tin");


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
