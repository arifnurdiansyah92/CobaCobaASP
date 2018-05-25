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
    [Route("api/SalesOrder")]
    public class SalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesOrder
        [HttpGet]
        public IEnumerable<SalesOrderHeader> GetSalesOrderHeader()
        {
            return _context.SalesOrderHeader;
        }

        // GET: api/SalesOrder/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesOrderHeader([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesOrderHeader = await _context.SalesOrderHeader.SingleOrDefaultAsync(m => m.SalesOrderHeaderId == id);

            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            return Ok(salesOrderHeader);
        }

        // PUT: api/SalesOrder/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesOrderHeader([FromRoute] int id, [FromBody] SalesOrderHeader salesOrderHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesOrderHeader.SalesOrderHeaderId)
            {
                return BadRequest();
            }

            _context.Entry(salesOrderHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesOrderHeaderExists(id))
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

        // POST: api/SalesOrder
        [HttpPost]
        public async Task<IActionResult> PostSalesOrderHeader([FromBody] SalesOrderHeader salesOrderHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SalesOrderHeader.Add(salesOrderHeader);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesOrderHeader", new { id = salesOrderHeader.SalesOrderHeaderId }, salesOrderHeader);
        }

        // DELETE: api/SalesOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOrderHeader([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesOrderHeader = await _context.SalesOrderHeader.SingleOrDefaultAsync(m => m.SalesOrderHeaderId == id);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            _context.SalesOrderHeader.Remove(salesOrderHeader);
            await _context.SaveChangesAsync();

            return Ok(salesOrderHeader);
        }

        private bool SalesOrderHeaderExists(int id)
        {
            return _context.SalesOrderHeader.Any(e => e.SalesOrderHeaderId == id);
        }
    }
}