using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersInTeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersInTeamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UsersInTeams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInTeam>>> GetUsersInTeams()
        {
            return await _context.UsersInTeams.ToListAsync();
        }

        // GET: api/UsersInTeams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInTeam>> GetUserInTeam(Guid id)
        {
            var userInTeam = await _context.UsersInTeams.FindAsync(id);

            if (userInTeam == null)
            {
                return NotFound();
            }

            return userInTeam;
        }

        // PUT: api/UsersInTeams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInTeam(Guid id, UserInTeam userInTeam)
        {
            if (id != userInTeam.Id)
            {
                return BadRequest();
            }

            _context.Entry(userInTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInTeamExists(id))
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

        // POST: api/UsersInTeams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserInTeam>> PostUserInTeam(UserInTeam userInTeam)
        {
            _context.UsersInTeams.Add(userInTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserInTeam", new { id = userInTeam.Id }, userInTeam);
        }

        // DELETE: api/UsersInTeams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInTeam(Guid id)
        {
            var userInTeam = await _context.UsersInTeams.FindAsync(id);
            if (userInTeam == null)
            {
                return NotFound();
            }

            _context.UsersInTeams.Remove(userInTeam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserInTeamExists(Guid id)
        {
            return _context.UsersInTeams.Any(e => e.Id == id);
        }
    }
}
