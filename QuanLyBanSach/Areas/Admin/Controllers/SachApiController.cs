using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SachApiController : ControllerBase
    {
        private readonly QlbanSachContext _context;

        public SachApiController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: api/SachApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sach>>> GetSaches()
        {
          if (_context.Saches == null)
          {
              return NotFound();
          }
            return await _context.Saches.ToListAsync();
        }

        // GET: api/SachApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sach>> GetSach(string id)
        {
          if (_context.Saches == null)
          {
              return NotFound();
          }
            var sach = await _context.Saches.FindAsync(id);

            if (sach == null)
            {
                return NotFound();
            }

            return sach;
        }

        // PUT: api/SachApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSach(int id, Sach sach)
        {
            if (id != sach.MaSach)
            {
                return BadRequest();
            }

            _context.Entry(sach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SachExists(id))
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

        // POST: api/SachApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sach>> PostSach(Sach sach)
        {
          if (_context.Saches == null)
          {
              return Problem("Entity set 'QlbanSachContext.Saches'  is null.");
          }
            _context.Saches.Add(sach);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SachExists(sach.MaSach))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSach", new { id = sach.MaSach }, sach);
        }

        // DELETE: api/SachApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSach(int id)
        {
            if (_context.Saches == null)
            {
                return NotFound();
            }
            var sach = await _context.Saches.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }

            _context.Saches.Remove(sach);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SachExists(int id)
        {
            return (_context.Saches?.Any(e => e.MaSach == id)).GetValueOrDefault();
        }
    }
}
