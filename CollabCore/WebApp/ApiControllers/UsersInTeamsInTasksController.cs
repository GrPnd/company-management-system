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
    public class UsersInTeamsInTasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersInTeamsInTasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UsersInTeamsInTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInTeamInTask>>> GetUsersInTeamsInTasks()
        {
            return await _context.UsersInTeamsInTasks.ToListAsync();
        }

        // GET: api/UsersInTeamsInTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInTeamInTask>> GetUserInTeamInTask(Guid id)
        {
            var userInTeamInTask = await _context.UsersInTeamsInTasks.FindAsync(id);

            if (userInTeamInTask == null)
            {
                return NotFound();
            }

            return userInTeamInTask;
        }

        // PUT: api/UsersInTeamsInTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInTeamInTask(Guid id, UserInTeamInTask userInTeamInTask)
        {
            if (id != userInTeamInTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(userInTeamInTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInTeamInTaskExists(id))
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

        // POST: api/UsersInTeamsInTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserInTeamInTask>> PostUserInTeamInTask(UserInTeamInTask userInTeamInTask)
        {
            _context.UsersInTeamsInTasks.Add(userInTeamInTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserInTeamInTask", new { id = userInTeamInTask.Id }, userInTeamInTask);
        }

        // DELETE: api/UsersInTeamsInTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInTeamInTask(Guid id)
        {
            var userInTeamInTask = await _context.UsersInTeamsInTasks.FindAsync(id);
            if (userInTeamInTask == null)
            {
                return NotFound();
            }

            _context.UsersInTeamsInTasks.Remove(userInTeamInTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserInTeamInTaskExists(Guid id)
        {
            return _context.UsersInTeamsInTasks.Any(e => e.Id == id);
        }
    }
}
