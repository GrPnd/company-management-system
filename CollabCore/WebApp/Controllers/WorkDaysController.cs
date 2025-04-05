using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class WorkDaysController : Controller
    {
        private readonly IAppUOW _uow;

        public WorkDaysController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: WorkDays
        public async Task<IActionResult> Index()
        {
            var res = await _uow.WorkDayRepository.AllAsync();
            return View(res);
        }

        // GET: WorkDays/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.WorkDayRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
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
        public async Task<IActionResult> Create(WorkDayViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = new WorkDay()
                {
                    Day = vm.Day
                };
                
                _uow.WorkDayRepository.Add(entity);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: WorkDays/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _uow.WorkDayRepository.FindAsync(id.Value, User.GetUserId());
            if (workDay == null)
            {
                return NotFound();
            }
            
            var vm = new WorkDayViewModel()
            {
                Id = workDay.Id,
                Day = workDay.Day
            };

            return View(vm);
        }

        // POST: WorkDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WorkDayViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var entity = await _uow.WorkDayRepository.FindAsync(vm.Id, User.GetUserId());
                if (entity == null)
                {
                    return NotFound();
                }
                
                entity.Day = vm.Day;
                
                _uow.WorkDayRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: WorkDays/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _uow.WorkDayRepository.FindAsync(id.Value, User.GetUserId());
            
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
            await _uow.WorkDayRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
