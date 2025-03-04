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
    public class UsersInWorkDaysController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersInWorkDaysController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UsersInWorkDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInWorkDay>>> GetUsersInWorkDays()
        {
            return await _context.UsersInWorkDays.ToListAsync();
        }

        // GET: api/UsersInWorkDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInWorkDay>> GetUserInWorkDay(Guid id)
        {
            var userInWorkDay = await _context.UsersInWorkDays.FindAsync(id);

            if (userInWorkDay == null)
            {
                return NotFound();
            }

            return userInWorkDay;
        }

        // PUT: api/UsersInWorkDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInWorkDay(Guid id, UserInWorkDay userInWorkDay)
        {
            if (id != userInWorkDay.Id)
            {
                return BadRequest();
            }

            _context.Entry(userInWorkDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInWorkDayExists(id))
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

        // POST: api/UsersInWorkDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserInWorkDay>> PostUserInWorkDay(UserInWorkDay userInWorkDay)
        {
            _context.UsersInWorkDays.Add(userInWorkDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserInWorkDay", new { id = userInWorkDay.Id }, userInWorkDay);
        }

        // DELETE: api/UsersInWorkDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInWorkDay(Guid id)
        {
            var userInWorkDay = await _context.UsersInWorkDays.FindAsync(id);
            if (userInWorkDay == null)
            {
                return NotFound();
            }

            _context.UsersInWorkDays.Remove(userInWorkDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserInWorkDayExists(Guid id)
        {
            return _context.UsersInWorkDays.Any(e => e.Id == id);
        }
    }
}
