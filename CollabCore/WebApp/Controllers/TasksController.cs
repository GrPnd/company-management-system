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
    public class TasksController : Controller
    {
        private readonly IAppBLL _bll;

        public TasksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var res = await _bll.TaskService.AllAsync();
            return View(res);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.TaskService.FindAsync(id.Value, User.GetUserId());
            
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
                StatusesSelectList = new SelectList(await _bll.StatusService.AllAsync(User.GetUserId()), nameof(Status.Id),
                    nameof(Status.Name)),
                UsersInTeamSelectList = new SelectList(await _bll.UserInTeamService.AllAsync(User.GetUserId()),
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
                vm.Task.AssignedAt = DateTime.SpecifyKind(vm.Task.AssignedAt, DateTimeKind.Utc);
                vm.Task.Deadline = DateTime.SpecifyKind(vm.Task.Deadline, DateTimeKind.Utc);
                
                _bll.TaskService.Add(vm.Task);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.StatusesSelectList = new SelectList(await _bll.StatusService.AllAsync(User.GetUserId()), nameof(Status.Id),
                nameof(Status.Name), vm.Task.StatusId);
            vm.UsersInTeamSelectList = new SelectList(await _bll.UserInTeamService.AllAsync(User.GetUserId()),
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

            var task = await _bll.TaskService.FindAsync(id.Value, User.GetUserId());
            if (task == null)
            {
                return NotFound();
            }
            
            var vm = new TaskViewModel()
            {
                StatusesSelectList = new SelectList(await _bll.StatusService.AllAsync(User.GetUserId()), nameof(Status.Id),
                    nameof(Status.Name), task.StatusId),
                UsersInTeamSelectList = new SelectList(await _bll.UserInTeamService.AllAsync(User.GetUserId()),
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
                vm.Task.AssignedAt = DateTime.SpecifyKind(vm.Task.AssignedAt, DateTimeKind.Utc);
                vm.Task.Deadline = DateTime.SpecifyKind(vm.Task.Deadline, DateTimeKind.Utc);
                
                _bll.TaskService.Update(vm.Task);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.StatusesSelectList = new SelectList(await _bll.StatusService.AllAsync(User.GetUserId()), nameof(Status.Id),
                nameof(Status.Name), vm.Task.StatusId);
            vm.UsersInTeamSelectList = new SelectList(await _bll.UserInTeamService.AllAsync(User.GetUserId()),
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

            var task = await _bll.TaskService.FindAsync(id.Value, User.GetUserId());
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
            await _bll.TaskService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
