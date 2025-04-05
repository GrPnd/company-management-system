using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;
using Task = App.Domain.Task;

namespace WebApp.Controllers
{
    [Authorize]
    public class UsersInTeamsInTasksController : Controller
    {
        private readonly IAppUOW _uow;

        public UsersInTeamsInTasksController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: UsersInTeamsInTasks
        public async Task<IActionResult> Index()
        {
            var res = await _uow.UserInTeamInTaskRepository.AllAsync();
            return View(res);
        }

        // GET: UsersInTeamsInTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.UserInTeamInTaskRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: UsersInTeamsInTasks/Create
        public async Task<IActionResult> Create()
        {
            var vm = new UserInTeamInTaskViewModel()
            {
                TaskSelectList = new SelectList(await _uow.TaskRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Task.Name)),
                UsersInTeamSelectList = new SelectList(await _uow.UserInTeamRepository.AllAsync(User.GetUserId()),
                    nameof(UserInTeam.Id), nameof(UserInTeam.Role))
            };
            return View(vm);
        }

        // POST: UsersInTeamsInTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserInTeamInTaskViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.UserInTeamInTaskRepository.Add(vm.UserInTeamInTask);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            

            vm.TaskSelectList = new SelectList(await _uow.TaskRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Task.Name), vm.UserInTeamInTask.TaskId);
            vm.UsersInTeamSelectList = new SelectList(await _uow.UserInTeamRepository.AllAsync(User.GetUserId()),
                nameof(UserInTeam.Id), nameof(UserInTeam.Role), vm.UserInTeamInTask.UserInTeamId);

            return View(vm);

        }

        // GET: UsersInTeamsInTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeamInTask = await _uow.UserInTeamInTaskRepository.FindAsync(id.Value, User.GetUserId());
            if (userInTeamInTask == null)
            {
                return NotFound();
            }

            var vm = new UserInTeamInTaskViewModel()
            {
                TaskSelectList = new SelectList(await _uow.TaskRepository.AllAsync(User.GetUserId()),
                    nameof(UserInTeam.Id),  nameof(Task.Name), userInTeamInTask.TaskId),
                UsersInTeamSelectList = new SelectList(await _uow.UserInTeamRepository.AllAsync(User.GetUserId()),
                    nameof(UserInTeam.Id), nameof(UserInTeam.Role), userInTeamInTask.UserInTeamId),
                UserInTeamInTask = userInTeamInTask
            };
            return View(vm);
        }

        // POST: UsersInTeamsInTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserInTeamInTaskViewModel vm)
        {
            if (id != vm.UserInTeamInTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.UserInTeamInTaskRepository.Update(vm.UserInTeamInTask);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            
            vm.TaskSelectList = new SelectList(await _uow.TaskRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Task.Name), vm.UserInTeamInTask.TaskId);
            vm.UsersInTeamSelectList = new SelectList(await _uow.UserInTeamRepository.AllAsync(User.GetUserId()),
                nameof(UserInTeam.Id), nameof(UserInTeam.Role), vm.UserInTeamInTask.UserInTeamId);
            return View(vm);

        }

        // GET: UsersInTeamsInTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeamInTask = await _uow.UserInTeamInTaskRepository.FindAsync(id.Value, User.GetUserId());

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
            await _uow.UserInTeamInTaskRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}