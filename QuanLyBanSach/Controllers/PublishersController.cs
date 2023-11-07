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
    public class PublishersController : ControllerBase
    {
        private readonly QlbanSachContext _context;

        public PublishersController(QlbanSachContext context)
        {
            _context = context;
        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhaXuatBan>>> GetNhaXuatBans()
        {
          if (_context.NhaXuatBans == null)
          {
              return NotFound();
          }
            return await _context.NhaXuatBans.ToListAsync();
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NhaXuatBan>> GetNhaXuatBan(int id)
        {
          if (_context.NhaXuatBans == null)
          {
              return NotFound();
          }
            var nhaXuatBan = await _context.NhaXuatBans.FindAsync(id);

            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return nhaXuatBan;
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhaXuatBan(int id, NhaXuatBan nhaXuatBan)
        {
            if (id != nhaXuatBan.MaNxb)
            {
                return BadRequest();
            }

            _context.Entry(nhaXuatBan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhaXuatBanExists(id))
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

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NhaXuatBan>> PostNhaXuatBan(NhaXuatBan nhaXuatBan)
        {
          if (_context.NhaXuatBans == null)
          {
              return Problem("Entity set 'QlbanSachContext.NhaXuatBans'  is null.");
          }
            _context.NhaXuatBans.Add(nhaXuatBan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNhaXuatBan", new { id = nhaXuatBan.MaNxb }, nhaXuatBan);
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhaXuatBan(int id)
        {
            if (_context.NhaXuatBans == null)
            {
                return NotFound();
            }
            var nhaXuatBan = await _context.NhaXuatBans.FindAsync(id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            _context.NhaXuatBans.Remove(nhaXuatBan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NhaXuatBanExists(int id)
        {
            return (_context.NhaXuatBans?.Any(e => e.MaNxb == id)).GetValueOrDefault();
        }
    }
}
