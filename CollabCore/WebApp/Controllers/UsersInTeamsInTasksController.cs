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
    public class UsersInTeamsInTasksController : Controller
    {
        private readonly AppDbContext _context;

        public UsersInTeamsInTasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UsersInTeamsInTasks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UsersInTeamsInTasks.Include(u => u.Task).Include(u => u.UserInTeam);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UsersInTeamsInTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeamInTask = await _context.UsersInTeamsInTasks
                .Include(u => u.Task)
                .Include(u => u.UserInTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInTeamInTask == null)
            {
                return NotFound();
            }

            return View(userInTeamInTask);
        }

        // GET: UsersInTeamsInTasks/Create
        public IActionResult Create()
        {
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Name");
            ViewData["UserInTeamId"] = new SelectList(_context.UsersInTeams, "Id", "Role");
            return View();
        }

        // POST: UsersInTeamsInTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Since,Until,Review,TaskId,UserInTeamId,Id")] UserInTeamInTask userInTeamInTask)
        {
            if (ModelState.IsValid)
            {
                userInTeamInTask.Id = Guid.NewGuid();
                _context.Add(userInTeamInTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Name", userInTeamInTask.TaskId);
            ViewData["UserInTeamId"] = new SelectList(_context.UsersInTeams, "Id", "Role", userInTeamInTask.UserInTeamId);
            return View(userInTeamInTask);
        }

        // GET: UsersInTeamsInTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeamInTask = await _context.UsersInTeamsInTasks.FindAsync(id);
            if (userInTeamInTask == null)
            {
                return NotFound();
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Name", userInTeamInTask.TaskId);
            ViewData["UserInTeamId"] = new SelectList(_context.UsersInTeams, "Id", "Role", userInTeamInTask.UserInTeamId);
            return View(userInTeamInTask);
        }

        // POST: UsersInTeamsInTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Since,Until,Review,TaskId,UserInTeamId,Id")] UserInTeamInTask userInTeamInTask)
        {
            if (id != userInTeamInTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInTeamInTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInTeamInTaskExists(userInTeamInTask.Id))
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
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Name", userInTeamInTask.TaskId);
            ViewData["UserInTeamId"] = new SelectList(_context.UsersInTeams, "Id", "Role", userInTeamInTask.UserInTeamId);
            return View(userInTeamInTask);
        }

        // GET: UsersInTeamsInTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeamInTask = await _context.UsersInTeamsInTasks
                .Include(u => u.Task)
                .Include(u => u.UserInTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInTeamInTask == null)
            {
                return NotFound();
            }

            return View(userInTeamInTask);
        }

        // POST: UsersInTeamsInTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userInTeamInTask = await _context.UsersInTeamsInTasks.FindAsync(id);
            if (userInTeamInTask != null)
            {
                _context.UsersInTeamsInTasks.Remove(userInTeamInTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInTeamInTaskExists(Guid id)
        {
            return _context.UsersInTeamsInTasks.Any(e => e.Id == id);
        }
    }
}
