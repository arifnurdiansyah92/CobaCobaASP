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
    [Route("api/Food")]
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Food
        [HttpGet]
        public IEnumerable<Food> GetFood()
        {
            return _context.Food;
        }

        // GET: api/Food/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFood([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var food = await _context.Food.SingleOrDefaultAsync(m => m.foodId == id);

            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
        }

        // PUT: api/Food/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood([FromRoute] int id, [FromBody] Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != food.foodId)
            {
                return BadRequest();
            }

            _context.Entry(food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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

        // POST: api/Food
        [HttpPost]
        public async Task<IActionResult> PostFood([FromBody] Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Food.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFood", new { id = food.foodId }, food);
        }

        // DELETE: api/Food/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var food = await _context.Food.SingleOrDefaultAsync(m => m.foodId == id);
            if (food == null)
            {
                return NotFound();
            }

            _context.Food.Remove(food);
            await _context.SaveChangesAsync();

            return Ok(food);
        }

        private bool FoodExists(int id)
        {
            return _context.Food.Any(e => e.foodId == id);
        }
    }
}