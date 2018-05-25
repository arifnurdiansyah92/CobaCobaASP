using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CobaCoba.Data;
using CobaCoba.Models;

namespace CobaCoba.Controllers
{
    public class SalesOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesOrderLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SalesOrderLine.Include(s => s.SalesOrderHeader);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SalesOrderLine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderLine = await _context.SalesOrderLine
                .Include(s => s.SalesOrderHeader)
                .SingleOrDefaultAsync(m => m.SalesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }

            return View(salesOrderLine);
        }

        // GET: SalesOrderLine/Create
        public IActionResult Create()
        {
            ViewData["SalesOrderHeaderId"] = new SelectList(_context.SalesOrderHeader, "SalesOrderHeaderId", "SalesOrderHeaderId");
            return View();
        }

        // POST: SalesOrderLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrderLineId,Product,Qty,Price,SalesOrderHeaderId")] SalesOrderLine salesOrderLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesOrderLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalesOrderHeaderId"] = new SelectList(_context.SalesOrderHeader, "SalesOrderHeaderId", "SalesOrderHeaderId", salesOrderLine.SalesOrderHeaderId);
            return View(salesOrderLine);
        }

        // GET: SalesOrderLine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderLine = await _context.SalesOrderLine.SingleOrDefaultAsync(m => m.SalesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }
            ViewData["SalesOrderHeaderId"] = new SelectList(_context.SalesOrderHeader, "SalesOrderHeaderId", "SalesOrderHeaderId", salesOrderLine.SalesOrderHeaderId);
            return View(salesOrderLine);
        }

        // POST: SalesOrderLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesOrderLineId,Product,Qty,Price,SalesOrderHeaderId")] SalesOrderLine salesOrderLine)
        {
            if (id != salesOrderLine.SalesOrderLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesOrderLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderLineExists(salesOrderLine.SalesOrderLineId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalesOrderHeaderId"] = new SelectList(_context.SalesOrderHeader, "SalesOrderHeaderId", "SalesOrderHeaderId", salesOrderLine.SalesOrderHeaderId);
            return View(salesOrderLine);
        }

        // GET: SalesOrderLine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderLine = await _context.SalesOrderLine
                .Include(s => s.SalesOrderHeader)
                .SingleOrDefaultAsync(m => m.SalesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }

            return View(salesOrderLine);
        }

        // POST: SalesOrderLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrderLine = await _context.SalesOrderLine.SingleOrDefaultAsync(m => m.SalesOrderLineId == id);
            _context.SalesOrderLine.Remove(salesOrderLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesOrderLineExists(int id)
        {
            return _context.SalesOrderLine.Any(e => e.SalesOrderLineId == id);
        }
    }
}
