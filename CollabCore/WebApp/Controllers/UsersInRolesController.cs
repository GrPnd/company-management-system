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
    public class UsersInRolesController : Controller
    {
        private readonly IAppUOW _uow;

        public UsersInRolesController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: UsersInRoles
        public async Task<IActionResult> Index()
        {
            var res = await _uow.UserInRoleRepository.AllAsync();
            return View(res);
        }

        // GET: UsersInRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.UserInRoleRepository.FindAsync(id.Value, User.GetUserId());
            
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
                RolesSelectList = new SelectList(await _uow.RoleRepository.AllAsync(User.GetUserId()), nameof(Role.Id),
                    nameof(Role.Name)),
                UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.Id))
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
                _uow.UserInRoleRepository.Add(vm.UserInRole);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.RolesSelectList = new SelectList(await _uow.RoleRepository.AllAsync(User.GetUserId()), nameof(Role.Id),
                nameof(Role.Name), vm.UserInRole.RoleId);
            vm.UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.UserInRole.UserId);
            
            return View(vm);
        }

        // GET: UsersInRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInRole = await _uow.UserInRoleRepository.FindAsync(id.Value, User.GetUserId());
            if (userInRole == null)
            {
                return NotFound();
            }
            
            var vm = new UserInRoleViewModel()
            {
                RolesSelectList = new SelectList(await _uow.RoleRepository.AllAsync(User.GetUserId()), nameof(Role.Id),
                    nameof(Role.Name), userInRole.RoleId),
                UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                    nameof(Person.Id), userInRole.UserId),
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
                _uow.UserInRoleRepository.Update(vm.UserInRole);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.RolesSelectList = new SelectList(await _uow.RoleRepository.AllAsync(User.GetUserId()), nameof(Role.Id),
                nameof(Role.Name), vm.UserInRole.RoleId);
            vm.UsersSelectList = new SelectList(await _uow.PersonRepository.AllAsync(User.GetUserId()), nameof(Person.Id),
                nameof(Person.Id), vm.UserInRole.UserId);
            
            return View(vm);
        }

        // GET: UsersInRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInRole = await _uow.UserInRoleRepository.FindAsync(id.Value, User.GetUserId());
            
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
            await _uow.UserInRoleRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
