using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.BLL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    
    public class AbsencesController : Controller
    {
        private readonly IAppBLL _bll;

        public AbsencesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Absences
        public async Task<IActionResult> Index()
        {
            var res = await _bll.AbsenceService.AllAsync();
            return View(res);
        }

        // GET: Absences/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var entity = await _bll.AbsenceService.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Absences/Create
        public async Task<IActionResult> Create()
        {
            var vm = new AbsenceViewModel()
            {
                AuthorizedByUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.Id)),
                ByUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.Id))
            };
            return View(vm);
        }

        // POST: Absences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AbsenceViewModel vm)
        {
            //"Reason,StartDate,EndDate,IsApproved,ByUserId,AuthorizedByUserId,Id")
            if (ModelState.IsValid)
            {
                _bll.AbsenceService.Add(vm.Absence);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.AuthorizedByUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.Absence.AuthorizedByUserId);
            vm.ByUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.Absence.ByUserId);

            return View(vm);
        }

        // GET: Absences/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var absence = await _bll.AbsenceService.FindAsync(id.Value, User.GetUserId());
            if (absence == null)
            {
                return NotFound();
            }

            var vm = new AbsenceViewModel()
            {
                AuthorizedByUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()),
                    nameof(Person.Id), nameof(Person.Id), absence.AuthorizedByUserId),
                ByUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()),
                    nameof(Person.Id), nameof(Person.Id), absence.ByUserId),
                Absence = absence
            };
            return View(vm);
        }

        // POST: Absences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AbsenceViewModel vm)
        {
            if (id != vm.Absence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.AbsenceService.Update(vm.Absence);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.AuthorizedByUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.Absence.AuthorizedByUserId);
            vm.ByUserSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()),
                nameof(Person.Id), nameof(Person.Id), vm.Absence.ByUserId);
            return View(vm);
        }

        // GET: Absences/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var absence = await _bll.AbsenceService.FindAsync(id.Value, User.GetUserId());

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
            await _bll.AbsenceService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
