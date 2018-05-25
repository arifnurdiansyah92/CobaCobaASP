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
    [Route("api/SalesLine")]
    public class SalesLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesLine
        [HttpGet]
        public IEnumerable<SalesOrderLine> GetSalesOrderLine()
        {
            return _context.SalesOrderLine;
        }

        // GET: api/SalesLine/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesOrderLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesOrderLine = await _context.SalesOrderLine.SingleOrDefaultAsync(m => m.SalesOrderLineId == id);

            if (salesOrderLine == null)
            {
                return NotFound();
            }

            return Ok(salesOrderLine);
        }

        // PUT: api/SalesLine/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesOrderLine([FromRoute] int id, [FromBody] SalesOrderLine salesOrderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesOrderLine.SalesOrderLineId)
            {
                return BadRequest();
            }

            _context.Entry(salesOrderLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesOrderLineExists(id))
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

        // POST: api/SalesLine
        [HttpPost]
        public async Task<IActionResult> PostSalesOrderLine([FromBody] SalesOrderLine salesOrderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SalesOrderLine.Add(salesOrderLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesOrderLine", new { id = salesOrderLine.SalesOrderLineId }, salesOrderLine);
        }

        // DELETE: api/SalesLine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOrderLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesOrderLine = await _context.SalesOrderLine.SingleOrDefaultAsync(m => m.SalesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }

            _context.SalesOrderLine.Remove(salesOrderLine);
            await _context.SaveChangesAsync();

            return Ok(salesOrderLine);
        }

        private bool SalesOrderLineExists(int id)
        {
            return _context.SalesOrderLine.Any(e => e.SalesOrderLineId == id);
        }
    }
}