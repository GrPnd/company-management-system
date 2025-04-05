using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class SchedulesController : Controller
    {
        private readonly IAppUOW _uow;

        public SchedulesController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var res = await _uow.ScheduleRepository.AllAsync();
            return View(res);
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.ScheduleRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Schedules/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ScheduleViewModel()
            {
                TeamSelectList = new SelectList(await _uow.TeamRepository.AllAsync(User.GetUserId()), nameof(Team.Id),
                    nameof(Team.Name))
            };
            return View(vm);
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.ScheduleRepository.Add(vm.Schedule);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.TeamSelectList = new SelectList(await _uow.TeamRepository.AllAsync(User.GetUserId()), nameof(Team.Id),
                nameof(Team.Name), vm.Schedule.TeamId);
            return View(vm);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _uow.ScheduleRepository.FindAsync(id.Value, User.GetUserId());
            if (schedule == null)
            {
                return NotFound();
            }
            
            var vm = new ScheduleViewModel()
            {
                TeamSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()),
                    nameof(Team.Id), nameof(Team.Name), schedule.TeamId),
                Schedule = schedule
            };
            return View(vm);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ScheduleViewModel vm)
        {
            if (id != vm.Schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.ScheduleRepository.Update(vm.Schedule);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.TeamSelectList = new SelectList(await _uow.TeamRepository.AllAsync(User.GetUserId()),
                nameof(Team.Id), nameof(Team.Name), vm.Schedule.TeamId);
            return View(vm);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _uow.ScheduleRepository.FindAsync(id.Value, User.GetUserId());
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.ScheduleRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
