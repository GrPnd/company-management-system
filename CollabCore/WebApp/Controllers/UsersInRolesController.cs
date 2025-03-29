using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class UsersInRolesController : Controller
    {
        private readonly AppDbContext _context;

        public UsersInRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UsersInRoles
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UsersInRoles.Include(u => u.Role).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UsersInRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInRole = await _context.UsersInRoles
                .Include(u => u.Role)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInRole == null)
            {
                return NotFound();
            }

            return View(userInRole);
        }

        // GET: UsersInRoles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UsersInRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Since,Until,UserId,RoleId,Id")] UserInRole userInRole)
        {
            if (ModelState.IsValid)
            {
                userInRole.Id = Guid.NewGuid();
                _context.Add(userInRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userInRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userInRole.UserId);
            return View(userInRole);
        }

        // GET: UsersInRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInRole = await _context.UsersInRoles.FindAsync(id);
            if (userInRole == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userInRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userInRole.UserId);
            return View(userInRole);
        }

        // POST: UsersInRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Since,Until,UserId,RoleId,Id")] UserInRole userInRole)
        {
            if (id != userInRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInRoleExists(userInRole.Id))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userInRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userInRole.UserId);
            return View(userInRole);
        }

        // GET: UsersInRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInRole = await _context.UsersInRoles
                .Include(u => u.Role)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInRole == null)
            {
                return NotFound();
            }

            return View(userInRole);
        }

        // POST: UsersInRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userInRole = await _context.UsersInRoles.FindAsync(id);
            if (userInRole != null)
            {
                _context.UsersInRoles.Remove(userInRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInRoleExists(Guid id)
        {
            return _context.UsersInRoles.Any(e => e.Id == id);
        }
    }
}
