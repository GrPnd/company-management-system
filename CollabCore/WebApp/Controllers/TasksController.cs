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
    public class TasksController : Controller
    {
        private readonly IAppUOW _uow;

        public TasksController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var res = await _uow.TaskRepository.AllAsync();
            return View(res);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.TaskRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Tasks/Create
        public async Task<IActionResult> Create()
        {
            var vm = new TaskViewModel()
            {
                StatusesSelectList = new SelectList(await _uow.StatusRepository.AllAsync(User.GetUserId()), nameof(Status.Id),
                    nameof(Status.Name)),
                UsersInTeamSelectList = new SelectList(await _uow.UserInTeamRepository.AllAsync(User.GetUserId()),
                    nameof(UserInTeam.Id), nameof(UserInTeam.Role))
            };
            return View(vm);
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.TaskRepository.Add(vm.Task);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.StatusesSelectList = new SelectList(await _uow.StatusRepository.AllAsync(User.GetUserId()), nameof(Status.Id),
                nameof(Status.Name), vm.Task.StatusId);
            vm.UsersInTeamSelectList = new SelectList(await _uow.UserInTeamRepository.AllAsync(User.GetUserId()),
                nameof(UserInTeam.Id), nameof(UserInTeam.Role), vm.Task.UserInTeamId);

            return View(vm);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _uow.TaskRepository.FindAsync(id.Value, User.GetUserId());
            if (task == null)
            {
                return NotFound();
            }
            
            var vm = new TaskViewModel()
            {
                StatusesSelectList = new SelectList(await _uow.StatusRepository.AllAsync(User.GetUserId()), nameof(Status.Id),
                    nameof(Status.Name), task.StatusId),
                UsersInTeamSelectList = new SelectList(await _uow.UserInTeamRepository.AllAsync(User.GetUserId()),
                    nameof(UserInTeam.Id), nameof(UserInTeam.Role), task.UserInTeamId),
                Task = task
            };
            return View(vm);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TaskViewModel vm)
        {
            if (id != vm.Task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.TaskRepository.Update(vm.Task);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.StatusesSelectList = new SelectList(await _uow.StatusRepository.AllAsync(User.GetUserId()), nameof(Status.Id),
                nameof(Status.Name), vm.Task.StatusId);
            vm.UsersInTeamSelectList = new SelectList(await _uow.UserInTeamRepository.AllAsync(User.GetUserId()),
                nameof(UserInTeam.Id), nameof(UserInTeam.Role), vm.Task.UserInTeamId);
            
            return View(vm);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _uow.TaskRepository.FindAsync(id.Value, User.GetUserId());
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.TaskRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
