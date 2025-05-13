using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;
using Department = App.BLL.DTO.Department;

namespace WebApp.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IAppBLL _bll;

        public DepartmentsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var res = await _bll.DepartmentService.AllAsync();
            return View(res);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.DepartmentService.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = new Department
                {
                    Name = vm.Name
                };
                
                _bll.DepartmentService.Add(entity, User.GetUserId());
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var department = await _bll.DepartmentService.FindAsync(id.Value, User.GetUserId());
            if (department == null)
            {
                return NotFound();
            }

            var vm = new DepartmentEditViewModel()
            {
                Id = department.Id,
                Name = department.Name
            };

            return View(vm);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DepartmentEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                var department = await _bll.DepartmentService.FindAsync(id, User.GetUserId());
                if (department == null)
                {
                    return NotFound();
                }
            
                department.Name = vm.Name;
                
                _bll.DepartmentService.Update(department);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var entity = await _bll.DepartmentService.FindAsync(id.Value, User.GetUserId());
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.DepartmentService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
