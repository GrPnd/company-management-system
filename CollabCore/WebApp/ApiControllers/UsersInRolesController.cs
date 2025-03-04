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
    public class UsersInRolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersInRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UsersInRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInRole>>> GetUsersInRoles()
        {
            return await _context.UsersInRoles.ToListAsync();
        }

        // GET: api/UsersInRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInRole>> GetUserInRole(Guid id)
        {
            var userInRole = await _context.UsersInRoles.FindAsync(id);

            if (userInRole == null)
            {
                return NotFound();
            }

            return userInRole;
        }

        // PUT: api/UsersInRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInRole(Guid id, UserInRole userInRole)
        {
            if (id != userInRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(userInRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInRoleExists(id))
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

        // POST: api/UsersInRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserInRole>> PostUserInRole(UserInRole userInRole)
        {
            _context.UsersInRoles.Add(userInRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserInRole", new { id = userInRole.Id }, userInRole);
        }

        // DELETE: api/UsersInRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInRole(Guid id)
        {
            var userInRole = await _context.UsersInRoles.FindAsync(id);
            if (userInRole == null)
            {
                return NotFound();
            }

            _context.UsersInRoles.Remove(userInRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserInRoleExists(Guid id)
        {
            return _context.UsersInRoles.Any(e => e.Id == id);
        }
    }
}
