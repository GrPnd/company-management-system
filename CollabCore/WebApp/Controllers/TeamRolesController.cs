using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.BLL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class TeamRolesController : Controller
    {
        private readonly IAppBLL _bll;

        public TeamRolesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var res = await _bll.TeamRoleService.AllAsync();
            return View(res);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _bll.TeamRoleService.FindAsync(id.Value, User.GetUserId());
            
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            var vm = new TeamRoleViewModel();
            return View(vm);
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = new TeamRole()
                {
                    Name = vm.Name,
                };
                
                _bll.TeamRoleService.Add(entity);
                await _bll.SaveChangesAsync();
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

            var role = await _bll.TeamRoleService.FindAsync(id.Value, User.GetUserId());
            if (role == null)
            {
                return NotFound();
            }

            var vm = new TeamRoleEditViewModel()
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
        public async Task<IActionResult> Edit(Guid id, TeamRoleEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var entity = await _bll.TeamRoleService.FindAsync(vm.Id, User.GetUserId());
                if (entity == null)
                {
                    return NotFound();
                }
                
                entity.Name = vm.Name;
                
                _bll.TeamRoleService.Update(entity);
                await _bll.SaveChangesAsync();
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

            var role = await _bll.TeamRoleService.FindAsync(id.Value, User.GetUserId());
            
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
            await _bll.TeamRoleService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
