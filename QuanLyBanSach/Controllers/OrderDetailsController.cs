using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;
using QuanLyBanSach.Models.ViewModels;

namespace QuanLyBanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly QlbanSachContext _context;

        public OrderDetailsController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public IEnumerable<OrderDetail> GetChiTietDonHangs()
        {
            var orderDetail = (from s in _context.Saches
                               join ct in _context.ChiTietDonHangs on s.MaSach equals ct.MaSach
                               select new OrderDetail
                               {
                                   MaCtdonHang = ct.MaCtdonHang,
                                   MaSach = ct.MaSach,
                                   SoLuong = ct.SoLuong,
                                   GiamGia = ct.GiamGia,
                                   ThanhTien = ct.ThanhTien,
                                   MaDonHang = ct.MaDonHang,
                                   TenSach = s.TenSach,
                                   GiaBan = s.GiaBan,
                               }).ToList();
            return orderDetail;
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChiTietDonHang>> GetChiTietDonHang(int id)
        {
          if (_context.ChiTietDonHangs == null)
          {
              return NotFound();
          }
            var chiTietDonHang = await _context.ChiTietDonHangs.FindAsync(id);

            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return chiTietDonHang;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChiTietDonHang(int id, ChiTietDonHang chiTietDonHang)
        {
            if (id != chiTietDonHang.MaCtdonHang)
            {
                return BadRequest();
            }

            _context.Entry(chiTietDonHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietDonHangExists(id))
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

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChiTietDonHang>> PostChiTietDonHang(ChiTietDonHang chiTietDonHang)
        {
          if (_context.ChiTietDonHangs == null)
          {
              return Problem("Entity set 'QlbanSachContext.ChiTietDonHangs'  is null.");
          }
            _context.ChiTietDonHangs.Add(chiTietDonHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChiTietDonHang", new { id = chiTietDonHang.MaCtdonHang }, chiTietDonHang);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChiTietDonHang(int id)
        {
            if (_context.ChiTietDonHangs == null)
            {
                return NotFound();
            }
            var chiTietDonHang = await _context.ChiTietDonHangs.FindAsync(id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            _context.ChiTietDonHangs.Remove(chiTietDonHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChiTietDonHangExists(int id)
        {
            return (_context.ChiTietDonHangs?.Any(e => e.MaCtdonHang == id)).GetValueOrDefault();
        }
    }
}
