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
    
    public class AbsencesController : Controller
    {
        private readonly AppDbContext _context;

        public AbsencesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Absences
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Absences.Include(a => a.AuthorizedByUser).Include(a => a.ByUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Absences/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences
                .Include(a => a.AuthorizedByUser)
                .Include(a => a.ByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absence == null)
            {
                return NotFound();
            }

            return View(absence);
        }

        // GET: Absences/Create
        public IActionResult Create()
        {
            ViewData["AuthorizedByUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ByUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Absences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Reason,StartDate,EndDate,IsApproved,ByUserId,AuthorizedByUserId,Id")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                absence.Id = Guid.NewGuid();
                _context.Add(absence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorizedByUserId"] = new SelectList(_context.Users, "Id", "Id", absence.AuthorizedByUserId);
            ViewData["ByUserId"] = new SelectList(_context.Users, "Id", "Id", absence.ByUserId);
            return View(absence);
        }

        // GET: Absences/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences.FindAsync(id);
            if (absence == null)
            {
                return NotFound();
            }
            ViewData["AuthorizedByUserId"] = new SelectList(_context.Users, "Id", "Id", absence.AuthorizedByUserId);
            ViewData["ByUserId"] = new SelectList(_context.Users, "Id", "Id", absence.ByUserId);
            return View(absence);
        }

        // POST: Absences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Reason,StartDate,EndDate,IsApproved,ByUserId,AuthorizedByUserId,Id")] Absence absence)
        {
            if (id != absence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(absence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsenceExists(absence.Id))
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
            ViewData["AuthorizedByUserId"] = new SelectList(_context.Users, "Id", "Id", absence.AuthorizedByUserId);
            ViewData["ByUserId"] = new SelectList(_context.Users, "Id", "Id", absence.ByUserId);
            return View(absence);
        }

        // GET: Absences/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences
                .Include(a => a.AuthorizedByUser)
                .Include(a => a.ByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absence == null)
            {
                return NotFound();
            }

            return View(absence);
        }

        // POST: Absences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var absence = await _context.Absences.FindAsync(id);
            if (absence != null)
            {
                _context.Absences.Remove(absence);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbsenceExists(Guid id)
        {
            return _context.Absences.Any(e => e.Id == id);
        }
    }
}
