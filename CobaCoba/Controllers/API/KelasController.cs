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
    [Route("api/Kelas")]
    public class KelasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KelasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Kelas
        [HttpGet]
        public IEnumerable<Kelas> GetKelas()
        {
            return _context.Kelas;
        }

        // GET: api/Kelas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKelas([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kelas = await _context.Kelas.SingleOrDefaultAsync(m => m.KelasId == id);

            if (kelas == null)
            {
                return NotFound();
            }

            return Ok(kelas);
        }

        // PUT: api/Kelas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKelas([FromRoute] int id, [FromBody] Kelas kelas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kelas.KelasId)
            {
                return BadRequest();
            }

            _context.Entry(kelas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KelasExists(id))
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

        // POST: api/Kelas
        [HttpPost]
        public async Task<IActionResult> PostKelas([FromBody] Kelas kelas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Kelas.Add(kelas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKelas", new { id = kelas.KelasId }, kelas);
        }

        // DELETE: api/Kelas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKelas([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kelas = await _context.Kelas.SingleOrDefaultAsync(m => m.KelasId == id);
            if (kelas == null)
            {
                return NotFound();
            }

            _context.Kelas.Remove(kelas);
            await _context.SaveChangesAsync();

            return Ok(kelas);
        }

        private bool KelasExists(int id)
        {
            return _context.Kelas.Any(e => e.KelasId == id);
        }
    }
}