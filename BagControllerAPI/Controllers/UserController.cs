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
    public class UserController : ControllerBase
    {
        private readonly BagDbContext _context;

        public UserController(BagDbContext context)
        {
            _context = context;
        }

        // GET: api/User/GetUserData
        [HttpGet("GetUserData")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // GET: api/User/GetUserById/{id}
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID '{id}' not found.");
            }
            return Ok(user);
        }

        // POST: api/User/PostUserData
        [HttpPost("PostUserData")]
        public async Task<IActionResult> PostUser([FromBody] Users newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newUser.CreatedAt = DateTime.UtcNow; // Set the creation timestamp
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        // PUT: api/User/UpdateUserData/{id}
        [HttpPut("UpdateUserData/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID '{id}' not found.");
            }

            // Update user fields
            user.Email = updatedUser.Email;
            user.UserName = updatedUser.UserName;
            user.Password = updatedUser.Password;
            user.RoleId = updatedUser.RoleId;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // DELETE: api/User/DeleteUser/{id}
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID '{id}' not found.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok($"User with ID '{id}' has been deleted.");
        }
    }
}
