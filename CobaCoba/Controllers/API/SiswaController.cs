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
    [Route("api/Siswa")]
    public class SiswaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiswaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Siswa
        [HttpGet]
        public IEnumerable<Siswa> GetSiswa()
        {
            var data_siswa = _context.Siswa.Join(_context.Kelas, x => x.KelasId, y => y.KelasId, (x, y) => x)
                .ToList();
            return data_siswa;
        }

        // GET: api/Siswa/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSiswa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var siswa = await _context.Siswa.SingleOrDefaultAsync(m => m.SiswaId == id);

            if (siswa == null)
            {
                return NotFound();
            }

            return Ok(siswa);
        }

        // PUT: api/Siswa/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSiswa([FromRoute] int id, [FromBody] Siswa siswa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != siswa.SiswaId)
            {
                return BadRequest();
            }

            _context.Entry(siswa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiswaExists(id))
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

        // POST: api/Siswa
        [HttpPost]
        public async Task<IActionResult> PostSiswa([FromBody] Siswa siswa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Siswa.Add(siswa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSiswa", new { id = siswa.SiswaId }, siswa);
        }

        // DELETE: api/Siswa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSiswa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var siswa = await _context.Siswa.SingleOrDefaultAsync(m => m.SiswaId == id);
            if (siswa == null)
            {
                return NotFound();
            }

            _context.Siswa.Remove(siswa);
            await _context.SaveChangesAsync();

            return Ok(siswa);
        }

        private bool SiswaExists(int id)
        {
            return _context.Siswa.Any(e => e.SiswaId == id);
        }
    }
}