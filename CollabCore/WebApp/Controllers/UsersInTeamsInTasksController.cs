using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.BLL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;
using Task = App.Domain.Task;

namespace WebApp.Controllers
{
    [Authorize]
    public class UsersInTeamsInTasksController : Controller
    {
        private readonly IAppBLL _bll;

        public UsersInTeamsInTasksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: UsersInTeamsInTasks
        public async Task<IActionResult> Index()
        {
            var res = await _bll.UserInTeamInTaskService.AllAsync();
            return View(res);
        }

        // GET: UsersInTeamsInTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.UserInTeamInTaskService.FindAsync(id.Value, User.GetUserId());
            
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
                TaskSelectList = new SelectList(await _bll.TaskService.AllAsync(User.GetUserId()), nameof(Task.Id),
                    nameof(Task.Name)),
                UsersInTeamSelectList = new SelectList(await _bll.UserInTeamService.AllAsync(User.GetUserId()),
                    nameof(UserInTeam.Id), nameof(UserInTeam.UserId))
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
                vm.UserInTeamInTask.Since = DateTime.SpecifyKind(vm.UserInTeamInTask.Since, DateTimeKind.Utc);
                if (vm.UserInTeamInTask.Until.HasValue)
                {
                    vm.UserInTeamInTask.Until = DateTime.SpecifyKind(vm.UserInTeamInTask.Until.Value, DateTimeKind.Utc);
                }
                
                _bll.UserInTeamInTaskService.Add(vm.UserInTeamInTask);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            

            vm.TaskSelectList = new SelectList(await _bll.TaskService.AllAsync(User.GetUserId()), nameof(Task.Id),
                nameof(Task.Name), vm.UserInTeamInTask.TaskId);
            vm.UsersInTeamSelectList = new SelectList(await _bll.UserInTeamService.AllAsync(User.GetUserId()),
                nameof(UserInTeam.Id), nameof(UserInTeam.UserId), vm.UserInTeamInTask.UserInTeamId);

            return View(vm);

        }

        // GET: UsersInTeamsInTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeamInTask = await _bll.UserInTeamInTaskService.FindAsync(id.Value, User.GetUserId());
            if (userInTeamInTask == null)
            {
                return NotFound();
            }

            var vm = new UserInTeamInTaskViewModel()
            {
                TaskSelectList = new SelectList(await _bll.TaskService.AllAsync(User.GetUserId()),
                    nameof(Task.Id),  nameof(Task.Name), userInTeamInTask.TaskId),
                UsersInTeamSelectList = new SelectList(await _bll.UserInTeamService.AllAsync(User.GetUserId()),
                    nameof(UserInTeam.Id), nameof(UserInTeam.UserId), userInTeamInTask.UserInTeamId),
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
                vm.UserInTeamInTask.Since = DateTime.SpecifyKind(vm.UserInTeamInTask.Since, DateTimeKind.Utc);
                if (vm.UserInTeamInTask.Until.HasValue)
                {
                    vm.UserInTeamInTask.Until = DateTime.SpecifyKind(vm.UserInTeamInTask.Until.Value, DateTimeKind.Utc);
                }
                
                _bll.UserInTeamInTaskService.Update(vm.UserInTeamInTask);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            
            vm.TaskSelectList = new SelectList(await _bll.TaskService.AllAsync(User.GetUserId()), nameof(Task.Id),
                nameof(Task.Name), vm.UserInTeamInTask.TaskId);
            vm.UsersInTeamSelectList = new SelectList(await _bll.UserInTeamService.AllAsync(User.GetUserId()),
                nameof(UserInTeam.Id), nameof(UserInTeam.UserId), vm.UserInTeamInTask.UserInTeamId);
            return View(vm);

        }

        // GET: UsersInTeamsInTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInTeamInTask = await _bll.UserInTeamInTaskService.FindAsync(id.Value, User.GetUserId());

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
            await _bll.UserInTeamInTaskService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}