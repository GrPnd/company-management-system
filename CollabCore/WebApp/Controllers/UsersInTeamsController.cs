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
    public class UsersInTeamsController : Controller
    {
        private readonly AppDbContext _context;

        public UsersInTeamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UsersInTeams
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UsersInTeams.Include(u => u.Team).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UsersInTeams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeam = await _context.UsersInTeams
                .Include(u => u.Team)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInTeam == null)
            {
                return NotFound();
            }

            return View(userInTeam);
        }

        // GET: UsersInTeams/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UsersInTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Role,Since,Until,UserId,TeamId,Id")] UserInTeam userInTeam)
        {
            if (ModelState.IsValid)
            {
                userInTeam.Id = Guid.NewGuid();
                _context.Add(userInTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", userInTeam.TeamId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userInTeam.UserId);
            return View(userInTeam);
        }

        // GET: UsersInTeams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeam = await _context.UsersInTeams.FindAsync(id);
            if (userInTeam == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", userInTeam.TeamId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userInTeam.UserId);
            return View(userInTeam);
        }

        // POST: UsersInTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Role,Since,Until,UserId,TeamId,Id")] UserInTeam userInTeam)
        {
            if (id != userInTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInTeamExists(userInTeam.Id))
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
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", userInTeam.TeamId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userInTeam.UserId);
            return View(userInTeam);
        }

        // GET: UsersInTeams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeam = await _context.UsersInTeams
                .Include(u => u.Team)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInTeam == null)
            {
                return NotFound();
            }

            return View(userInTeam);
        }

        // POST: UsersInTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userInTeam = await _context.UsersInTeams.FindAsync(id);
            if (userInTeam != null)
            {
                _context.UsersInTeams.Remove(userInTeam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInTeamExists(Guid id)
        {
            return _context.UsersInTeams.Any(e => e.Id == id);
        }
    }
}
