using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CobaCoba.Models;
using CobaCoba.Data;
using Microsoft.EntityFrameworkCore;

namespace CobaCoba.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Food.ToListAsync());
        }
        public IActionResult About()
        {
            ViewData["Message"] = "CobaCoba adalah aplikasi kuliner yang dapat membantu anda mencoba berbagai makanan!";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "+6282240708329";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
