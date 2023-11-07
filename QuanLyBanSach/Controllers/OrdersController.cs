using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly QlbanSachContext _context;

        public OrdersController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHangs()
        {
          if (_context.DonHangs == null)
          {
              return NotFound();
          }
            return await _context.DonHangs.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonHang>> GetDonHang(int id)
        {
          if (_context.DonHangs == null)
          {
              return NotFound();
          }
            var donHang = await _context.DonHangs.FindAsync(id);

            if (donHang == null)
            {
                return NotFound();
            }

            return donHang;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonHang(int id, DonHang donHang)
        {
            if (id != donHang.MaDonHang)
            {
                return BadRequest();
            }

            _context.Entry(donHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonHangExists(id))
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

        //POST: api/Orders
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
        public async Task<ActionResult<DonHang>> PostDonHang(DonHang donHang)
        {
            if (_context.DonHangs == null)
            {
                return Problem("Entity set 'QlbanSachContext.DonHangs'  is null.");
            }
            _context.DonHangs.Add(donHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonHang", new { id = donHang.MaDonHang }, donHang);
        }

        //[HttpPost]
        //public async Task<ActionResult<DonHang>> PostDonHang(string location, double totalMoney, string note, string customerName, string orderState, int userId)
        //{
        //	if (_context.DonHangs == null)
        //	{
        //		return Problem("Entity set 'QlbanSachContext.DonHangs'  is null.");
        //	}
        //          DonHang donHang = new DonHang
        //          {
        //              DiaDiemGh = location,
        //              TongTien = (decimal?)totalMoney,
        //              NgayDat = DateTime.Now,
        //              GhiChu = note,
        //              TenKh = customerName,
        //              TrangThaiDh = orderState,
        //              MaNd = userId,
        //	};
        //	_context.DonHangs.Add(donHang);
        //	await _context.SaveChangesAsync();

        //	return CreatedAtAction("GetDonHang", new { id = donHang.MaDonHang }, donHang);
        //}


        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonHang(int id)
        {
			if (_context.DonHangs == null)
            {
                return NotFound();
            }
            var donHang = await _context.DonHangs.FindAsync(id);
            var chiTietsToRemove = new List<ChiTietDonHang>();

            foreach (var chiTiet in _context.ChiTietDonHangs)
            {
                if (chiTiet.MaDonHang == id)
                {
                    chiTietsToRemove.Add(chiTiet);
                }
            }

            if (donHang == null)
            {
                return NotFound();
            }

            _context.DonHangs.Remove(donHang);
            _context.ChiTietDonHangs.RemoveRange(chiTietsToRemove);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonHangExists(int id)
        {
            return (_context.DonHangs?.Any(e => e.MaDonHang == id)).GetValueOrDefault();
        }
    }
}
