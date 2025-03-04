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
    public class AbsencesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AbsencesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Absences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Absence>>> GetAbsences()
        {
            return await _context.Absences.ToListAsync();
        }

        // GET: api/Absences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Absence>> GetAbsence(Guid id)
        {
            var absence = await _context.Absences.FindAsync(id);

            if (absence == null)
            {
                return NotFound();
            }

            return absence;
        }

        // PUT: api/Absences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbsence(Guid id, Absence absence)
        {
            if (id != absence.Id)
            {
                return BadRequest();
            }

            _context.Entry(absence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbsenceExists(id))
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

        // POST: api/Absences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Absence>> PostAbsence(Absence absence)
        {
            _context.Absences.Add(absence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbsence", new { id = absence.Id }, absence);
        }

        // DELETE: api/Absences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbsence(Guid id)
        {
            var absence = await _context.Absences.FindAsync(id);
            if (absence == null)
            {
                return NotFound();
            }

            _context.Absences.Remove(absence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbsenceExists(Guid id)
        {
            return _context.Absences.Any(e => e.Id == id);
        }
    }
}
