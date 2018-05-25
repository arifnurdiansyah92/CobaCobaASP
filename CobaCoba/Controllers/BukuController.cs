using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CobaCoba.Data;
using CobaCoba.Models;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace CobaCoba.Controllers
{
    [Authorize(Roles = Roles.GetRoles.Admin.Roles)]

    public class BukuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BukuController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            return View();
        }
        // GET: Buku
        public async Task<IActionResult> Index()
        {
            return View(await _context.Buku.ToListAsync());
        }

        // GET: Buku/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Buku
                .SingleOrDefaultAsync(m => m.BukuId == id);
            if (buku == null)
            {
                return NotFound();
            }

            return View(buku);
        }

        // GET: Buku/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buku/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BukuId,judul,penulis,tahunTerbit,harga")] Buku buku)
        {
            if (ModelState.IsValid)
            {
                buku.harga = "Rp. " + buku.harga;
                _context.Add(buku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buku);
        }

        // GET: Buku/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Buku.SingleOrDefaultAsync(m => m.BukuId == id);
            if (buku == null)
            {
                return NotFound();
            }
            return View(buku);
        }

        // POST: Buku/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BukuId,judul,penulis,tahunTerbit,harga")] Buku buku)
        {
            if (id != buku.BukuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BukuExists(buku.BukuId))
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
            return View(buku);
        }

        // GET: Buku/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Buku
                .SingleOrDefaultAsync(m => m.BukuId == id);
            if (buku == null)
            {
                return NotFound();
            }

            return View(buku);
        }

        // POST: Buku/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buku = await _context.Buku.SingleOrDefaultAsync(m => m.BukuId == id);
            _context.Buku.Remove(buku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BukuExists(int id)
        {
            return _context.Buku.Any(e => e.BukuId == id);
        }

    }
}
