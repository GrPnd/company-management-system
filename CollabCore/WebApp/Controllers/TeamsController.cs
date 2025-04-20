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
    public class TeamsController : Controller
    {
        private readonly IAppUOW _uow;

        public TeamsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var res = await _uow.TeamRepository.AllAsync();
            return View(res);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.TeamRepository.FindAsync(id.Value, User.GetUserId());

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Teams/Create
        public async Task<IActionResult> Create()
        {
            var vm = new TeamViewModel()
            {
                DepartmentsSelectList = new SelectList(await _uow.DepartmentRepository.AllAsync(User.GetUserId()), nameof(Department.Id),
                    nameof(Department.Name))
            };
            return View(vm);
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.TeamRepository.Add(vm.Team);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.DepartmentsSelectList = new SelectList(await _uow.DepartmentRepository.AllAsync(User.GetUserId()),
                nameof(Department.Id), nameof(Department.Name), vm.Team.DepartmentId);

            return View(vm);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _uow.TeamRepository.FindAsync(id.Value, User.GetUserId());
            if (team == null)
            {
                return NotFound();
            }

            var vm = new TeamViewModel()
            {
                DepartmentsSelectList = new SelectList(await _uow.DepartmentRepository.AllAsync(User.GetUserId()), nameof(Department.Id),
                    nameof(Department.Name), team.DepartmentId),
                Team = team
            };
            return View(vm);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TeamViewModel vm)
        {
            if (id != vm.Team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.TeamRepository.Update(vm.Team);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.DepartmentsSelectList = new SelectList(await _uow.DepartmentRepository.AllAsync(User.GetUserId()),
                nameof(Department.Id), nameof(Department.Name), vm.Team.DepartmentId);

            return View(vm);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _uow.TeamRepository.FindAsync(id.Value, User.GetUserId());
            
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.TeamRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
