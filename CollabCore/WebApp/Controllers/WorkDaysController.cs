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
    public class WorkDaysController : Controller
    {
        private readonly AppDbContext _context;

        public WorkDaysController(AppDbContext context)
        {
            _context = context;
        }

        // GET: WorkDays
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkDays.ToListAsync());
        }

        // GET: WorkDays/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _context.WorkDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workDay == null)
            {
                return NotFound();
            }

            return View(workDay);
        }

        // GET: WorkDays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Id")] WorkDay workDay)
        {
            if (ModelState.IsValid)
            {
                workDay.Id = Guid.NewGuid();
                _context.Add(workDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workDay);
        }

        // GET: WorkDays/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _context.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return NotFound();
            }
            return View(workDay);
        }

        // POST: WorkDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Date,Id")] WorkDay workDay)
        {
            if (id != workDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkDayExists(workDay.Id))
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
            return View(workDay);
        }

        // GET: WorkDays/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _context.WorkDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workDay == null)
            {
                return NotFound();
            }

            return View(workDay);
        }

        // POST: WorkDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workDay = await _context.WorkDays.FindAsync(id);
            if (workDay != null)
            {
                _context.WorkDays.Remove(workDay);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkDayExists(Guid id)
        {
            return _context.WorkDays.Any(e => e.Id == id);
        }
    }
}
