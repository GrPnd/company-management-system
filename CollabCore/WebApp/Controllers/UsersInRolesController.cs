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
    public class UsersInRolesController : Controller
    {
        private readonly IAppBLL _bll;

        public UsersInRolesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: UsersInRoles
        public async Task<IActionResult> Index()
        {
            var res = await _bll.UserInRoleService.AllAsync();
            return View(res);
        }

        // GET: UsersInRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.UserInRoleService.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: UsersInRoles/Create
        public async Task<IActionResult> Create()
        {
            var vm = new UserInRoleViewModel()
            {
                RolesSelectList = new SelectList(await _bll.RoleService.AllAsync(User.GetUserId()), nameof(Role.Id),
                    nameof(Role.Name)),
                UsersSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.PersonName))
            };
            return View(vm);
        }

        // POST: UsersInRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserInRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.UserInRole.Since = DateTime.SpecifyKind(vm.UserInRole.Since, DateTimeKind.Utc);
                if (vm.UserInRole.Until.HasValue)
                {
                    vm.UserInRole.Until = DateTime.SpecifyKind(vm.UserInRole.Until.Value, DateTimeKind.Utc);
                }
                
                _bll.UserInRoleService.Add(vm.UserInRole);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.RolesSelectList = new SelectList(await _bll.RoleService.AllAsync(User.GetUserId()), nameof(Role.Id),
                nameof(Role.Name), vm.UserInRole.RoleId);
            vm.UsersSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.PersonName), vm.UserInRole.UserId);
            
            return View(vm);
        }

        // GET: UsersInRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInRole = await _bll.UserInRoleService.FindAsync(id.Value, User.GetUserId());
            if (userInRole == null)
            {
                return NotFound();
            }
            
            var vm = new UserInRoleViewModel()
            {
                RolesSelectList = new SelectList(await _bll.RoleService.AllAsync(User.GetUserId()), nameof(Role.Id),
                    nameof(Role.Name), userInRole.RoleId),
                UsersSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.PersonName), userInRole.UserId),
                UserInRole = userInRole
            };
            return View(vm);
        }

        // POST: UsersInRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserInRoleViewModel vm)
        {
            if (id != vm.UserInRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vm.UserInRole.Since = DateTime.SpecifyKind(vm.UserInRole.Since, DateTimeKind.Utc);
                if (vm.UserInRole.Until.HasValue)
                {
                    vm.UserInRole.Until = DateTime.SpecifyKind(vm.UserInRole.Until.Value, DateTimeKind.Utc);
                }
                
                _bll.UserInRoleService.Update(vm.UserInRole);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.RolesSelectList = new SelectList(await _bll.RoleService.AllAsync(User.GetUserId()), nameof(Role.Id),
                nameof(Role.Name), vm.UserInRole.RoleId);
            vm.UsersSelectList = new SelectList(await _bll.PersonService.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.PersonName), vm.UserInRole.UserId);
            
            return View(vm);
        }

        // GET: UsersInRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInRole = await _bll.UserInRoleService.FindAsync(id.Value, User.GetUserId());
            
            if (userInRole == null)
            {
                return NotFound();
            }

            return View(userInRole);
        }

        // POST: UsersInRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.UserInRoleService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
