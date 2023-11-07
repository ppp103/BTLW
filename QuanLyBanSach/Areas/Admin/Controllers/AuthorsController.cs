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
    public class AuthorsController : ControllerBase
    {
        private readonly QlbanSachContext _context;

        public AuthorsController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TacGia>>> GetTacGia()
        {
          if (_context.TacGia == null)
          {
              return NotFound();
          }
            return await _context.TacGia.ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TacGia>> GetTacGia(int id)
        {
          if (_context.TacGia == null)
          {
              return NotFound();
          }
            var tacGia = await _context.TacGia.FindAsync(id);

            if (tacGia == null)
            {
                return NotFound();
            }

            return tacGia;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTacGia(int id, TacGia tacGia)
        {
            if (id != tacGia.MaTg)
            {
                return BadRequest();
            }

            _context.Entry(tacGia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TacGiaExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TacGia>> PostTacGia(TacGia tacGia)
        {
          if (_context.TacGia == null)
          {
              return Problem("Entity set 'QlbanSachContext.TacGia'  is null.");
          }
            _context.TacGia.Add(tacGia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTacGia", new { id = tacGia.MaTg }, tacGia);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTacGia(int id)
        {
            if (_context.TacGia == null)
            {
                return NotFound();
            }
            var tacGia = await _context.TacGia.FindAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }

            _context.TacGia.Remove(tacGia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TacGiaExists(int id)
        {
            return (_context.TacGia?.Any(e => e.MaTg == id)).GetValueOrDefault();
        }
    }
}
