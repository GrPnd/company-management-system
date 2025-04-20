using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.DAL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class UsersInWorkDaysController : Controller
    {
        private readonly IAppUOW _uow;

        public UsersInWorkDaysController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: UsersInWorkDays
        public async Task<IActionResult> Index()
        {
            var res = await _uow.UserInWorkDayRepository.AllAsync();
            return View(res);
        }

        // GET: UsersInWorkDays/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.UserInWorkDayRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: UsersInWorkDays/Create
        public async Task<IActionResult> Create()
        {
            var vm = new UserInWorkDayViewModel()
            {
                UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.Id)),
                WorkDaysSelectList = new SelectList(await _uow.WorkDayRepository.AllAsync(User.GetUserId()), nameof(WorkDay.Id),
                    nameof(WorkDay.Id))
            };
            return View(vm);
        }

        // POST: UsersInWorkDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserInWorkDayViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.UserInWorkDayRepository.Add(vm.UserInWorkDay);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.UserInWorkDay.UserId);
            vm.WorkDaysSelectList = new SelectList(await _uow.WorkDayRepository.AllAsync(User.GetUserId()), nameof(WorkDay.Id),
                nameof(WorkDay.Id), vm.UserInWorkDay.WorkDayId);
            
            return View(vm);
        }

        // GET: UsersInWorkDays/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInWorkDay = await _uow.UserInWorkDayRepository.FindAsync(id.Value, User.GetUserId());
            if (userInWorkDay == null)
            {
                return NotFound();
            }
            
            var vm = new UserInWorkDayViewModel()
            {
                UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.Id), userInWorkDay.UserId),
                WorkDaysSelectList = new SelectList(await _uow.WorkDayRepository.AllAsync(User.GetUserId()), nameof(WorkDay.Id),
                    nameof(WorkDay.Id), userInWorkDay.WorkDayId)
            };
            return View(vm);
        }

        // POST: UsersInWorkDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserInWorkDayViewModel vm)
        {
            if (id != vm.UserInWorkDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.UserInWorkDayRepository.Update(vm.UserInWorkDay);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.UserInWorkDay.UserId);
            vm.WorkDaysSelectList = new SelectList(await _uow.WorkDayRepository.AllAsync(User.GetUserId()), nameof(WorkDay.Id),
                nameof(WorkDay.Id), vm.UserInWorkDay.WorkDayId);
            
            return View(vm);
        }

        // GET: UsersInWorkDays/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInWorkDay = await _uow.UserInWorkDayRepository.FindAsync(id.Value, User.GetUserId());
            
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
            await _uow.UserInWorkDayRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
