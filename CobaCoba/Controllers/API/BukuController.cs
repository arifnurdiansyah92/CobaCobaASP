using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CobaCoba.Data;
using CobaCoba.Models;

namespace CobaCoba.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Buku")]
    public class BukuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BukuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Buku
        [HttpGet]
        public IEnumerable<Buku> GetBuku()
        {
            return _context.Buku;
        }

        // GET: api/Buku/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuku([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var buku = await _context.Buku.SingleOrDefaultAsync(m => m.BukuId == id);

            if (buku == null)
            {
                return NotFound();
            }

            return Ok(buku);
        }

        // PUT: api/Buku/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuku([FromRoute] int id, [FromBody] Buku buku)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buku.BukuId)
            {
                return BadRequest();
            }

            _context.Entry(buku).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BukuExists(id))
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

        // POST: api/Buku
        [HttpPost]
        public async Task<IActionResult> PostBuku([FromBody] Buku buku)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            buku.harga = "Rp. " + buku.harga;
            _context.Buku.Add(buku);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuku", new { id = buku.BukuId }, buku);
        }

        // DELETE: api/Buku/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuku([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var buku = await _context.Buku.SingleOrDefaultAsync(m => m.BukuId == id);
            if (buku == null)
            {
                return NotFound();
            }

            _context.Buku.Remove(buku);
            await _context.SaveChangesAsync();

            return Ok(buku);
        }

        private bool BukuExists(int id)
        {
            return _context.Buku.Any(e => e.BukuId == id);
        }
    }
}