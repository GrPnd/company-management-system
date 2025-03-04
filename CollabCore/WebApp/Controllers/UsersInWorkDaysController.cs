using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    public class UsersInWorkDaysController : Controller
    {
        private readonly AppDbContext _context;

        public UsersInWorkDaysController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UsersInWorkDays
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UsersInWorkDays.Include(u => u.WorkDay);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UsersInWorkDays/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInWorkDay = await _context.UsersInWorkDays
                .Include(u => u.WorkDay)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInWorkDay == null)
            {
                return NotFound();
            }

            return View(userInWorkDay);
        }

        // GET: UsersInWorkDays/Create
        public IActionResult Create()
        {
            ViewData["WorkDayId"] = new SelectList(_context.WorkDays, "Id", "Id");
            return View();
        }

        // POST: UsersInWorkDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SinceDate,ToDate,UserId,WorkDayId,Id")] UserInWorkDay userInWorkDay)
        {
            if (ModelState.IsValid)
            {
                userInWorkDay.Id = Guid.NewGuid();
                _context.Add(userInWorkDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkDayId"] = new SelectList(_context.WorkDays, "Id", "Id", userInWorkDay.WorkDayId);
            return View(userInWorkDay);
        }

        // GET: UsersInWorkDays/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInWorkDay = await _context.UsersInWorkDays.FindAsync(id);
            if (userInWorkDay == null)
            {
                return NotFound();
            }
            ViewData["WorkDayId"] = new SelectList(_context.WorkDays, "Id", "Id", userInWorkDay.WorkDayId);
            return View(userInWorkDay);
        }

        // POST: UsersInWorkDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SinceDate,ToDate,UserId,WorkDayId,Id")] UserInWorkDay userInWorkDay)
        {
            if (id != userInWorkDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInWorkDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInWorkDayExists(userInWorkDay.Id))
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
            ViewData["WorkDayId"] = new SelectList(_context.WorkDays, "Id", "Id", userInWorkDay.WorkDayId);
            return View(userInWorkDay);
        }

        // GET: UsersInWorkDays/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInWorkDay = await _context.UsersInWorkDays
                .Include(u => u.WorkDay)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInWorkDay == null)
            {
                return NotFound();
            }

            return View(userInWorkDay);
        }

        // POST: UsersInWorkDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userInWorkDay = await _context.UsersInWorkDays.FindAsync(id);
            if (userInWorkDay != null)
            {
                _context.UsersInWorkDays.Remove(userInWorkDay);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInWorkDayExists(Guid id)
        {
            return _context.UsersInWorkDays.Any(e => e.Id == id);
        }
    }
}
