using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.DAL.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IAppUOW _uow;

        public DepartmentsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var res = await _uow.DepartmentRepository.AllAsync();
            return View(res);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.DepartmentRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            var vm = new DepartmentViewModel();
            return View(vm);
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
                
                _uow.DepartmentRepository.Add(entity);
                await _uow.SaveChangesAsync();
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
            
            var department = await _uow.DepartmentRepository.FindAsync(id.Value, User.GetUserId());
            if (department == null)
            {
                return NotFound();
            }

            var vm = new DepartmentViewModel()
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
        public async Task<IActionResult> Edit(Guid id, DepartmentViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                var department = await _uow.DepartmentRepository.FindAsync(id, User.GetUserId());
                if (department == null)
                {
                    return NotFound();
                }
            
                department.Name = vm.Name;
                
                _uow.DepartmentRepository.Update(department);
                await _uow.SaveChangesAsync();
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
            
            var entity = await _uow.DepartmentRepository.FindAsync(id.Value, User.GetUserId());
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
            await _uow.DepartmentRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
