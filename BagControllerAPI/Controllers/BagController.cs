using BagControllerAPI.Data;
using BagControllerAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BagControllerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BagController : ControllerBase
    {
        private readonly BagDbContext _context;

        public BagController(BagDbContext context)
        {
            _context = context;
        }

        // GET: api/Bag/GetAllBags
        [HttpGet("GetAllBags")]
        public async Task<IActionResult> GetAllBags()
        {
            var bags = await _context.Bags.ToListAsync();
            return Ok(bags);
        }

        // GET: api/Bag/GetBagById/{id}
        [HttpGet("GetBagById/{id}")]
        public async Task<IActionResult> GetBagById(int id)
        {
            var bag = await _context.Bags.FindAsync(id);
            if (bag == null)
            {
                return NotFound($"Bag with ID '{id}' not found.");
            }
            return Ok(bag);
        }

        // POST: api/Bag/PostBagData
        [HttpPost("PostBagData")]
        public async Task<IActionResult> PostBag([FromBody] Bags newBag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newBag.CreatedAt = DateTime.UtcNow; // Set the creation timestamp
            await _context.Bags.AddAsync(newBag);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBagById), new { id = newBag.BagId }, newBag);
        }

        // PUT: api/Bag/UpdateBagData/{id}
        [HttpPut("UpdateBagData/{id}")]
        public async Task<IActionResult> UpdateBag(int id, [FromBody] Bags updatedBag)
        {
            if (id != updatedBag.BagId)
            {
                return BadRequest("Bag ID mismatch.");
            }

            var bag = await _context.Bags.FindAsync(id);
            if (bag == null)
            {
                return NotFound($"Bag with ID '{id}' not found.");
            }

            // Update bag fields
            bag.Name = updatedBag.Name;
            bag.Description = updatedBag.Description;
            bag.Price = updatedBag.Price;
            bag.StockQuantity = updatedBag.StockQuantity;
            bag.CategoryId = updatedBag.CategoryId;
            bag.UpdatedAt = DateTime.UtcNow;

            _context.Bags.Update(bag);
            await _context.SaveChangesAsync();

            return Ok(bag);
        }

        // DELETE: api/Bag/DeleteBag/{id}
        [HttpDelete("DeleteBag/{id}")]
        public async Task<IActionResult> DeleteBag(int id)
        {
            var bag = await _context.Bags.FindAsync(id);
            if (bag == null)
            {
                return NotFound($"Bag with ID '{id}' not found.");
            }

            _context.Bags.Remove(bag);
            await _context.SaveChangesAsync();

            return Ok($"Bag with ID '{id}' has been deleted.");
        }
    }
}
