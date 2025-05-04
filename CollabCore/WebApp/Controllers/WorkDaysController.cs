using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.BLL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class WorkDaysController : Controller
    {
        private readonly IAppBLL _bll;

        public WorkDaysController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: WorkDays
        public async Task<IActionResult> Index()
        {
            var res = await _bll.WorkDayService.AllAsync();
            return View(res);
        }

        // GET: WorkDays/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.WorkDayService.FindAsync(id.Value, User.GetUserId());
            
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
                
                _bll.WorkDayService.Add(entity);
                await _bll.SaveChangesAsync();
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

            var workDay = await _bll.WorkDayService.FindAsync(id.Value, User.GetUserId());
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
                var entity = await _bll.WorkDayService.FindAsync(vm.Id, User.GetUserId());
                if (entity == null)
                {
                    return NotFound();
                }
                
                entity.Day = vm.Day;
                
                _bll.WorkDayService.Update(entity);
                await _bll.SaveChangesAsync();
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

            var workDay = await _bll.WorkDayService.FindAsync(id.Value, User.GetUserId());
            
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
            await _bll.WorkDayService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
