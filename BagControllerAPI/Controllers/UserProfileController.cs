using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using BagControllerAPI.Data;
using BagControllerAPI.Model;

namespace BagControllerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly BagDbContext _context;

        public UserProfileController(BagDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProfile/GetAllProfiles
        [HttpGet("GetAllProfiles")]
        public async Task<IActionResult> GetAllUserProfiles()
        {
            var profiles = await _context.UserProfiles.ToListAsync();
            return Ok(profiles);
        }

        // GET: api/UserProfile/GetProfileById/{id}
        [HttpGet("GetProfileById/{id}")]
        public async Task<IActionResult> GetUserProfileById(int id)
        {
            var profile = await _context.UserProfiles
                                         .Where(up => up.UserId == id)
                                         .FirstOrDefaultAsync();

            if (profile == null)
            {
                return NotFound($"User profile with ID '{id}' not found.");
            }

            return Ok(profile);
        }

        // POST: api/UserProfile/CreateProfile
        [HttpPost("CreateProfile")]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfiles newProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure you do not set the 'userId' manually, let the database handle it.
            if (newProfile.User != null)
            {
                newProfile.User.Id = 0; // Ensures the database generates a new ID
            }

            _context.UserProfiles.Add(newProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserProfileById), new { id = newProfile.UserId }, newProfile);
        }


        // PUT: api/UserProfile/UpdateProfile/{id}
        [HttpPut("UpdateProfile/{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromBody] UserProfiles updatedProfile)
        {
            if (id != updatedProfile.UserId)
            {
                return BadRequest("User profile ID mismatch.");
            }

            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound($"User profile with ID '{id}' not found.");
            }

            // Update profile fields
            profile.FirstName = updatedProfile.FirstName;
            profile.LastName = updatedProfile.LastName;
            profile.Phone = updatedProfile.Phone;
            profile.Address = updatedProfile.Address;
            profile.City = updatedProfile.City;
            profile.State = updatedProfile.State;
            profile.Country = updatedProfile.Country;
            profile.ZipCode = updatedProfile.ZipCode;

            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();

            return Ok(profile);
        }

        // DELETE: api/UserProfile/DeleteProfile/{id}
        [HttpDelete("DeleteProfile/{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound($"User profile with ID '{id}' not found.");
            }

            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();

            return Ok($"User profile with ID '{id}' has been deleted.");
        }
    }
}
