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
    public class TeamsController : Controller
    {
        private readonly IAppBLL _bll;

        public TeamsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var res = await _bll.TeamService.AllAsync();
            return View(res);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.TeamService.FindAsync(id.Value, User.GetUserId());

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
                DepartmentsSelectList = new SelectList(await _bll.DepartmentService.AllAsync(User.GetUserId()), nameof(Department.Id),
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
                _bll.TeamService.Add(vm.Team);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.DepartmentsSelectList = new SelectList(await _bll.DepartmentService.AllAsync(User.GetUserId()),
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

            var team = await _bll.TeamService.FindAsync(id.Value, User.GetUserId());
            if (team == null)
            {
                return NotFound();
            }

            var vm = new TeamViewModel()
            {
                DepartmentsSelectList = new SelectList(await _bll.DepartmentService.AllAsync(User.GetUserId()), nameof(Department.Id),
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
                _bll.TeamService.Update(vm.Team);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.DepartmentsSelectList = new SelectList(await _bll.DepartmentService.AllAsync(User.GetUserId()),
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

            var team = await _bll.TeamService.FindAsync(id.Value, User.GetUserId());
            
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
            await _bll.TeamService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
