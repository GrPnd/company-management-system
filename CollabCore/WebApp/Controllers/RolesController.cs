using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.DAL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IAppUOW _uow;

        public RolesController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var res = await _uow.RoleRepository.AllAsync();
            return View(res);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _uow.RoleRepository.FindAsync(id.Value, User.GetUserId());
            
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            var vm = new RoleViewModel();
            return View(vm);
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = new Role()
                {
                    Name = vm.Name,
                };
                
                _uow.RoleRepository.Add(entity);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _uow.RoleRepository.FindAsync(id.Value, User.GetUserId());
            if (role == null)
            {
                return NotFound();
            }

            var vm = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(vm);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RoleViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var entity = await _uow.RoleRepository.FindAsync(vm.Id, User.GetUserId());
                if (entity == null)
                {
                    return NotFound();
                }
                
                entity.Name = vm.Name;
                
                _uow.RoleRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _uow.RoleRepository.FindAsync(id.Value, User.GetUserId());
            
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.RoleRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
